using Domca.Core.Repositories;
using Domca.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domca.EntityFrameworkCore.Repositories;

/// <summary>
/// Provides methods for managing user data in a database context.
/// </summary>
/// <remarks>The <see cref="UserRepository"/> class offers asynchronous operations to create, retrieve, update,
/// and delete user entities. It utilizes Entity Framework Core to interact with the database and includes related
/// session data where applicable. Ensure that the <see cref="DataContext"/> is properly configured for asynchronous
/// operations before using this repository.</remarks>
/// <param name="context"></param>
public class UserRepository(DataContext context) : IUserRepository
{
    private readonly DataContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Asynchronously retrieves a user by their unique identifier.
    /// </summary>
    /// <remarks>This method includes related session data in the result. Ensure that the database context is
    /// properly configured to handle asynchronous operations.</remarks>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user with the specified
    /// identifier, or <see langword="null"/> if no user is found.</returns>
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Sessions)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    /// Asynchronously retrieves all users from the database, including their associated sessions.
    /// </summary>
    /// <remarks>This method uses Entity Framework Core to query the database and includes related session
    /// data  for each user. Ensure that the database context is properly configured and connected before  calling this
    /// method.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of User objects,
    /// each with their associated sessions.</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Sessions)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously creates a new user in the database.
    /// </summary>
    /// <remarks>The user's email address is normalized before saving, and the creation and update timestamps
    /// are set to the current UTC time.</remarks>
    /// <param name="user">The user entity to be created. The <paramref name="user"/> object must have a valid email address.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CreateAsync(User user)
    {
        user.EmailAddressNormalized = NormalizeEmail(user.EmailAddress);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the specified user's information in the database asynchronously.
    /// </summary>
    /// <param name="user">The user entity containing updated information. The user's email address will be normalized, and the update
    /// timestamp will be set to the current UTC time.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    public async Task UpdateAsync(User user)
    {
        user.EmailAddressNormalized = NormalizeEmail(user.EmailAddress);
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes a user with the specified identifier from the database.
    /// </summary>
    /// <remarks>If the user with the specified <paramref name="id"/> does not exist, no action is
    /// taken.</remarks>
    /// <param name="id">The unique identifier of the user to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Normalizes the specified email address by trimming whitespace and converting it to uppercase.
    /// </summary>
    /// <param name="email">The email address to normalize. Cannot be null or empty.</param>
    /// <returns>The normalized email address in uppercase with no leading or trailing whitespace.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="email"/> is null or empty.</exception>
    private static string NormalizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }
        return email.Trim().ToUpperInvariant();
    }
}
