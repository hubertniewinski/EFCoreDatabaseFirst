using EFCoreDatabaseFirst.Repositories.Abstractions;
using EFCoreDatabaseFirst.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirst.Repositories;

public class TripRepository(ApbdContext context) : ITripRepository
{
    public Task<bool> AnyAsync(int idTrip, CancellationToken cancellationToken) 
        => context.Trips.AnyAsync(x => x.IdTrip == idTrip, cancellationToken);

    public async Task<IEnumerable<Trip>> GetTripsAsync(CancellationToken cancellationToken) 
        => await context.Trips
            .Include(x => x.IdCountries)
            .Include(x => x.ClientTrips)
            .ThenInclude(x => x.IdClientNavigation)
            .OrderByDescending(x => x.DateFrom)
            .ToListAsync(cancellationToken);
}