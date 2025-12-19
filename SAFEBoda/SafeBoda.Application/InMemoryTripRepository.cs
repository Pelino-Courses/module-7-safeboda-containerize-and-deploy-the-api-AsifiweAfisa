using System;
using System.Collections.Generic;
using SafeBoda.Core;
using SafeBoda.Core.Models;

namespace SafeBoda.Application
{
    public class InMemoryTripRepository : ITripRepository
    {
        private readonly List<Trip> trips;
        

        public InMemoryTripRepository()
        {
            trips = new List<Trip>
            {
                new Trip(
                    Guid.NewGuid(),
                    Guid.NewGuid(),  
                    Guid.NewGuid(),  
                    new Location(-1.9441, 30.0619),  // Start (Kigali city center)
                    new Location(-1.9500, 30.0891),  // End (Remera)
                    2000.00m,
                    DateTime.Now
                ),

                new Trip(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    new Location(-1.9501, 30.0902),  // Start (Remera)
                    new Location(-1.9532, 30.0984),  // End (Kimironko)
                    1600.00m,
                    DateTime.Now.AddMinutes(-15)
                )
            };
        }

        public IEnumerable<Trip> GetActiveTrips()
        {
            return trips;
        }

        public void AddTrip(Trip trip)
        {
            trips.Add(trip);
        }
        public Trip GetTripById(Guid id)
        {
            return trips.FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTrip(Trip trip)
        {
            var index = trips.FindIndex(t => t.Id == trip.Id);

            if (index != -1)
            {
                trips[index] = trip;
            }
        }
        public void DeleteTrip(Guid id)
        {
            var trip = GetTripById(id);
            if(trip != null)
            {
                trips.Remove(trip);
            }
        }

    }
}
