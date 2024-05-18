
using EFCoreDatabaseFirst.Repositories.Models;

namespace EFCoreDatabaseFirst.Repositories.Abstractions;

public interface ITripRepository
{
    Task<bool> AnyAsync(int idTrip, CancellationToken cancellationToken = default);
    Task<IEnumerable<Trip>> GetTripsAsync(CancellationToken cancellationToken = default);
}