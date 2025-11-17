using Domca.Core.Helpers;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents a strongly-typed identifier for a user entity.
/// </summary>
/// <param name="Value">The string value that uniquely identifies the user. Must be non-null and conform to the expected identifier format.</param>
public readonly struct UserId(string Value) : IEntityId<UserId>
{
    private const string Prefix = "USR";

    /// <summary>
    /// Creates a new, unique instance of the <see cref="UserId"/> class.
    /// </summary>
    /// <remarks>Use this method when a new hydratation record needs a unique identifier. The generated ID is
    /// suitable for use as a primary key or for tracking individual records.</remarks>
    /// <returns>A <see cref="HydratationRecordId"/> representing a newly generated identifier. Each call returns a distinct
    /// value.</returns>
    public static UserId New()
    {
        var suffix = IdGeneratorHelper.GenerateSuffix();
        return new UserId($"{Prefix}{suffix}");
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
