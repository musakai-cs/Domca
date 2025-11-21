using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domca.EntityFrameworkCore.Configurations;

/// <summary>
/// Provides configuration for the HydrationRecord entity type within the Entity Framework model.
/// </summary>
/// <remarks>This class is typically used to define entity mapping, relationships, and constraints for
/// HydrationRecord when using Entity Framework Core's fluent API. It is intended to be used in the OnModelCreating
/// method of a DbContext.</remarks>
public class HydrationRecordConfiguration : IEntityTypeConfiguration<HydrationRecord>
{
    /// <summary>
    /// Configures the entity mapping for the HydrationRecord type within the Entity Framework model builder.
    /// </summary>
    /// <remarks>This method sets up key properties, value conversions, required fields, and relationships for
    /// HydrationRecord entities. It should be called from the OnModelCreating method when customizing the model for
    /// database persistence.</remarks>
    /// <param name="builder">The EntityTypeBuilder instance used to define the HydrationRecord entity's schema and relationships.</param>
    public void Configure(EntityTypeBuilder<HydrationRecord> builder)
    {
        builder.HasKey(hr => hr.Id);

        builder.Property(hr => hr.Id)
               .HasConversion(id => id.Value, value => new HydrationRecordId(value));

        builder.Property(hr => hr.UserId)
               .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(hr => hr.Date).IsRequired();
        builder.Property(hr => hr.AmountMl).IsRequired();

        builder.HasOne(hr => hr.User)
               .WithMany(u => u.HydrationRecords)
               .HasForeignKey(hr => hr.UserId);
    }
}
