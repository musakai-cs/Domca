using Domca.Core.Entities.IDs;
using Domca.Core.Helpers;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a record of water consumption by a user.
/// </summary>
/// <remarks>
/// This entity captures the amount of water consumed and the precise timestamp.
/// It enforces invariants such as positive amount and valid associations via the Primary Constructor.
/// </remarks>
/// <param name="id">Unique identifier for this record.</param>
/// <param name="userId">Foreign key of the user who owns this record.</param>
/// <param name="date">The date and time when the water was consumed.</param>
/// <param name="amountMl">The amount of water consumed in milliliters.</param>
public sealed class HydrationRecord(
    UserId userId,
    DateTime date,
    int amountMl)
{
    private DateTime _date = date;

    /// <summary>
    /// EF Core constructor for materialization.
    /// Passes dummy but VALID values to the primary constructor to bypass guard clauses.
    /// </summary>
    private HydrationRecord() : this(
        UserId.New(),
        DateTime.UtcNow,
        1)
    {
    }

    /// <summary>
    /// Gets the unique identifier for this hydration record.
    /// </summary>
    public HydrationRecordId Id { get; private set; } = HydrationRecordId.New();

    /// <summary>
    /// Gets the Foreign Key referencing the user who owns this record.
    /// </summary>
    public UserId UserId { get; private set; } = userId;

    /// <summary>
    /// Gets the date and time of the hydration record (Always UTC).
    /// </summary>
    public DateTime Date
    {
        get => _date;
        private set => _date = DateHelper.EnsureUtc(value);
    }

    /// <summary>
    /// Gets the total amount of water consumed in milliliters.
    /// </summary>
    public int AmountMl { get; private set; } = amountMl > 0 && amountMl < 10_000
        ? amountMl
        : throw new ArgumentOutOfRangeException(nameof(amountMl), "Amount of water must be between 1 and 10,000 millilitres.");

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets the user associated with this hydration record.
    /// </summary>
    public User? User { get; private set; }

    #endregion
}