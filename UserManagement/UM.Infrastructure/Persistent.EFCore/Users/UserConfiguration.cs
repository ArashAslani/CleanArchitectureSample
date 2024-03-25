using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UM.Domain.Users;
using UM.Domain.RoleAgg;

namespace UM.Infrastructure.Persistent.EFCore.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "user");
        builder.HasIndex(b => b.PhoneNumber).IsUnique();
        builder.HasIndex(b => b.Email).IsUnique();

        builder.Property(x => x.Id)
                    .HasConversion(
                        v => v.Value,
                        v => new UserId(v));

        builder.Property(b => b.Email)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(b => b.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(b => b.FirstName)
            .IsRequired(false)

           .HasMaxLength(80);

        builder.Property(b => b.LastName)
            .IsRequired(false)
            .HasMaxLength(80);

        builder.Property(b => b.Password)
            .IsRequired()
            .HasMaxLength(50);



        builder.OwnsMany(b => b.Tokens, option =>
        {
            option.ToTable("Tokens", "user");
            option.HasKey(b => b.Id);

            option.Property(x => x.UserId)
                .HasConversion(
                    v => v.Value,
                    v => new UserId(v));
            option.HasIndex(b => b.UserId);


            option.Property(b => b.HashJwtToken)
                .IsRequired()
                .HasMaxLength(250);

            option.Property(b => b.HashRefreshToken)
                .IsRequired()
                .HasMaxLength(250);

            option.Property(b => b.Device)
                .IsRequired()
                .HasMaxLength(100);
        });
       

        builder.OwnsMany(b => b.Roles, option =>
        {
            option.ToTable("Roles", "user");
            option.Property(x => x.UserId)
                .HasConversion(
                    v => v.Value,
                    v => new UserId(v));
            option.Property(x => x.RoleId)
                .HasConversion(
                    v => v.Value,
                    v => new RoleId(v));
            option.HasIndex(b => b.UserId);
            option.HasIndex(b => b.RoleId);

        });
    }
}