using System.Diagnostics.CodeAnalysis;
using EFCoreDatabaseFirst.Repositories.Models;

namespace EFCoreDatabaseFirst.Dtos;

[method: SetsRequiredMembers]
public class ClientDto(Client c)
{
    public required string FirstName { get; set; } = c.FirstName;
    public required string LastName { get; set; } = c.LastName;
}