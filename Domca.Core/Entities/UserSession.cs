using Domca.Core.Entities.IDs;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a user session containing authentication and session details.
/// </summary>
/// <remarks>This class is used to manage user sessions, providing information such as the session's unique
/// identifier, authentication token, and associated user details. It also tracks the session's creation and expiration
/// times.</remarks>
public sealed class UserSession
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public UserSessionId Id { get; set; }

    /// <summary>
    /// Gets or sets the authentication token used for accessing secured resources.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the expiration date and time for the current item.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets or sets the associated user entity.
    /// </summary>
    public User? User { get; set; }

    #endregion
}
