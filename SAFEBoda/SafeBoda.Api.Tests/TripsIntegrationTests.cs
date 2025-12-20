using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Net;

namespace SafeBoda.Api.Tests
{
    public class TripsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TripsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetTrips_WithAuth_ReturnsOk()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                    services.PostConfigure<AuthenticationOptions>(options => 
                    {
                        options.DefaultAuthenticateScheme = "Test";
                        options.DefaultChallengeScheme = "Test";
                    });
                });
            })
            .CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", "DummyToken");

            var response = await client.GetAsync("/api/trips");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                
                throw new System.Exception("Still 401! Make sure TripsController has [Authorize] and not [Authorize(AuthenticationSchemes='Bearer')]");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}