using Domca.Core.Generators;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents the unique identifier for a hydration record.
/// </summary>
/// <remarks>Use this type to reference or distinguish individual hydration records within the system. Instances
/// of this class are typically used as keys or identifiers in data operations involving hydration records.</remarks>
public readonly record struct SchoolYearId(string Value) : IEntityId<SchoolYearId>
{
    private const string Prefix = "SY";

    /// <summary>
    /// Creates a new unique instance of the <see cref="SchoolYearId"/> identifier.
    /// </summary>
    /// <returns>A <see cref="SchoolYearId"/> representing a newly generated unique identifier.</returns>
    public static SchoolYearId New()
            => new SchoolYearId(EntityIdGenerator.Generate(Prefix));

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
