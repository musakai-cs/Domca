using Domca.Core.Generators;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents a strongly-typed identifier for a subject entity.
/// </summary>
/// <remarks>Use <see cref="SubjectId"/> to uniquely identify subject entities within the system. Instances are
/// immutable and can be compared for equality. The identifier value is expected to follow a specific format, typically
/// starting with the "SUB" prefix.</remarks>
/// <param name="Value">The unique identifier value for the subject. Must be a non-empty string prefixed with "SUB".</param>
public readonly record struct SubjectId(string Value) : IEntityId<SubjectId>
{
    private const string Prefix = "SUB";

    /// <summary>
    /// Creates a new unique <see cref="SubjectId"/> identifier.
    /// </summary>
    /// <returns>A <see cref="SubjectId"/> representing a newly generated unique identifier.</returns>
    public static SubjectId New()
        => new SubjectId(EntityIdGenerator.Generate(Prefix));

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
