using Domca.EntityFrameworkCore.Configurations;
using Domca.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore;

/// <summary>
/// Represents the Entity Framework Core database context for managing users, user sessions, and hydration records.
/// </summary>
/// <remarks>Use this context to query and persist user, session, and hydration data within the application's
/// database. The context tracks changes to entities and saves them to the database when SaveChanges is called. This
/// class should be registered with dependency injection and disposed appropriately to manage database connections and
/// resources.</remarks>
/// <param name="options">The options to be used by the database context, including configuration such as the database provider, connection
/// string, and other context behaviors.</param>
public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
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
    public DbSet<HydrationRecord> HydrationRecords { get; set; } = default!;

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
        modelBuilder.ApplyConfiguration(new HydrationRecordConfiguration());
    }
}
