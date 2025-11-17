using System.Security.Cryptography;

namespace Domca.Core.Helpers;

/// <summary>
/// 
/// </summary>
public static class IdGeneratorHelper
{
    /// <summary>
    /// Generates a random lowercase hexadecimal string of the specified length.
    /// </summary>
    /// <remarks>This method is useful for creating unique suffixes for identifiers, filenames, or tokens. The
    /// randomness is cryptographically secure, making the suffix suitable for scenarios where unpredictability is
    /// important.</remarks>
    /// <param name="length">The number of bytes to use for generating the suffix. Must be greater than zero. The resulting string will be
    /// twice this length in characters.</param>
    /// <returns>A lowercase hexadecimal string representing random data. The string will contain 2 × length characters.</returns>
    public static string GenerateSuffix(int length = 10)
    {
        var bytes = RandomNumberGenerator.GetBytes(length);
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}
