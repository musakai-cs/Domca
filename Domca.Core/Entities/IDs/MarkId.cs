using Domca.Core.Helpers;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents the unique identifier for a hydration record.
/// </summary>
/// <remarks>Use this type to reference or distinguish individual hydration records within the system. Instances
/// of this class are typically used as keys or identifiers in data operations involving hydration records.</remarks>
public readonly struct MarkId(string Value) : IEntityId<MarkId>
{
    private const string Prefix = "MARK";

    /// <summary>
    /// Creates a new instance of the <see cref="MarkId"/> type with a unique identifier value.
    /// </summary>
    /// <remarks>This method guarantees that each returned <see cref="MarkId"/> is unique within the
    /// application's context. Use this method when a distinct identifier is required for marking or tracking
    /// purposes.</remarks>
    /// <returns>A <see cref="MarkId"/> containing a newly generated unique identifier.</returns>
    public static MarkId New()
    {
        var suffix = IdGeneratorHelper.GenerateSuffix();
        return new MarkId($"{Prefix}{suffix}");
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
