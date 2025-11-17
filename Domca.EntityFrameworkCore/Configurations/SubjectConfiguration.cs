using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Configures the entity mapping for the Subject type within the Entity Framework model.
/// </summary>
/// <remarks>This class defines the database schema, relationships, and property conversions for the Subject
/// entity using the Entity Framework Core fluent API. It is typically used during model creation to ensure that the
/// Subject entity is correctly mapped to the underlying database structure.</remarks>
public sealed class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    /// <summary>
    /// Configures the entity type mapping for the <see cref="Subject"/> entity in the Entity Framework model builder.
    /// </summary>
    /// <remarks>This method sets up primary keys, property conversions, required fields, ignored properties,
    /// and relationships for the <see cref="Subject"/> entity. It should be called within the Entity Framework model
    /// configuration process, typically in <c>OnModelCreating</c>.</remarks>
    /// <param name="builder">The <see cref="EntityTypeBuilder{Subject}"/> used to define the entity's schema, relationships, and property
    /// configurations.</param>
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
               .HasConversion(id => id.Value, value => new SubjectId(value));

        builder.Property(s => s.TeacherId)
               .HasConversion(id => id.Value, value => new TeacherId(value));

        builder.Property(s => s.SchoolYearId)
               .HasConversion(id => id.Value, value => new SchoolYearId(value));

        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);

        builder.Ignore(s => s.WeightedAverage);

        builder.HasOne(s => s.Teacher)
               .WithMany(t => t.Subjects)
               .HasForeignKey(s => s.TeacherId);

        builder.HasOne(s => s.SchoolYear)
               .WithMany(sy => sy.Subjects)
               .HasForeignKey(s => s.SchoolYearId);

        builder.HasMany(s => s.Marks)
               .WithOne(m => m.Subject)
               .HasForeignKey(m => m.SubjectId);
    }
}
