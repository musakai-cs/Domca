using Domca.Core.Entities;

namespace Domca.Core.Repositories;

/// <summary>
/// Defines a contract for a repository that manages user entities.
/// </summary>
/// <remarks>This interface provides methods for adding, retrieving, updating, and deleting user entities from a
/// data store. Implementations should ensure thread safety and handle any data access exceptions
/// appropriately.</remarks>
public interface IUserRepository
{
    /// <summary>
    /// Asynchronously retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user with the specified
    /// identifier, or <see langword="null"/> if no user is found.</returns>
    Task<User?> GetByIdAsync(Guid id);
    /// <summary>
    /// Asynchronously retrieves all users from the data store.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of
    /// users.</returns>
    Task<IEnumerable<User>> GetAllAsync();
    /// <summary>
    /// Asynchronously creates a new user in the data store.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    Task CreateAsync(User user);
    /// <summary>
    /// Asynchronously updates an existing user in the data store.
    /// </summary>
    /// <param name="user">The user entity with updated values.</param>
    Task UpdateAsync(User user);
    /// <summary>
    /// Asynchronously deletes a user from the data store.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    Task DeleteAsync(Guid id);
}
