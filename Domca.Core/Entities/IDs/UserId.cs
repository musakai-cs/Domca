using Domca.Core.Abstractions;
using Domca.Core.Generators;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents a strongly-typed identifier for a user entity.
/// </summary>
/// <param name="Value">The string value that uniquely identifies the user. Must be non-null and conform to the expected identifier format.</param>
public readonly record struct UserId(string Value) : IEntityId<UserId>
{
    private const string Prefix = "USR";

    /// <summary>
    /// Creates a new unique <see cref="UserId"/> instance.
    /// </summary>
    /// <returns>A <see cref="UserId"/> representing a newly generated unique identifier.</returns>
    public static UserId New()
        => new UserId(EntityIdGenerator.Generate(Prefix));

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
