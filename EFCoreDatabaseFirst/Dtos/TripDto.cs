using System.Diagnostics.CodeAnalysis;
using EFCoreDatabaseFirst.Repositories.Models;

namespace EFCoreDatabaseFirst.Dtos;

public class TripDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime DateFrom { get; set; }
    public required DateTime DateTo { get; set; }
    public required int MaxPeople { get; set; }
    public required IEnumerable<CountryDto> Countries { get; set; }
    public required IEnumerable<ClientDto> Clients { get; set; }
    
    [SetsRequiredMembers]
    public TripDto(Trip t)
    {
        Name = t.Name;
        Description = t.Description;
        DateFrom = t.DateFrom;
        DateTo = t.DateTo;
        MaxPeople = t.MaxPeople;
        Countries = t.IdCountries.Select(c => new CountryDto(c));
        Clients = t.ClientTrips.Select(ct => new ClientDto(ct.IdClientNavigation));
    }
}