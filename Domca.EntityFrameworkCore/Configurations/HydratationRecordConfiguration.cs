using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Provides configuration for the HydratationRecord entity type within the Entity Framework model.
/// </summary>
/// <remarks>This class is typically used to define entity mapping, relationships, and constraints for
/// HydrationRecord when using Entity Framework Core's fluent API. It is intended to be used in the OnModelCreating
/// method of a DbContext.</remarks>
public class HydratationRecordConfiguration : IEntityTypeConfiguration<HydratationRecord>
{
    /// <summary>
    /// Configures the entity mapping for the HydrtaationRecord type within the Entity Framework model builder.
    /// </summary>
    /// <remarks>This method sets up key properties, value conversions, required fields, and relationships for
    /// HydrationRecord entities. It should be called from the OnModelCreating method when customizing the model for
    /// database persistence.</remarks>
    /// <param name="builder">The EntityTypeBuilder instance used to define the HydratationRecord entity's schema and relationships.</param>
    public void Configure(EntityTypeBuilder<HydratationRecord> builder)
    {
        builder.HasKey(hr => hr.Id);

        builder.Property(hr => hr.Id)
               .HasConversion(id => id.Value, value => new HydratationRecordId(value));

        builder.Property(hr => hr.UserId)
               .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(hr => hr.Date).IsRequired();
        builder.Property(hr => hr.AmountMl).IsRequired();

        builder.HasOne(hr => hr.User)
               .WithMany(u => u.HydratationRecords)
               .HasForeignKey(hr => hr.UserId);
    }
}
