using EFCoreDatabaseFirst.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirst.Controllers;

[Route("api/[controller]")]
public class ClientsController(IClientRepository clientRepository, IClientTripRepository clientTripRepository) : ControllerBase
{
    [HttpDelete("{idClient}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteClientAsync([FromRoute] int idClient, CancellationToken cancellationToken)
    {
        var client = await clientRepository.AnyAsync(idClient, cancellationToken);
        if (!client)
        {
            return NotFound();
        }
        
        var tripForClientExists = await clientTripRepository.AnyByClientIdAsync(idClient, cancellationToken);
        if (tripForClientExists)
        {
            return Conflict();
        }

        await clientRepository.DeleteClientAsync(idClient, cancellationToken);
        return NoContent();
    }
}