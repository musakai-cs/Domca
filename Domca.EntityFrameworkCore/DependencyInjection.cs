using Domca.Core.Repositories;
using Domca.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domca.EntityFrameworkCore;

/// <summary>
/// Provides extension methods for registering persistence-related services and repositories with the dependency
/// injection container.
/// </summary>
/// <remarks>This class is intended to be used during application startup to configure database contexts and
/// repository dependencies. All methods are static and should be called on an instance of <see
/// cref="IServiceCollection"/>.</remarks>
public static class DependencyInjection
{
    /// <summary>
    /// Adds persistence-related services, including the database context and repositories, to the specified service
    /// collection using the provided SQL Server connection string.
    /// </summary>
    /// <remarks>This method registers the application's data context and repository services for dependency
    /// injection. Call this method during application startup to enable data access features.</remarks>
    /// <param name="services">The service collection to which the persistence services will be added.</param>
    /// <param name="connectionString">The connection string used to configure the SQL Server database context. Cannot be null or empty.</param>
    /// <returns>The same service collection instance with persistence services registered.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));

        AddRepositories(services);

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}