using Domca.Core.Entities;
using Domca.Core.Entities.IDs;

namespace Domca.Core.Repositories;

/// <summary>
/// Defines a contract for managing and retrieving hydration records within a repository.
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
    /// Asynchronously retrieves a hydration record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hydration record to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record if found;
    /// otherwise, null.</returns>
    Task<HydrationRecord?> GetByIdAsync(HydrationRecordId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the hydration record associated with the specified user, if one exists.
    /// </summary>
    /// <param name="id">The identifier of the user whose hydration record is to be retrieved. Cannot be null.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record for the
    /// specified user, or <c>null</c> if no record is found.</returns>
    Task<List<HydrationRecord>?> GetByUserAsync(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all hydration records associated with the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A list of hydration records for the specified user. The list is empty if the user has no hydration records.</returns>
    Task<List<HydrationRecord>?> GetByUserForTodayAsync(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all hydration records for the specified user within the week containing the given reference date.
    /// </summary>
    /// <remarks>The week is defined as starting on Monday and ending before the following Monday, based on
    /// the provided reference date.</remarks>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="referenceDate">A date used to determine the target week. The method returns records from the week that includes this date,
    /// starting on Monday.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A list of hydration records for the specified user that fall within the week containing the reference date. The
    /// list is empty if no records are found.</returns>
    Task<List<HydrationRecord>?> GetByUserForWeek(UserId id, DateTime referenceDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all hydration records for the specified user within the given month and year.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="month">The month for which to retrieve records, specified as an integer from 1 (January) to 12 (December).</param>
    /// <param name="year">The year for which to retrieve records.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of hydration records for the
    /// specified user and month, or <see langword="null"/> if no records are found.</returns>
    Task<List<HydrationRecord>?> GetByUserForMonth(UserId id, int month, int year, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all hydration records for the specified user and year.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="year">The year for which to retrieve hydration records. Must be a four-digit year.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A list of hydration records for the specified user and year, or <see langword="null"/> if no records are found.</returns>
    Task<List<HydrationRecord>?> GetByUserForYear(UserId id, int year, CancellationToken cancellationToken = default);

    #endregion

    // Write Methods
    #region Write Methods

    /// <summary>
    /// Adds a hydration record to the data context for tracking and persistence.
    /// </summary>
    /// <remarks>The record is not saved to the database until changes are committed. This method does not
    /// check for duplicate records.</remarks>
    /// <param name="hydrationRecord">The hydration record to add to the context. Cannot be null.</param>
    void Add(HydrationRecord hydrationRecord);

    /// <summary>
    /// Adds a collection of hydration records to the data context for insertion.
    /// </summary>
    /// <remarks>This method stages the specified hydration records for addition to the underlying data store.
    /// Changes are not persisted until the context is saved. If any record in the collection already exists in the
    /// context, it may result in duplicate entries unless handled appropriately.</remarks>
    /// <param name="hydrationRecords">The collection of <see cref="HydrationRecord"/> objects to add. Cannot be null.</param>
    void AddRange(List<HydrationRecord> hydrationRecords);

    /// <summary>
    /// Updates the specified hydration record in the data store.
    /// </summary>
    /// <remarks>If the specified record does not exist in the data store, no changes will be made. This
    /// method does not save changes to the database; call SaveChanges to persist updates.</remarks>
    /// <param name="hydrationRecord">The hydration record to update. Must not be null.</param>
    void Update(HydrationRecord hydrationRecord);

    /// <summary>
    /// Removes the specified hydration record from the data context.
    /// </summary>
    /// <param name="hydrationRecord">The hydration record to remove from the context. Cannot be null.</param>
    void Remove(HydrationRecord hydrationRecord);

    #endregion
}
