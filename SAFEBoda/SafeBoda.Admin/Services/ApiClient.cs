using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafeBoda.Admin.Services
{
    public class ApiClient
    {
        public HttpClient HttpClient { get; }

        public ApiClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<List<TripDto>> GetTripsAsync()
        {
            return await HttpClient.GetFromJsonAsync<List<TripDto>>("api/trips");
        }
    }

    public class TripDto
    {
        public Guid Id { get; set; }
        public LocationDto Start { get; set; } 
        public LocationDto End { get; set; }
        public decimal Fare { get; set; } 
    }

    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}