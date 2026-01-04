using Domca.Core.Entities;
using Domca.Core.Entities.IDs;

namespace Domca.Core.Repositories;

/// <summary>
/// Defines a contract for a repository that manages user session entities.
/// </summary>
/// <remarks>This interface provides methods for adding, retrieving, and removing user sessions.
/// Implementations should ensure thread safety and handle data persistence operations appropriately.</remarks>
public interface IUserSessionRepository
{
    // Read Methods
    #region Read Methods

    /// <summary>
    /// Asynchronously retrieves a user session by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user session to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user session associated with the
    /// specified identifier, or <see langword="null"/> if no session is found.</returns>
    Task<UserSession?> GetByIdAsync(UserSessionId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a user session by its authentication token.
    /// </summary>
    /// <param name="token">The authentication token of the session to retrieve. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user session associated with the
    /// specified token, or <see langword="null"/> if no session is found.</returns>
    Task<UserSession?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves all active (non-expired) sessions for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose active sessions are to be retrieved.</param>
    /// <param name="utcNow">The current UTC time, used to filter out expired sessions.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of active user sessions for the
    /// specified user. The list is empty if no active sessions are found.</returns>
    Task<List<UserSession>?> GetActiveByUserIdAsync(UserId userId, DateTime? utcNow, CancellationToken cancellationToken = default);

    #endregion

    // Write Methods
    #region Write Methods

    /// <summary>
    /// Adds the specified user session to the collection.
    /// </summary>
    /// <param name="userSession">The user session to add.</param>
    void Add(UserSession userSession);

    /// <summary>
    /// Removes the specified user session from the database. Changes are not persisted until SaveChanges is called on the context.
    /// </summary>
    /// <param name="userSession">The user session to mark for removal from the database.</param>
    void Remove(UserSession userSession);

    /// <summary>
    /// Removes a collection of user sessions from the database.
    /// </summary>
    /// <remarks>Useful for revoking all sessions for a user (e.g., global logout).</remarks>
    /// <param name="userSessions">The collection of user sessions to remove.</param>
    void RemoveRange(List<UserSession> userSessions);

    #endregion
}