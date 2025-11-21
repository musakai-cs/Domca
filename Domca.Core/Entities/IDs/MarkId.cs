using Domca.Core.Generators;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents the unique identifier for a hydration record.
/// </summary>
/// <remarks>Use this type to reference or distinguish individual hydration records within the system. Instances
/// of this class are typically used as keys or identifiers in data operations involving hydration records.</remarks>
public readonly record struct MarkId(string Value) : IEntityId<MarkId>
{
    private const string Prefix = "MARK";

    /// <summary>
    /// Creates a new unique instance of the <see cref="MarkId"/> type.
    /// </summary>
    /// <remarks>Each call to this method returns a distinct <see cref="MarkId"/> value. This method is
    /// thread-safe and suitable for generating identifiers in concurrent scenarios.</remarks>
    /// <returns>A <see cref="MarkId"/> representing a newly generated unique identifier.</returns>
    public static MarkId New()
            => new MarkId(EntityIdGenerator.Generate(Prefix));

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
