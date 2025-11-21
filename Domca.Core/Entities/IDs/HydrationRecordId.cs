using Domca.Core.Generators;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents the unique identifier for a hydration record.
/// </summary>
/// <remarks>Use this type to reference or distinguish individual hydration records within the system. Instances
/// of this class are typically used as keys or identifiers in data operations involving hydration records.</remarks>
public readonly record struct HydrationRecordId(string Value) : IEntityId<HydrationRecordId>
{
    private const string Prefix = "HR";

    /// <summary>
    /// Creates a new <see cref="HydrationRecordId"/> instance using a default value or implementation.
    /// </summary>
    /// <returns>A new instance of <see cref="HydrationRecordId"/> with a unique identifier.</returns>
    public static HydrationRecordId New()
        => new HydrationRecordId(EntityIdGenerator.Generate(Prefix));

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
