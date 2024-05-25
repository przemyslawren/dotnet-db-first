using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_E5.context;
using PJATK_APBD_E5.dtos;
using PJATK_APBD_E5.models;

namespace PJATK_APBD_E5.controllers;

[Route("api/[controller]")]
public class TripController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClientsAsync()
    {
        var trips = await context.Trip
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync();
        
        return Ok(trips);
    }
    
    [HttpPost]
    [Route("/{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTripAsync(int idTrip, AssignClientToTripDto assignClientToTripDto)
    {
        var trip = await context.Trip
            .FirstOrDefaultAsync(trip => trip.IdTrip == idTrip);
        if (trip == null)
        {
            return NotFound("Trip not found");
        }
        
        var client = await context.Client.FirstOrDefaultAsync(c => 
            c.Pesel == assignClientToTripDto.Pesel
        );
        
        if (client == null)
        {
            client = new Client
            {
                FirstName = assignClientToTripDto.FirstName,
                LastName = assignClientToTripDto.LastName,
                Email = assignClientToTripDto.Email,
                Telephone = assignClientToTripDto.Telephone,
                Pesel = assignClientToTripDto.Pesel
            };
            
            await context.Client.AddAsync(client);
            await context.SaveChangesAsync();
        }
        
        var isClientAlreadyRegistered = await context.Client_Trip.AnyAsync(ct => 
            ct.IdClient == client.IdClient && ct.IdTrip == idTrip
        );
        if (isClientAlreadyRegistered)
        {
            return BadRequest("Client is already registered for this trip");
        }
        
        var clientTrip = new Client_Trip
        {
            Client = client,
            Trip = trip,
            RegisteredAt = DateTime.Now,
            PaymentDate = assignClientToTripDto.PaymentDate
        };

        await context.Client_Trip.AddAsync(clientTrip);
        await context.SaveChangesAsync();

        var tripDto = new TripDto
        {
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople
        };

        return Ok(tripDto);
    }
}