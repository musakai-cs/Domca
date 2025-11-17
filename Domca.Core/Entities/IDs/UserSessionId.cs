using Domca.Core.Helpers;
using Domca.Core.Interfaces;

namespace Domca.Core.Entities.IDs;

/// <summary>
/// Represents a unique identifier for a user session.
/// </summary>
/// <remarks>UserSessionId instances are used to track and reference individual user sessions throughout the
/// application. Each identifier is guaranteed to be unique within the application's context.</remarks>
/// <param name="Value">The string value that uniquely identifies the user session. Must be non-null and conform to the expected session
/// identifier format.</param>
public readonly struct UserSessionId(string Value) : IEntityId<UserSessionId>
{
    private const string Prefix = "USESS";

    /// <summary>
    /// Creates a new, unique user session identifier.
    /// </summary>
    /// <remarks>Each call returns a distinct identifier suitable for tracking individual user sessions. The
    /// generated identifier is guaranteed to be unique within the application's context.</remarks>
    /// <returns>A <see cref="UserSessionId"/> instance representing a newly generated session identifier.</returns>
    public static UserSessionId New()
    {
        var suffix = IdGeneratorHelper.GenerateSuffix();
        return new UserSessionId($"{Prefix}{suffix}");
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => Value.ToString();
}
