using Domca.Core.Entities.IDs;

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
    HydrationRecordId id,
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
        HydrationRecordId.New(),
        UserId.New(),
        DateTime.UtcNow,
        1)
    {
    }

    /// <summary>
    /// Gets the unique identifier for this hydration record.
    /// </summary>
    public HydrationRecordId Id { get; private set; } = id;

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
        private set => _date = EnsureUtc(value);
    }

    /// <summary>
    /// Gets the total amount of water consumed in milliliters.
    /// </summary>
    public int AmountMl { get; private set; } = amountMl > 0
        ? amountMl
        : throw new ArgumentOutOfRangeException(nameof(amountMl), "Amount of water must be greater than zero.");

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets the user associated with this hydration record.
    /// </summary>
    public User? User { get; private set; }

    #endregion

    /// <summary>
    /// Ensures that the DateTime is always stored as UTC.
    /// </summary>
    private static DateTime EnsureUtc(DateTime value)
    {
        if (value.Kind == DateTimeKind.Utc)
            return value;

        if (value.Kind == DateTimeKind.Local)
            return value.ToUniversalTime();

        return DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}