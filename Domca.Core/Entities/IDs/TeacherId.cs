using Domca.Core.Generators;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents a strongly-typed unique identifier for a teacher entity.
/// </summary>
/// <param name="Value">The underlying string value of the teacher identifier. Must be a non-empty, unique string prefixed with "TCHR".</param>
public readonly record struct TeacherId(string Value) : IEntityId<TeacherId>
{
    private const string Prefix = "TCHR";

    /// <summary>
    /// Creates a new, unique instance of the TeacherId class.
    /// </summary>
    /// <returns>A TeacherId representing a newly generated unique identifier.</returns>
    public static TeacherId New()
        => new TeacherId(EntityIdGenerator.Generate(Prefix));

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}