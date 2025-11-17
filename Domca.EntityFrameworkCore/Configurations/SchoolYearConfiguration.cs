using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Provides the Entity Framework Core configuration for the SchoolYear entity type.
/// </summary>
/// <remarks>This class defines how the SchoolYear entity is mapped to the database schema, including key
/// configuration, property conversions, required fields, and relationships. It is typically used within the DbContext's
/// OnModelCreating method to apply custom mapping rules.</remarks>
public class SchoolYearConfiguration : IEntityTypeConfiguration<SchoolYear>
{
    /// <summary>
    /// Configures the entity type mapping for the SchoolYear entity in the Entity Framework model builder.
    /// </summary>
    /// <remarks>Call this method within the Entity Framework model configuration to specify keys, property
    /// conversions, required fields, ignored properties, and relationships for the SchoolYear entity.</remarks>
    /// <param name="builder">The builder used to define the SchoolYear entity's schema, relationships, and property configurations.</param>
    public void Configure(EntityTypeBuilder<SchoolYear> builder)
    {
        builder.HasKey(sy => sy.Id);

        builder.Property(sy => sy.Id)
               .HasConversion(id => id.Value, value => new SchoolYearId(value));

        builder.Property(sy => sy.StartYear).IsRequired();
        builder.Property(sy => sy.EndYear).IsRequired();

        builder.Ignore(sy => sy.Label);

        builder.HasMany(sy => sy.Subjects)
               .WithOne(s => s.SchoolYear)
               .HasForeignKey(s => s.SchoolYearId);
    }
}
