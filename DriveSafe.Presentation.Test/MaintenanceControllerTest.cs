using Xunit;
using Moq;
using DriveSafe.Presentation.Publishing.Controllers;
using DriveSafe.Domain.Publishing.Services;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DriveSafe.Domain.Publishing.Models.Queries;

namespace DriveSafe.Presentation.Test
{
    public class MaintenanceControllerTest
    {
        [Fact]
        public async Task GetAllAsync_ReturnsOkResult()
        {
            // Arrange
            var mockQueryService = new Mock<IMaintenanceQueryService>();
            var mockCommandService = new Mock<IMaintenanceCommandService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new MaintenanceController(mockQueryService.Object, mockCommandService.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}