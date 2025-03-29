using GroepsProject1._3Backend.WebApi;
using GroepsProject1._3Backend.WebApi.Controllers;
using GroepsProject1._3Backend.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Castle.Components.DictionaryAdapter;
namespace BackendGroepsProject1._3.Test
{
    [TestClass]
    public sealed class AppointmentCreatedTheRightAppointent_AtAppointmentCreate
    {
        [Fact]
        public async Task CreateAppointment_ReturnsCreatedWhenRandomAppointmentIsCreated()
        {
            //ARANGE
            var newAppointment = GenerateRandomAppointment("appointment");

            var appointmentRepository = new Mock<IAppointmentRepository>();
            var authenticationService = new Mock<IAuthenticationService>();
            Guid userid = Guid.NewGuid();
            //neppe data aanmaken zodat er een nieuwe appointment kan worden opgehaald
            appointmentRepository.Setup(x => x.GetAppointmentAsync(newAppointment.Id)).ReturnsAsync(newAppointment);
            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());

            var appointmentController = new AppointmentController(appointmentRepository.Object, authenticationService.Object);
            
            // ACT
            var response = await appointmentController.CreateAppointment(newAppointment);

            var createdAtActionResult = Xunit.Assert.IsType<CreatedAtActionResult>(response);
            
            var createdAppointment = createdAtActionResult.Value as Appointment;

            //ASSERT
            Xunit.Assert.NotNull(createdAppointment);
            
            Xunit.Assert.Equal(newAppointment.Id, createdAppointment.Id);
        }
        private Appointment GenerateRandomAppointment(string name)
        {
            var random = new Random();
            return new Appointment
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                Name = name,
                StickerId = random.Next(0, 3)
            };
        }
    }
}
