using Domca.Core.Entities.IDs;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a collection of marks
/// </summary>
public sealed class Mark
{
    /// <summary>
    /// Gets or sets the unique identifier for this mark instance.
    /// </summary>
    public MarkId Id { get; set; } = MarkId.New();

    /// <summary>
    /// Gets or sets the integer value associated with this instance.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Gets or sets the weight value associated with the object.
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related subject entity.
    /// </summary>
    public SubjectId SubjectId { get; set; }

    // Navigation Properties
    #region Navigation Properties

    /// <summary>
    /// Gets or sets the subject associated with the current context.
    /// </summary>
    public Subject Subject { get; set; } = null!;

    #endregion
}
