using Domca.Core.Entities;
using Domca.Core.Entities.IDs;

namespace Domca.Core.Repositories;

/// <summary>
/// Defines a contract for managing and retrieving user hydration data within a repository.
/// </summary>
/// <remarks>This interface provides asynchronous methods for querying user information and synchronous methods
/// for adding, updating, and removing user records. Implementations are expected to handle data persistence and ensure
/// thread safety where appropriate. Methods that accept a <see cref="CancellationToken"/> allow callers to cancel
/// ongoing operations, which is useful for long-running queries or when integrating with responsive
/// applications.</remarks>
public interface IHydrationRepository
{
    // Read Methods
    #region Read Methods

    /// <summary>
    /// Retrieves a hydration record by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the hydration record to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record if found;
    /// otherwise, <see langword="null"/>.</returns>
    Task<HydrationRecord?> GetById(HydrationRecordId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the hydration record for the specified user for the current day, if one exists.
    /// </summary>
    /// <param name="id">The identifier of the user whose hydration record is to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record for the user
    /// for today, or null if no record exists.</returns>
    Task<HydrationRecord?> GetByUserForToday(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the hydration record for the specified user for the current week, if available.
    /// </summary>
    /// <param name="id">The identifier of the user whose hydration record is to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record for the user
    /// for the current week, or <see langword="null"/> if no record exists.</returns>
    Task<HydrationRecord?> GetByUserForWeek(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the hydration record for the specified user for the current month, if available.
    /// </summary>
    /// <param name="id">The identifier of the user whose hydration record is to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record for the user
    /// for the current month, or <see langword="null"/> if no record exists.</returns>
    Task<HydrationRecord?> GetByUserForMonth(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the hydration record for the specified user and year asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the user whose hydration record is to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record for the user
    /// and year if found; otherwise, null.</returns>
    Task<HydrationRecord?> GetByUserForYear(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the hydration record associated with the specified user, if one exists.
    /// </summary>
    /// <param name="id">The identifier of the user whose hydration record is to be retrieved. Cannot be null.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record for the
    /// specified user, or <c>null</c> if no record is found.</returns>
    Task<HydrationRecord?> GetByUser(UserId id, CancellationToken cancellationToken = default);

    #endregion

    // Write Methods
    #region Write Methods

    /// <summary>
    /// Adds a new hydration record to the collection.
    /// </summary>
    /// <param name="hydrationRecord">The hydration record to add. Cannot be null.</param>
    void Add(HydrationRecord hydrationRecord);

    /// <summary>
    /// Updates the specified hydration record with new data.
    /// </summary>
    /// <param name="hydrationRecord">The hydration record to update. Cannot be null.</param>
    void Update(HydrationRecord hydrationRecord);

    /// <summary>
    /// Removes the specified hydration record from the collection.
    /// </summary>
    /// <param name="hydrationRecord">The hydration record to remove. Cannot be null.</param>
    void Remove(HydrationRecord hydrationRecord);

    #endregion
}
