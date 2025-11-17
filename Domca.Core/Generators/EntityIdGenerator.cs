using System.Security.Cryptography;

namespace Domca.Core.Generators;

/// <summary>
/// Provides helper methods for generating unique identifier strings with customizable prefixes and random hexadecimal
/// suffixes.
/// </summary>
/// <remarks>This class is intended for use in scenarios where non-sequential, hard-to-guess identifiers are
/// needed, such as temporary resource names or tokens. All methods are static and thread-safe.</remarks>
public static class EntityIdGenerator
{

    /// <summary>
    /// Generates a unique identifier string by combining the specified prefix with a random hexadecimal suffix.
    /// </summary>
    /// <remarks>The generated identifier is suitable for scenarios where a non-sequential, hard-to-guess ID
    /// is required. The randomness is provided by a cryptographically secure random number generator. The method does
    /// not guarantee global uniqueness, but the probability of collision is extremely low for typical values of
    /// <paramref name="length"/>.</remarks>
    /// <param name="prefix">The string to prepend to the generated identifier. Typically used to indicate the type or source of the ID.</param>
    /// <param name="length">The number of random bytes to use for the hexadecimal suffix. Must be greater than zero. The default is 10.</param>
    /// <returns>A string consisting of the specified prefix followed by a random hexadecimal string of twice the specified
    /// length.</returns>
    public static string Generate(string prefix, int length = 10)
    {
        if (string.IsNullOrWhiteSpace(prefix))
            throw new ArgumentException("Prefix must not be null, empty, or consist only of whitespace.", nameof(prefix));

        var bytes = RandomNumberGenerator.GetBytes(length);
        var suffix = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return $"{prefix}{suffix}";
    }
}
