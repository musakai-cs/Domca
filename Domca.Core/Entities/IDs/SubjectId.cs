using Domca.Core.Helpers;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents a strongly-typed identifier for a subject entity.
/// </summary>
/// <remarks>Use <see cref="SubjectId"/> to uniquely identify subject entities within the system. Instances are
/// immutable and can be compared for equality. The identifier value is expected to follow a specific format, typically
/// starting with the "SUB" prefix.</remarks>
/// <param name="Value">The unique identifier value for the subject. Must be a non-empty string prefixed with "SUB".</param>
public readonly struct SubjectId(string Value) : IEntityId<SubjectId>
{
    private const string Prefix = "SUB";

    /// <summary>
    /// Creates a new instance of the <see cref="SubjectId"/> class with a unique identifier value.
    /// </summary>
    /// <remarks>This method generates a unique identifier by combining a predefined prefix with a generated
    /// suffix. Each call returns a distinct value suitable for identifying subjects within the system.</remarks>
    /// <returns>A <see cref="SubjectId"/> containing a newly generated unique identifier.</returns>
    public static SubjectId New()
    {
        var suffix = IdGeneratorHelper.GenerateSuffix();
        return new SubjectId($"{Prefix}{suffix}");
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
