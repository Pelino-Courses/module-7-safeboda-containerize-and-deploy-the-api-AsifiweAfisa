using SafeBoda.Core;
using SafeBoda.Core.Models;




namespace SafeBoda.Application
{
    public interface ITripRepository
    {
        IEnumerable<Trip> GetActiveTrips();
        void AddTrip(Trip trip);
        Trip GetTripById(Guid id);
        void UpdateTrip(Trip trip);
        void DeleteTrip(Guid id);

    }
}