namespace Domca.Core.Helpers;

/// <summary>
/// Provides utility methods for working with <see cref="DateTime"/> values, ensuring consistent handling of UTC and
/// local time representations.
/// </summary>
/// <remarks>Use this class to standardize <see cref="DateTime"/> values to UTC, which is recommended for storing
/// and comparing dates across different time zones. All methods are static and thread-safe.</remarks>
public static class DateHelper
{
    /// <summary>
    /// Ensures that the DateTime is always stored as UTC.
    /// </summary>
    public static DateTime EnsureUtc(DateTime value)
    {
        if (value.Kind == DateTimeKind.Utc)
            return value;

        if (value.Kind == DateTimeKind.Local)
            return value.ToUniversalTime();

        return DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}
