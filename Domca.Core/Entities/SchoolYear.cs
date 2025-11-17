using Domca.Core.Entities.IDs;

namespace Domca.Core.Entities;

/// <summary>
/// Represents an academic school year, including its start and end years, unique identifier, and associated subjects.
/// </summary>
/// <remarks>A school year typically spans two consecutive calendar years (for example, 2024/2025). The class
/// provides navigation to related subjects and exposes a formatted label for display purposes. This type is immutable
/// in its identity and is intended to be used as a domain entity within educational systems.</remarks>
public sealed class SchoolYear
{
    /// <summary>
    /// Gets or sets the unique identifier for the school year.
    /// </summary>
    public SchoolYearId Id { get; set; } = SchoolYearId.New();

    /// <summary>
    /// Gets or sets the starting year for the associated period or entity.
    /// </summary>
    public int StartYear { get; set; }

    /// <summary>
    /// Gets or sets the ending year for the period represented by this instance.
    /// </summary>
    public int EndYear { get; set; }

    /// <summary>
    /// Gets the label representing the range between the start year and end year, formatted as "StartYear/EndYear".
    /// </summary>
    public string Label => $"{StartYear}/{EndYear}";

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets or sets the collection of subjects associated with this entity.
    /// </summary>
    /// <remarks>Modifications to the returned collection will affect the subjects linked to this entity. The
    /// collection is initialized to an empty list by default.</remarks>
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    #endregion
}
