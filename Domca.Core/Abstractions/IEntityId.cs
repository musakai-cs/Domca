namespace Domca.Core.Abstractions;

/// <summary>
/// Defines a strongly-typed identifier for an entity, providing access to its underlying value and utility methods for
/// creation and representation.
/// </summary>
/// <typeparam name="T">The type of the underlying value used to represent the entity identifier.</typeparam>
public interface IEntityId<T>
{
    /// <summary>
    /// Creates a new string instance using a default value or implementation.
    /// </summary>
    /// <returns>A string representing the newly created value. The specific contents depend on the implementation.</returns>
    static abstract T New();

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    string ToString();
}
