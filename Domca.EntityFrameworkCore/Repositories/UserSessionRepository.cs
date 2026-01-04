using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Domca.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore.Repositories;

/// <summary>
/// Provides methods for querying and managing user session entities in the application's data store.
/// </summary>
/// <remarks>This class is sealed and cannot be inherited. All operations are performed against the provided data
/// context, and changes to session entities are not persisted until the context is saved.</remarks>
/// <param name="context">The data context used to access and persist user session information.</param>
public sealed class UserSessionRepository(DataContext context) : IUserSessionRepository
{
    // Read Methods
    #region Read Methods

    /// <summary>
    /// Asynchronously retrieves a user session entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the session to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user session entity if found; otherwise,
    /// <see langword="null"/>.</returns>
    public async Task<UserSession?> GetByIdAsync(UserSessionId id, CancellationToken cancellationToken = default)
        => await context.UserSessions.FindAsync([id], cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a user session associated with the specified authentication token.
    /// </summary>
    /// <param name="token">The authentication token to search for.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user session matching the
    /// specified token, or <see langword="null"/> if not found.</returns>
    public async Task<UserSession?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        => await context.UserSessions
            .FirstOrDefaultAsync(s => s.Token == token, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves all active sessions for the specified user where the expiration date is in the future.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of active sessions for the
    /// specified user, or <see langword="null"/> if no records are found.</returns>
    public async Task<List<UserSession>?> GetActiveByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
        => await context.UserSessions
            .Where(s => s.UserId == userId && s.ExpiresAt > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

    #endregion

    // Write Methods
    #region Write Methods

    /// <summary>
    /// Adds the specified user session to the data context.
    /// </summary>
    /// <remarks>Changes are not persisted to the database until SaveChanges is called on the context.</remarks>
    /// <param name="userSession">The user session entity to add.</param>
    public void Add(UserSession userSession) => context.UserSessions.Add(userSession);

    /// <summary>
    /// Removes the specified user session from the data context.
    /// </summary>
    /// <remarks>Changes are not persisted to the database until SaveChanges is called on the context.</remarks>
    /// <param name="userSession">The user session entity to remove.</param>
    public void Remove(UserSession userSession) => context.UserSessions.Remove(userSession);

    /// <summary>
    /// Removes a collection of user sessions from the data context.
    /// </summary>
    /// <remarks>Changes are not persisted to the database until SaveChanges is called on the context.</remarks>
    /// <param name="userSessions">The collection of user session entities to remove.</param>
    public void RemoveRange(List<UserSession> userSessions) => context.UserSessions.RemoveRange(userSessions);

    #endregion
}