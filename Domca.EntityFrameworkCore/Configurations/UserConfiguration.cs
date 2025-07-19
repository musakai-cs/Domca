using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domca.Core.Entities;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Configures the entity of type <see cref="User"/> in the database context.
/// </summary>
/// <remarks>This configuration class sets up the properties and relationships for the <see cref="User"/> entity,
/// including primary key, required fields, maximum lengths, and relationships with other entities.</remarks>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity of type <see cref="User"/> by setting up its properties and relationships.
    /// </summary>
    /// <remarks>This method defines the primary key, required properties, and maximum lengths for the <see
    /// cref="User"/> entity. It also configures the conversion for the <c>AvatarUrl</c> property and establishes a
    /// one-to-many relationship with the <c>Sessions</c> entity, enforcing cascade delete behavior.</remarks>
    /// <param name="builder">The <see cref="EntityTypeBuilder{User}"/> used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.EmailAddress)
            .IsRequired();

        builder.Property(u => u.EmailAddressNormalized)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.PasswordSalt)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();

        builder.HasMany(u => u.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}