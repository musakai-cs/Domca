namespace Domca.Core.Abstractions;

/// <summary>
/// Defines a contract for committing changes to a data store as a single unit of work.
/// </summary>
/// <remarks>Implementations of this interface coordinate the saving of changes across multiple repositories or
/// contexts, ensuring that all updates are persisted together. This pattern is commonly used to maintain transactional
/// consistency in data operations.</remarks>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves all changes made in the current context to the underlying data store.
    /// </summary>
    /// <remarks>If the cancellation token is triggered before the operation completes, the returned task will
    /// be canceled. This method does not guarantee that all changes are persisted if an error occurs during the save
    /// process.</remarks>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation. The default value is <see
    /// cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
