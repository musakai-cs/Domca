using Domca.Core.Entities.IDs;
using Domca.Core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a user with personal and authentication details within the domain.
/// </summary>
/// <remarks>
/// This class encapsulates user information including identifiers, contact details, and authentication
/// data. It enforces invariants via the Primary Constructor and private setters.
/// </remarks>
/// <param name="id">Unique identifier for the user.</param>
/// <param name="firstName">The first name of the user.</param>
/// <param name="lastName">The last name of the user.</param>
/// <param name="userName">The username (login) of the user.</param>
/// <param name="email">The email address of the user.</param>
/// <param name="passwordHash">The hashed password.</param>
/// <param name="passwordSalt">The salt used for password hashing.</param>
public sealed class User(
    string firstName,
    string lastName,
    string userName,
    string email,
    string passwordHash,
    string passwordSalt)
{
    private readonly List<UserSession> _sessions = [];
    private readonly List<HydrationRecord> _hydrationRecords = [];

    private DateTime _createdAt = DateTime.UtcNow;
    private DateTime _updatedAt = DateTime.UtcNow;

    /// <summary>
    /// EF Core constructor for materialization.
    /// Passes dummy but VALID values to the primary constructor to bypass guard clauses.
    /// </summary>
    private User() : this(
        "EF_DUMMY",
        "EF_DUMMY",
        "EF_DUMMY",
        "dummy@ef.com",
        "EF_DUMMY",
        "EF_DUMMY")
    {
    }

    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public UserId Id { get; private set; } = UserId.New();

    /// <summary>
    /// Gets the first name of the user.
    /// </summary>
    public string FirstName { get; private set; } = !string.IsNullOrWhiteSpace(firstName)
        ? firstName
        : throw new ArgumentException("First name cannot be empty.", nameof(firstName));

    /// <summary>
    /// Gets the last name of the user.
    /// </summary>
    public string LastName { get; private set; } = !string.IsNullOrWhiteSpace(lastName)
        ? lastName
        : throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    public string UserName { get; private set; } = !string.IsNullOrWhiteSpace(userName)
        ? userName
        : throw new ArgumentException("Username cannot be empty.", nameof(userName));

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    [EmailAddress]
    public string EmailAddress { get; private set; } = !string.IsNullOrWhiteSpace(email)
        ? email
        : throw new ArgumentException("Email cannot be empty.", nameof(email));

    /// <summary>
    /// Gets the normalized version of the email address (uppercase).
    /// </summary>
    [EmailAddress]
    public string EmailAddressNormalized { get; private set; } = email?.ToUpperInvariant() ?? string.Empty;

    /// <summary>
    /// Gets the hashed representation of the user's password.
    /// </summary>
    public string PasswordHash { get; private set; } = !string.IsNullOrWhiteSpace(passwordHash)
        ? passwordHash
        : throw new ArgumentException("Password hash is required.", nameof(passwordHash));

    /// <summary>
    /// Gets the salt value used for hashing the password.
    /// </summary>
    public string PasswordSalt { get; private set; } = !string.IsNullOrWhiteSpace(passwordSalt)
        ? passwordSalt
        : throw new ArgumentException("Password salt is required.", nameof(passwordSalt));

    /// <summary>
    /// Gets the URL of the user's avatar image.
    /// </summary>
    public Uri? AvatarUrl { get; private set; }

    /// <summary>
    /// Gets the date and time when the entity was created (Always UTC).
    /// </summary>
    public DateTime CreatedAt
    {
        get => _createdAt;
        private set => _createdAt = DateHelper.EnsureUtc(value);
    }

    /// <summary>
    /// Gets the date and time when the entity was last updated (Always UTC).
    /// </summary>
    public DateTime UpdatedAt
    {
        get => _updatedAt;
        private set => _updatedAt = DateHelper.EnsureUtc(value);
    }

    /// <summary>
    /// Gets the collection of user sessions.
    /// </summary>
    public IReadOnlyCollection<UserSession> Sessions => _sessions.AsReadOnly();

    /// <summary>
    /// Gets the collection of hydration records for the user.
    /// </summary>
    public IReadOnlyCollection<HydrationRecord> HydrationRecords => _hydrationRecords.AsReadOnly();

    #region Domain Behaviors

    /// <summary>
    /// Updates the user's personal profile information.
    /// </summary>
    /// <param name="newFirstName">The new first name.</param>
    /// <param name="newLastName">The new last name.</param>
    /// <param name="newAvatarUrl">The new avatar URL (optional).</param>
    /// <exception cref="ArgumentException">Thrown when required fields are empty.</exception>
    public void UpdateProfile(string newFirstName, string newLastName, Uri? newAvatarUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newFirstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(newLastName);

        FirstName = newFirstName;
        LastName = newLastName;
        AvatarUrl = newAvatarUrl;

        UpdateTimestamp();
    }

    /// <summary>
    /// Changes the user's email address and updates the normalized version.
    /// </summary>
    /// <param name="newEmail">The new email address.</param>
    public void ChangeEmail(string newEmail)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newEmail);

        if (EmailAddress.Equals(newEmail, StringComparison.OrdinalIgnoreCase))
            return;

        // Add validation for existing email in the database.
        EmailAddress = newEmail;
        EmailAddressNormalized = newEmail.ToUpperInvariant();

        UpdateTimestamp();
    }

    /// <summary>
    /// Securely updates the user's password hash and salt.
    /// </summary>
    /// <param name="newPasswordHash">The new password hash.</param>
    /// <param name="newPasswordSalt">The new password salt.</param>
    public void ChangePassword(string newPasswordHash, string newPasswordSalt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newPasswordHash);
        ArgumentException.ThrowIfNullOrWhiteSpace(newPasswordSalt);

        // Will be fixed in Authentication task.
        PasswordHash = newPasswordHash;
        PasswordSalt = newPasswordSalt;

        UpdateTimestamp();
    }

    /// <summary>
    /// Adds a new session to the user's history.
    /// </summary>
    /// <param name="session">The session to add.</param>
    public void AddSession(UserSession session)
    {
        ArgumentNullException.ThrowIfNull(session);

        if (session.UserId != Id)
            throw new ArgumentException("Session does not belong to this user.", nameof(session));

        _sessions.Add(session);
    }

    /// <summary>
    /// Logs a new hydration record for the user.
    /// </summary>
    /// <param name="record">The hydration record to add.</param>
    /// <remarks>
    /// This method allows the User aggregate to maintain invariants related to hydration limits or achievements in the future.
    /// </remarks>
    public void LogHydration(HydrationRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        if(record.UserId != Id)
            throw new ArgumentException("Hydration record does not belong to this user.", nameof(record));

        _hydrationRecords.Add(record);
        UpdateTimestamp();
    }

    /// <summary>
    /// Updates the LastModified timestamp to current UTC time.
    /// </summary>
    private void UpdateTimestamp() => UpdatedAt = DateTime.UtcNow;

    #endregion
}