using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Provides the Entity Framework Core configuration for the Mark entity type.
/// </summary>
/// <remarks>This class defines how the Mark entity is mapped to the database schema, including key configuration,
/// property conversions, required fields, and relationships. It is typically used within the DbContext's
/// OnModelCreating method to apply custom mapping rules for the Mark entity.</remarks>
public class MarkConfiguration : IEntityTypeConfiguration<Mark>
{
    /// <summary>
    /// Configures the entity type mapping for the Mark entity within the model builder context.
    /// </summary>
    /// <remarks>Call this method within the Entity Framework model configuration to specify key properties,
    /// required fields, value conversions, and relationships for the Mark entity.</remarks>
    /// <param name="builder">The builder used to define the Mark entity's schema, relationships, and property conversions.</param>
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
               .HasConversion(id => id.Value, value => new MarkId(value));

        builder.Property(m => m.SubjectId)
               .HasConversion(id => id.Value, value => new SubjectId(value));

        builder.Property(m => m.Value).IsRequired();
        builder.Property(m => m.Weight).IsRequired();

        builder.HasOne(m => m.Subject)
               .WithMany(s => s.Marks)
               .HasForeignKey(m => m.SubjectId);
    }
}
