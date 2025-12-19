using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SafeBoda.Core;
using SafeBoda.Core.Models;
using SafeBoda.Application;

namespace SafeBoda.Infrastructure
{
    public class EfTripRepository : ITripRepository
    {
        private readonly SafeBodaDbContext db;

        public EfTripRepository(SafeBodaDbContext context)
        {
            db = context;
        }

        public void AddTrip(Trip trip)
        {
            db.Trips.Add(trip);
            db.SaveChanges();
        }

        public IEnumerable<Trip> GetActiveTrips()
        {
            return db.Trips
                           .Include(t => t.Start)
                           .Include(t => t.End)
                           .AsNoTracking()
                           .ToList();
        }
        public Trip GetTripById(Guid id)
        {
        return db.Trips
             .Include(t => t.Start)
             .Include(t => t.End)
             .FirstOrDefault(t => t.Id == id);
        }


        public void UpdateTrip(Trip trip)
        {
            db.Trips.Update(trip);
            db.SaveChanges();
        }
        public void DeleteTrip(Guid id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                db.Trips.Remove(trip);
                db.SaveChanges();
            }
        }
    }
}