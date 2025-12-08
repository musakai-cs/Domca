using Domca.Core.Entities;
using Domca.Core.Entities.IDs;
using Domca.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore.Repositories;

/// <summary>
/// Provides access to hydration-related data operations using the specified data context.
/// </summary>
/// <param name="dataContext">The data context used to interact with the underlying hydration data store. Cannot be null.</param>
public class HydrationRepository(DataContext dataContext) : IHydrationRepository
{
    /// <summary>
    /// Asynchronously retrieves a hydration record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hydration record to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hydration record if found;
    /// otherwise, null.</returns>
    public async Task<HydrationRecord?> GetById(HydrationRecordId id, CancellationToken cancellationToken = default)
        => await dataContext.HydrationRecords.FindAsync([id], cancellationToken);

    public List<HydrationRecord?> GetByUser(UserId id, CancellationToken cancellationToken = default)
        => dataContext.HydrationRecords.Where(h => h.UserId == id).ToList();

    public Task<HydrationRecord?> GetByUserForMonth(UserId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<HydrationRecord?> GetByUserForToday(UserId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<HydrationRecord?> GetByUserForWeek(UserId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<HydrationRecord?> GetByUserForYear(UserId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Add(HydrationRecord hydrationRecord)
    {
        throw new NotImplementedException();
    }

    public void Update(HydrationRecord hydrationRecord)
    {
        throw new NotImplementedException();
    }

    public void Remove(HydrationRecord hydrationRecord)
    {
        throw new NotImplementedException();
    }
}