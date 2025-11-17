using Domca.Core.Entities.IDs;

namespace Domca.Core.Entities;

/// <summary>
/// Represents a teacher with identifying information and a collection of subjects taught.
/// </summary>
/// <remarks>This class encapsulates the basic details of a teacher, including their unique identifier, name, and
/// the subjects they are assigned to. Instances of this class are typically used to manage teacher records within
/// educational systems.</remarks>
public sealed class Teacher
{
    /// <summary>
    /// Gets or sets the unique identifier for the teacher.
    /// </summary>
    public TeacherId Id { get; set; } = TeacherId.New();

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    // Navigation Properties
    #region Navigation Properties
    
    /// <summary>
    /// Gets or sets the collection of subjects associated with this entity.
    /// </summary>
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    #endregion
}