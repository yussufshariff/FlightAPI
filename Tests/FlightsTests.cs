/*/ using FlightAPI.Controllers;
using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FlightAPI.Tests
{
    public class FlightsControllerTests
    {
        [Fact]
        public async Task GetAllFlights_ReturnsOkWithFlights()
        {
            // Arrange
            var expectedFlights = new List<Flight>
            {
                new Flight { FlightId = "EK123"},
                new Flight { FlightId = "QR456" },
            };

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetAllFlightsAsync()).ReturnsAsync(expectedFlights);

            var controller = new FlightController(mockRepository.Object);

            // Act
            var result = await controller.GetAllFlights();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var flights = Assert.IsType<List<Flight>>(okResult.Value);
            Assert.Equal(expectedFlights.Count, flights.Count);
        }
    }
}
/*/