using Domca.Core.Entities.IDs;
using System.ComponentModel.DataAnnotations;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a user with personal and authentication details.
/// </summary>
/// <remarks>This class encapsulates user information including identifiers, contact details, and authentication
/// data. It is designed to be used in systems that require user management and authentication
/// functionalities.</remarks>
public sealed class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public UserId Id { get; set; } = UserId.New();

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the normalized version of the email address.
    /// </summary>
    [EmailAddress]
    public string EmailAddressNormalized { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed representation of the user's password.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the salt value used for hashing the password.
    /// </summary>
    public string PasswordSalt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the user's avatar image.
    /// </summary>
    public Uri? AvatarUrl { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets or sets the collection of user sessions.
    /// </summary>
    public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();

    /// <summary>
    /// Gets or sets the collection of hydration records for the user.
    /// </summary>
    public ICollection<HydrationRecord> HydrationRecords { get; set; } = new List<HydrationRecord>();

    #endregion
}
