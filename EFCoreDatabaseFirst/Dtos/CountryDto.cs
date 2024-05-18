using System.Diagnostics.CodeAnalysis;
using EFCoreDatabaseFirst.Repositories.Models;

namespace EFCoreDatabaseFirst.Dtos;

[method: SetsRequiredMembers]
public class CountryDto(Country c)
{
    public required string Name { get; set; } = c.Name;
}