using Microsoft.AspNetCore.Mvc;
using Moq;
using SafeBoda.Api.Controllers;
using SafeBoda.Application;
using SafeBoda.Core.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SafeBoda.Api.Tests
{
    public class TripsControllerTests
    {
        private readonly Mock<ITripRepository> _mockRepo;
        private readonly TripsController _controller;

        public TripsControllerTests()
        {
            _mockRepo = new Mock<ITripRepository>();
            _controller = new TripsController(_mockRepo.Object);
        }

        [Fact]
        public void GetTrips_ReturnsOkResult_WithListOfTrips()
        {
            
            var fakeTrips = new List<Trip>
            {
                new Trip(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new Location(0,0), new Location(0,0), 5000m, DateTime.UtcNow),
                new Trip(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new Location(0,0), new Location(0,0), 6000m, DateTime.UtcNow)
            };

            _mockRepo.Setup(repo => repo.GetActiveTrips()).Returns(fakeTrips);

            
            var result = _controller.GetTrips();

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTrips = Assert.IsType<List<Trip>>(okResult.Value);
            Assert.Equal(2, returnTrips.Count);
        }

        [Fact]
        public void RequestTrip_ValidRequest_ReturnsOkAndAddsTrip()
        {
            
            var request = new TripsController.TripRequest(
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                0.34, 32.58,    
                0.31, 32.58,    
                5000 
            );

           
            var result = _controller.RequestTrip(request);

           
            Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(repo => repo.AddTrip(It.IsAny<Trip>()), Times.Once);
        }
    }
}