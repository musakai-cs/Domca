using Domca.EntityFrameworkCore.Configurations;
using Domca.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore;

/// <summary>
/// Represents a session with the database, allowing for querying and saving instances of entities.
/// </summary>
/// <remarks>This class is a sealed implementation of <see cref="DbContext"/> and is configured using the provided
/// <see cref="DbContextOptions{DataContext}"/>. It is typically used to interact with the database in a strongly-typed
/// manner, leveraging the Entity Framework Core ORM.</remarks>
public sealed class DataContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext"/> class using the specified options.
    /// </summary>
    /// <remarks>The <paramref name="options"/> parameter allows configuration of the database context, such
    /// as the connection string and database provider.</remarks>
    /// <param name="options">The options to be used by the <see cref="DataContext"/>. This parameter is required and cannot be null.</param>
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the collection of users in the database context.
    /// </summary>
    /// <remarks>This property provides access to query, add, update, or remove user entities within the
    /// database. Changes made to the collection are tracked by the context and persisted to the database when
    /// SaveChanges is called.</remarks>
    public DbSet<User> Users { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of user session entities for querying and saving.
    /// </summary>
    /// <remarks>Use this property to access, add, update, or remove user session records in the database
    /// through Entity Framework Core. Changes made to the collection are tracked and persisted when SaveChanges is
    /// called.</remarks>
    public DbSet<UserSession> UserSessions { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of hydration records in the database.
    /// </summary>
    /// <remarks>This property provides access to query, add, update, or remove hydration records using Entity
    /// Framework Core. Changes made to the collection are tracked and persisted to the database when SaveChanges is
    /// called.</remarks>
    public DbSet<HydratationRecord> HydratationRecords { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of teacher entities in the database.
    /// </summary>
    /// <remarks>
    /// Use this property to query, add, update, or remove teacher records in the database
    /// through Entity Framework Core. Changes made to the collection are tracked and
    /// persisted when SaveChanges is called.
    /// </remarks>
    public DbSet<Teacher> Teachers { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of subject entities in the database.
    /// </summary>
    /// <remarks>
    /// This property provides access to query, add, update, or remove subject records
    /// using Entity Framework Core. Changes made to the collection are tracked and
    /// persisted to the database when SaveChanges is called.
    /// </remarks>
    public DbSet<Subject> Subjects { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of school year entities in the database.
    /// </summary>
    /// <remarks>
    /// Use this property to query, add, update, or remove school year records in the
    /// database through Entity Framework Core. Changes made to the collection are tracked
    /// and persisted when SaveChanges is called.
    /// </remarks>
    public DbSet<SchoolYear> SchoolYears { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of mark entities in the database.
    /// </summary>
    /// <remarks>
    /// This property provides access to query, add, update, or remove mark records
    /// using Entity Framework Core. Changes made to the collection are tracked and
    /// persisted to the database when SaveChanges is called.
    /// </remarks>
    public DbSet<Mark> Marks { get; set; } = default!;

    /// <summary>
    /// Configures the model that is used by this context.
    /// </summary>
    /// <remarks>This method is called when the model for a derived context has been initialized, but before
    /// the model has been locked down and used to initialize the context. The default implementation of this method
    /// does nothing, but it can be overridden in a derived class to customize the model.</remarks>
    /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
        modelBuilder.ApplyConfiguration(new HydratationRecordConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        modelBuilder.ApplyConfiguration(new SchoolYearConfiguration());
        modelBuilder.ApplyConfiguration(new MarkConfiguration());
    }
}
