namespace Common.Domain;

public class BaseEntity<TKey>
{
    public TKey Id { get; protected set; }
    public DateTime CreationDate { get; set; }

    public BaseEntity()
    {
        CreationDate = DateTime.Now;
    }

    public override bool Equals(object obj)
    {
        var entity = obj as BaseEntity<TKey>;
        return entity != null &&
            GetType() == entity.GetType() &&
            EqualityComparer<TKey>.Default.Equals(Id, entity.Id);
    }

    public static bool operator ==(BaseEntity<TKey> a, BaseEntity<TKey> b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity<TKey> a, BaseEntity<TKey> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }
}
