using EFCoreDatabaseFirst.Dtos;
using EFCoreDatabaseFirst.Repositories.Abstractions;
using EFCoreDatabaseFirst.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirst.Controllers;

[Route("api/[controller]")]
public class TripsController(ITripRepository tripRepository, IClientRepository clientRepository, IClientTripRepository clientTripRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTripsAsync(CancellationToken cancellationToken)
    {
        var trips = await tripRepository.GetTripsAsync(cancellationToken);
        
        var tripDtos = trips.Select(t => new TripDto(t)).ToList();
        return Ok(tripDtos);
    }
    
    [HttpPost("{idTrip}/clients")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AssignClientToTripAsync([FromRoute] int idTrip, [FromBody] AssignClientToTripDto assignClientToTripDto, CancellationToken cancellationToken)
    {
        if(ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }
        
        var client = await clientRepository.GetByPeselAsync(assignClientToTripDto.Pesel, cancellationToken);
        
        var trip = await tripRepository.AnyAsync(idTrip, cancellationToken);
        if (!trip)
        {
            return NotFound();
        }
        
        var currentDate = DateTime.UtcNow;
        var clientTrip = new ClientTrip()
        {
            RegisteredAt = currentDate,
            PaymentDate = assignClientToTripDto.PaymentDate
        };

        if (client is not null)
        {
            var clientTripExists = await clientTripRepository.AnyAsync(client.IdClient, idTrip, cancellationToken);
            if (clientTripExists)
            {
                return Conflict();
            }
            
            client.ClientTrips.Add(clientTrip);
            await clientRepository.UpdateAsync(client, cancellationToken);
        }
        else
        {
            client = new Client()
            {
                FirstName = assignClientToTripDto.FirstName,
                LastName = assignClientToTripDto.LastName,
                Email = assignClientToTripDto.Email,
                Telephone = assignClientToTripDto.Telephone,
                Pesel = assignClientToTripDto.Pesel,
                ClientTrips = new List<ClientTrip>() { clientTrip }
            };
            await clientRepository.CreateAsync(client, cancellationToken);
        }
            
        return Created(nameof(AssignClientToTripAsync), new { id = client.IdClient });
    }
}