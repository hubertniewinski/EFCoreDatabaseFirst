using EFCoreDatabaseFirst.Repositories.Models;

namespace EFCoreDatabaseFirst.Repositories.Abstractions;

public interface IClientRepository
{
    Task CreateAsync(Client client, CancellationToken cancellationToken = default);
    Task UpdateAsync(Client client, CancellationToken cancellationToken = default);
    Task<Client?> GetByPeselAsync(string pesel, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(int idClient, CancellationToken cancellationToken = default);
    Task DeleteClientAsync(int idClient, CancellationToken cancellationToken = default);
}