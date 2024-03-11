using System.Reflection;

namespace Common.Domain;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreMemberAttribute : Attribute
{
}

public abstract class ValueObject : IEquatable<ValueObject>
{
    private List<PropertyInfo> properties;
    private List<FieldInfo> fields;

    public static bool operator ==(ValueObject? obj1, ValueObject? obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null))
            {
                return true;
            }
            return false;
        }
        return obj1.Equals(obj: obj2);
    }

    public static bool operator !=(ValueObject? obj1, ValueObject? obj2)
    {
        return !(obj1 == obj2);
    }

    public bool Equals(ValueObject obj)
    {
        return Equals(obj as object);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        return Properties.All(p => ArePropertiesEqual(obj, p))
            && Fields.All(f => AreFieldsEqual(obj, f));
    }

    private bool ArePropertiesEqual(object obj, PropertyInfo p)
    {
        return Equals(p.GetValue(this, null), p.GetValue(obj, null));
    }

    private bool AreFieldsEqual(object obj, FieldInfo f)
    {
        return Equals(f.GetValue(this), f.GetValue(obj));
    }

    private IEnumerable<PropertyInfo> Properties
    {
        get
        {
            properties ??= GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();

            return properties;
        }
    }

    private IEnumerable<FieldInfo> Fields
    {
        get
        {
            fields ??= GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute))).ToList();

            return fields;
        }
    }

    public override int GetHashCode()
    {
        unchecked   //Allow Overflow
        {
            int hash = 17;
            foreach (var prop in Properties)
            {
                var value = prop.GetValue(this, null);
                hash = HashValue(hash, value);
            }

            foreach (var field in Fields)
            {
                var value = field.GetValue(this);
                hash = HashValue(hash, value);
            }

            return hash;
        }
    }

    private int HashValue(int seed, object value)
    {
        var currentHash = value != null
            ? value.GetHashCode()
            : 0;

        return seed * 23 + currentHash;
    }
}