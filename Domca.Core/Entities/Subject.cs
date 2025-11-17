using Domca.Core.Entities.IDs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a subject entity used to identify or describe an object within a domain.
/// </summary>
public sealed class Subject
{
    /// <summary>
    /// Gets or sets the unique identifier for the subject.
    /// </summary>
    public SubjectId Id { get; set; } = SubjectId.New();

    /// <summary>
    /// Gets or sets the name associated with the object.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the associated teacher.
    /// </summary>
    public TeacherId TeacherId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the associated school year.
    /// </summary>
    public SchoolYearId SchoolYearId { get; set; }

    /// <summary>
    /// Gets the weighted average of all marks, calculated using their assigned weights.
    /// </summary>
    /// <remarks>If there are no marks, the weighted average is 0.0. The calculation uses the sum of each
    /// mark's value multiplied by its weight, divided by the total weight of all marks.</remarks>
    public double WeightedAverage => Marks.Any()
        ? Marks.Sum(m => m.Value * m.Weight) / Marks.Sum(m => m.Weight)
        : 0.0;

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets or sets the teacher associated with this entity.
    /// </summary>
    public Teacher Teacher { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of marks associated with the current entity.
    /// </summary>
    public ICollection<Mark> Marks { get; set; } = new List<Mark>();

    /// <summary>
    /// Gets or sets the school year associated with this instance.
    /// </summary>
    public SchoolYear SchoolYear { get; set; } = null!;

    #endregion
}
