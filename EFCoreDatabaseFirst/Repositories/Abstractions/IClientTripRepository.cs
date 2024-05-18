namespace EFCoreDatabaseFirst.Repositories.Abstractions;

public interface IClientTripRepository
{
    Task<bool> AnyAsync(int idClient, int idTrip, CancellationToken cancellationToken = default);
    Task<bool> AnyByClientIdAsync(int idClient, CancellationToken cancellationToken = default);
}