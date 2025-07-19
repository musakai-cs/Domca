using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domca.Core.Entities;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Configures the entity type for <see cref="UserSession"/>.
/// </summary>
/// <remarks>This configuration sets up the primary key, required properties, and relationships for the <see
/// cref="UserSession"/> entity. It ensures that the <c>Token</c>, <c>UserId</c>, <c>CreatedAt</c>, and <c>ExpiresAt</c>
/// properties are required. Additionally, it establishes a one-to-many relationship between <see cref="User"/> and <see
/// cref="UserSession"/>, with a cascade delete behavior on the foreign key.</remarks>
public sealed class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    /// <summary>
    /// Configures the <see cref="UserSession"/> entity type.
    /// </summary>
    /// <remarks>This method sets up the primary key, required properties, and relationships for the <see
    /// cref="UserSession"/> entity. It ensures that the <c>Token</c>, <c>UserId</c>, <c>CreatedAt</c>, and
    /// <c>ExpiresAt</c> properties are required. Additionally, it configures a one-to-many relationship between <see
    /// cref="User"/> and <see cref="UserSession"/>, with a cascade delete behavior on the foreign key
    /// <c>UserId</c>.</remarks>
    /// <param name="builder">The builder used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Token)
            .IsRequired();

        builder.Property(s => s.UserId)
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.ExpiresAt)
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}