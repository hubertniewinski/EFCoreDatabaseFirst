using EFCoreDatabaseFirst.Repositories.Abstractions;
using EFCoreDatabaseFirst.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirst.Repositories;

public class ClientRepository(ApbdContext context) : IClientRepository
{
    public async Task CreateAsync(Client client, CancellationToken cancellationToken)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Client client, CancellationToken cancellationToken)
    {
        context.Clients.Update(client);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<Client?> GetByPeselAsync(string pesel, CancellationToken cancellationToken) 
        => context.Clients.Where(x => x.Pesel == pesel).FirstOrDefaultAsync(cancellationToken);

    public async Task<bool> AnyAsync(int idClient, CancellationToken cancellationToken)
        => await context.Clients.AnyAsync(x => x.IdClient == idClient, cancellationToken);

    public async Task DeleteClientAsync(int idClient, CancellationToken cancellationToken)
    {
        await context.Clients.Where(x => x.IdClient == idClient).ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}