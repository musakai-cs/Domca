using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Configures the entity of type <see cref="User"/> in the database context.
/// </summary>
/// <remarks>This configuration class sets up the properties and relationships for the <see cref="User"/> entity,
/// including primary key, required fields, maximum lengths, and relationships with other entities.</remarks>
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
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

        builder.Property(u => u.Id)
               .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.EmailAddress).IsRequired().HasMaxLength(255);
        builder.Property(u => u.EmailAddressNormalized).IsRequired().HasMaxLength(255);

        builder.HasMany(u => u.Sessions)
               .WithOne(s => s.User)
               .HasForeignKey(s => s.UserId);

        builder.HasMany(u => u.HydrationRecords)
               .WithOne(hr => hr.User)
               .HasForeignKey(hr => hr.UserId);


        // Indexing
        #region Indexing

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.EmailAddressNormalized).IsUnique();

        #endregion
    }
}