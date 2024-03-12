using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UM.Domain.RoleAgg;

namespace UM.Infrastructure.Persistent.EFCore.RoleAgg;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "role");

        builder.Property(b => b.Id)
               .HasConversion(v => v.Value
                            , v => new RoleId(v));

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(60);

        builder.OwnsMany(b => b.Permissions, option =>
        {
            option.ToTable("Permissions", "role");
        });
    }
}
