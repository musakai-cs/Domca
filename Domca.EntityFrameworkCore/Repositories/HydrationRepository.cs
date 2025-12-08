using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Domca.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore.Repositories;

/// <summary>
/// Provides access to hydration-related data operations using the specified data context.
/// </summary>
/// <param name="context">The data context used to interact with the underlying hydration data store. Cannot be null.</param>
public class HydrationRepository(context context) : IHydrationRepository
{
    /// <summary>
    /// Asynchronously retrieves a hydration record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hydration record to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record if found;
    /// otherwise, null.</returns>
    public async Task<HydrationRecord?> GetById(HydrationRecordId id, CancellationToken cancellationToken = default)
        => await context.HydrationRecords.FindAsync([id], cancellationToken);

    /// <summary>
    /// Retrieves all hydration records associated with the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A list of hydration records for the specified user. The list is empty if the user has no hydration records.</returns>
    public async Task<List<HydrationRecord>?> GetByUser(UserId id, CancellationToken cancellationToken = default)
        => await context.HydrationRecords
            .Where(h => h.UserId == id)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves all hydration records for the specified user that were created on the current UTC date.
    /// </summary>
    /// <remarks>The method compares record dates using UTC time. If called near midnight UTC, results may
    /// differ from local time expectations.</remarks>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A list of hydration records for the specified user for today, or <see langword="null"/> if no records are found.</returns>
    public async Task<List<HydrationRecord>?> GetByUserForToday(UserId id, CancellationToken cancellationToken = default)
    => await context.HydrationRecords
        .Where(h => h.UserId == id
                && h.Date.Date == DateTime.UtcNow.Date)
        .ToListAsync(cancellationToken);

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
    public async Task<List<HydrationRecord>?> GetByUserForWeek(UserId id, DateTime referenceDate, CancellationToken cancellationToken = default)
    {
        int diff = (7 + (referenceDate.DayOfWeek - DayOfWeek.Monday)) % 7;

        var startOfWeek = referenceDate.Date.AddDays(-1 * diff);

        var endOfWeek = startOfWeek.AddDays(7);

        return await context.HydrationRecords
            .Where(h => h.UserId == id
                        && h.Date >= startOfWeek 
                        && h.Date < endOfWeek)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all hydration records for the specified user within the given month and year.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="month">The month for which to retrieve records, specified as an integer from 1 (January) to 12 (December).</param>
    /// <param name="year">The year for which to retrieve records.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of hydration records for the
    /// specified user and month, or <see langword="null"/> if no records are found.</returns>
    public async Task<List<HydrationRecord>?> GetByUserForMonth(UserId id, int month, int year, CancellationToken cancellationToken = default)
    => await context.HydrationRecords
        .Where(h => h.UserId == id
                && h.Date.Month == month
                && h.Date.Year == year)
        .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves all hydration records for the specified user and year.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose hydration records are to be retrieved.</param>
    /// <param name="year">The year for which to retrieve hydration records. Must be a four-digit year.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A list of hydration records for the specified user and year, or <see langword="null"/> if no records are found.</returns>
    public async Task<List<HydrationRecord>?> GetByUserForYear(UserId id, int year, CancellationToken cancellationToken = default)
        => await context.HydrationRecords
            .Where(h => h.UserId == id
                    && h.Date.Year == year)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Adds a hydration record to the data context for tracking and persistence.
    /// </summary>
    /// <remarks>The record is not saved to the database until changes are committed. This method does not
    /// check for duplicate records.</remarks>
    /// <param name="hydrationRecord">The hydration record to add to the context. Cannot be null.</param>
    public void Add(HydrationRecord hydrationRecord) => context.HydrationRecords.Add(hydrationRecord);

    /// <summary>
    /// Adds a collection of hydration records to the data context for insertion.
    /// </summary>
    /// <remarks>This method stages the specified hydration records for addition to the underlying data store.
    /// Changes are not persisted until the context is saved. If any record in the collection already exists in the
    /// context, it may result in duplicate entries unless handled appropriately.</remarks>
    /// <param name="hydrationRecords">The collection of <see cref="HydrationRecord"/> objects to add. Cannot be null.</param>
    public void AddRange(List<HydrationRecord> hydrationRecords) => context.HydrationRecords.AddRange(hydrationRecords);

    /// <summary>
    /// Updates the specified hydration record in the data store.
    /// </summary>
    /// <remarks>If the specified record does not exist in the data store, no changes will be made. This
    /// method does not save changes to the database; call SaveChanges to persist updates.</remarks>
    /// <param name="hydrationRecord">The hydration record to update. Must not be null.</param>
    public void Update(HydrationRecord hydrationRecord) => context.HydrationRecords.Update(hydrationRecord);

    /// <summary>
    /// Removes the specified hydration record from the data context.
    /// </summary>
    /// <param name="hydrationRecord">The hydration record to remove from the context. Cannot be null.</param>
    public void Remove(HydrationRecord hydrationRecord) => context.HydrationRecords.Remove(hydrationRecord);
}