using Domca.Core.Entities.IDs;
using System.ComponentModel.DataAnnotations;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a hydration record for a user, tracking daily water intake.
/// </summary>
/// <remarks>
/// This entity captures the amount of water consumed by a user on a specific date.
/// It is designed to support health and fitness tracking functionalities.
/// </remarks>
public sealed class HydrationRecord
{
    /// <summary>
    /// Gets or sets the unique identifier for this hydration record.
    /// </summary>
    public HydrationRecordId Id { get; set; } = HydrationRecordId.New();

    // Scalar properties
    /// <summary>
    /// Gets or sets the date of the hydration record.
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the total amount of water consumed in milliliters.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int AmountMl { get; set; }

    /// <summary>
    /// Foreign key referencing the user who owns this hydration record.
    /// </summary>
    public UserId UserId { get; set; }

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets or sets the user associated with this hydration record.
    /// </summary>
    public User User { get; set; } = null!;

    #endregion
}
