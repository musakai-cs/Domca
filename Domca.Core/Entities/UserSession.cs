using Domca.Core.Entities.IDs;
using Domca.Core.Helpers;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a user session containing authentication and session details.
/// </summary>
/// <remarks>
/// This class manages user sessions, tracking identifiers, tokens, and expiration.
/// It enforces invariants (valid token, dates) via the Primary Constructor.
/// </remarks>
/// <param name="id">Unique identifier for the session.</param>
/// <param name="userId">Foreign key of the user who owns this session.</param>
/// <param name="token">The authentication token.</param>
/// <param name="createdAt">The date and time when the session was created.</param>
/// <param name="expiresAt">The date and time when the session expires.</param>
public sealed class UserSession(
    UserSessionId id,
    UserId userId,
    string token,
    DateTime createdAt,
    DateTime expiresAt)
{
    private DateTime _createdAt = createdAt;
    private DateTime _expiresAt = expiresAt;

    /// <summary>
    /// EF Core constructor for materialization.
    /// Passes dummy but VALID values to the primary constructor.
    /// Crucial: ExpiresAt must be > CreatedAt to pass validation logic if strict validation is added later.
    /// </summary>
    private UserSession() : this(
        UserSessionId.New(),
        UserId.New(),
        "EF_DUMMY_TOKEN",
        DateTime.UtcNow,
        DateTime.UtcNow.AddDays(1))
    {
    }

    /// <summary>
    /// Gets the unique identifier for the session.
    /// </summary>
    public UserSessionId Id { get; private set; } = id;

    /// <summary>
    /// Gets the unique identifier for the user.
    /// </summary>
    public UserId UserId { get; private set; } = userId;

    /// <summary>
    /// Gets the authentication token used for accessing secured resources.
    /// </summary>
    public string Token { get; private set; } = !string.IsNullOrWhiteSpace(token)
        ? token
        : throw new ArgumentException("Session token cannot be empty.", nameof(token));

    /// <summary>
    /// Gets the date and time when the entity was created (Always UTC).
    /// </summary>
    public DateTime CreatedAt
    {
        get => _createdAt;
        private set => _createdAt = DateHelper.EnsureUtc(value);
    }

    /// <summary>
    /// Gets the expiration date and time for the current session (Always UTC).
    /// </summary>
    public DateTime ExpiresAt
    {
        get => _expiresAt;
        private set => _expiresAt = DateHelper.EnsureUtc(value);
    }

    /// <summary>
    /// Computed property indicating if the session is currently active (not expired).
    /// </summary>
    public bool IsActive => DateTime.UtcNow < ExpiresAt;

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets the associated user entity.
    /// </summary>
    public User? User { get; private set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a new UserSession with a default expiration policy (e.g., 30 days).
    /// </summary>
    /// <param name="userId">The ID of the user logging in.</param>
    /// <param name="token">The generated authentication token.</param>
    /// <param name="validDays">Validity period in days (default is 30).</param>
    /// <returns>A new, valid UserSession instance.</returns>
    public static UserSession Create(UserId userId, string token, int validDays = 30)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);
        if (validDays <= 0) throw new ArgumentOutOfRangeException(nameof(validDays), "Validity period must be positive.");

        var now = DateTime.UtcNow;
        var expiresAt = now.AddDays(validDays);

        return new UserSession(
            UserSessionId.New(),
            userId,
            token,
            now,
            expiresAt
        );
    }

    #endregion

    #region Domain Behaviors

    /// <summary>
    /// Extends the session validity to a new date.
    /// </summary>
    /// <param name="newExpiration">The new expiration date.</param>
    public void ExtendValidity(DateTime newExpiration)
    {
        var utcNewExpiration = DateHelper.EnsureUtc(newExpiration);

        if (utcNewExpiration <= CreatedAt)
        {
            throw new InvalidOperationException("New expiration date must be after the creation date.");
        }

        if (utcNewExpiration <= DateTime.UtcNow)
        {
            throw new InvalidOperationException("Cannot extend session to a time in the past.");
        }

        ExpiresAt = utcNewExpiration;
    }

    #endregion
}