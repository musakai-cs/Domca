using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Provides the Entity Framework Core configuration for the Teacher entity type.
/// </summary>
/// <remarks>This class defines the schema mapping, property constraints, and relationships for the Teacher entity
/// when using Entity Framework Core. It is typically used in the OnModelCreating method to apply custom configuration
/// for the Teacher table, including key definitions, required properties, maximum lengths, and navigation
/// properties.</remarks>
public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    /// <summary>
    /// Configures the entity type mapping for the Teacher entity in the model builder.
    /// </summary>
    /// <remarks>This method sets up primary key, property constraints, and relationships for the Teacher
    /// entity. It should be called within the Entity Framework Core model configuration process, typically in the
    /// OnModelCreating method of a DbContext.</remarks>
    /// <param name="builder">The builder used to define the Teacher entity's schema and relationships.</param>
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .HasConversion(id => id.Value, value => new TeacherId(value));

        builder.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(t => t.LastName).IsRequired().HasMaxLength(100);

        builder.HasMany(t => t.Subjects)
               .WithOne(s => s.Teacher)
               .HasForeignKey(s => s.TeacherId);
    }
}
