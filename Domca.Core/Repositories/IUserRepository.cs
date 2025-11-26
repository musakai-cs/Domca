using Domca.Core.Entities;
using Domca.Core.Entities.IDs;

namespace Domca.Core.Repositories;

/// <summary>
/// Defines a contract for a repository that manages user entities.
/// </summary>
/// <remarks>This interface provides methods for adding, retrieving, updating, and deleting user entities from a
/// data store. Implementations should ensure thread safety and handle any data access exceptions
/// appropriately.</remarks>
public interface IUserRepository
{
    // Read Methods
    #region Read Methods

    /// <summary>
    /// Asynchronously retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user associated with the
    /// specified identifier, or <see langword="null"/> if no user is found.</returns>
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user associated with the
    /// specified email address, or <see langword="null"/> if no user is found.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously determines whether the specified email address is not already associated with an existing user account.
    /// </summary>
    /// <param name="email">The email address to check for uniqueness. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the email
    /// address is unique; otherwise, <see langword="false"/>.</returns>
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    #endregion

    // Write Methods
    #region Write Methods

    /// <summary>
    /// Adds the specified user to the collection.
    /// </summary>
    /// <param name="user">The user to add.</param>
    void Add(User user);

    /// <summary>
    /// Updates the specified user with new information.
    /// </summary>
    /// <param name="user">The user object containing updated data.</param>
    void Update(User user);

    /// <summary>
    /// Marks the specified user for removal from the database. Changes are not persisted until SaveChanges is called on the context.
    /// </summary>
    /// <param name="user">The user to mark for removal from the database.</param>
    void Remove(User user);

    #endregion
}
