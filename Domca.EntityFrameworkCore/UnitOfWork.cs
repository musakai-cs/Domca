using Domca.Core.Abstractions;

namespace Domca.EntityFrameworkCore;

/// <summary>
/// EF Core implementation of the Unit of Work pattern.
/// </summary>
public sealed class UnitOfWork(DataContext context) : IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves all changes made in the context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the database.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}