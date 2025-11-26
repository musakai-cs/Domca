using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Domca.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore.Repositories;

/// <summary>
/// Provides methods for querying and managing user entities in the application's data store.
/// </summary>
/// <remarks>This class is sealed and cannot be inherited. All operations are performed against the provided data
/// context, and changes to user entities are not persisted until the context is saved.</remarks>
/// <param name="context">The data context used to access and persist user information.</param>
public sealed class UserRepository(DataContext context) : IUserRepository
{
    // Read Methods
    #region Read Methods

    /// <summary>
    /// Asynchronously retrieves a user entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user entity if found; otherwise,
    /// <see langword="null"/>.</returns>
    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FindAsync([id], cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves a user whose normalized email address matches the specified value.
    /// </summary>
    /// <param name="email">The email address to search for. The comparison is case-insensitive and uses the normalized form of the email
    /// address.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user whose normalized email
    /// address matches the specified value, or <see langword="null"/> if no such user is found.</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.EmailAddressNormalized == email.ToUpperInvariant(), cancellationToken);
    }

    /// <summary>
    /// Asynchronously determines whether the specified email address is not already associated with an existing user.
    /// </summary>
    /// <remarks>The comparison is case-insensitive and uses the normalized form of the email address. This
    /// method does not reserve the email address; concurrent operations may affect uniqueness.</remarks>
    /// <param name="email">The email address to check for uniqueness. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the email
    /// address is unique; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return !await context.Users
            .AnyAsync(u => u.EmailAddressNormalized == email.ToUpperInvariant(), cancellationToken);
    }

    #endregion

    // Write Methods
    #region Write Methods

    /// <summary>
    /// Adds the specified user to the data context for insertion into the database.
    /// </summary>
    /// <remarks>Changes made to the user entity are not persisted to the database until SaveChanges is called
    /// on the context.</remarks>
    /// <param name="user">The user entity to add to the context.</param>
    public void Add(User user) => context.Users.Add(user);

    /// <summary>
    /// Updates the specified user entity in the data context.
    /// </summary>
    /// <remarks>Changes made to the user entity are not persisted to the database until SaveChanges is called
    /// on the context.</remarks>
    /// <param name="user">The user entity to update.</param>
    public void Update(User user) => context.Users.Update(user);

    /// <summary>
    /// Removes the specified user from the data context.
    /// </summary>
    /// <remarks>Changes made to the user entity are not persisted to the database until SaveChanges is called
    /// on the context.</remarks>
    /// <param name="user">The user to remove from the context.</param>
    public void Remove(User user) => context.Users.Remove(user);

    #endregion
}