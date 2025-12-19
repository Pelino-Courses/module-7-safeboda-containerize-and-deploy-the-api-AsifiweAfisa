using Microsoft.AspNetCore.Mvc;
using SafeBoda.Application;
using SafeBoda.Core;
using SafeBoda.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace SafeBoda.Api.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository tripsRepository;

        public TripsController(ITripRepository tripRepository)
        {
            tripsRepository = tripRepository;
        }

        [HttpGet]
        public IActionResult GetTrips()
        {
            var trips = tripsRepository.GetActiveTrips();
            return Ok(trips);
        }

        
        public record TripRequest(
            Guid RiderId, 
            Guid DriverId, 
            double StartLat, 
            double StartLong, 
            double EndLat, 
            double EndLong, 
            decimal Price
        );

        [HttpPost("request")]
        public IActionResult RequestTrip([FromBody] TripRequest request)
        {
            
            var startLocation = new Location(request.StartLat, request.StartLong);
            var endLocation = new Location(request.EndLat, request.EndLong);

            var trip = new Trip(
                Guid.NewGuid(),       
                request.RiderId,     
                request.DriverId,     
                startLocation,        
                endLocation,          
                request.Price,        
                DateTime.UtcNow       
            );
            
            tripsRepository.AddTrip(trip);

            return Ok(trip);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrip(Guid id, [FromBody] TripRequest updatedTrip)
        {
            var existingTrip = tripsRepository.GetTripById(id);
            if (existingTrip == null)
            {
                return NotFound($"Trip with ID {id} not found.");
            }

            
            existingTrip.Start = new Location(updatedTrip.StartLat, updatedTrip.StartLong);
            existingTrip.End = new Location(updatedTrip.EndLat, updatedTrip.EndLong);
            
            tripsRepository.UpdateTrip(existingTrip);
            return Ok(existingTrip);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrip(Guid id)
        {
            var trip = tripsRepository.GetTripById(id);
            if (trip == null)
            {
                return NotFound($"Trip with ID {id} not found.");
            }

            tripsRepository.DeleteTrip(id);
            return NoContent();
        }
    }
}