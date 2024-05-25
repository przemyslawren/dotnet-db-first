using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_E5.context;
using PJATK_APBD_E5.models;

namespace PJATK_APBD_E5.controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClientsAsync()
    {
        var clients = await context.Client.ToListAsync();
        
        return Ok(clients);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        var client = await context.Client
            .Include(c => c.Client_Trips)
            .FirstOrDefaultAsync(client => client.IdClient == id);
        
        if (client == null)
        {
            return NotFound();
        }
        
        if (client.Client_Trips.Count > 0)
        {
            return BadRequest("Client has trips assigned");
        }
        
        
        context.Client.Remove(client);
        await context.SaveChangesAsync();
        
        return Ok(client);
    }
}