using EFCoreDatabaseFirst.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirst.Repositories;

public class ClientTripRepository(ApbdContext context) : IClientTripRepository
{
    public Task<bool> AnyAsync(int idClient, int idTrip, CancellationToken cancellationToken) 
        => context.ClientTrips.AnyAsync(x => x.IdClient == idClient && x.IdTrip == idTrip, cancellationToken);

    public async Task<bool> AnyByClientIdAsync(int idClient, CancellationToken cancellationToken)
        => await context.ClientTrips.AnyAsync(x => x.IdClient == idClient, cancellationToken);
}