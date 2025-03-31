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
    public sealed class AppointmentDeleteAppointment_ReturnsNoContentResult
    {
        [Fact]
        public async Task DeleteAppointment_DeleteAppointment()
        {
            //ARANGE
            var appointment = GenerateRandomAppointment("appointment");

            var appointmentRepository = new Mock<IAppointmentRepository>();
            var authenticationService = new Mock<IAuthenticationService>();
            Guid userid = Guid.NewGuid();
            //de neppe appointments "in de database stoppen"
            appointmentRepository.Setup(x => x.GetAppointmentAsync(appointment.Id)).ReturnsAsync(appointment);
            appointmentRepository.Setup(x => x.DeleteAppointmentAsync(appointment.Id)).Returns(Task.CompletedTask);
            appointmentRepository.Setup(x => x.GetAppointmentAsync(It.IsAny<Guid>())).ReturnsAsync((Appointment)null);

            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());

            var appointmentController = new AppointmentController(appointmentRepository.Object, authenticationService.Object);

            // ACT
            var deleteResponse = await appointmentController.DeleteAppointment(appointment.Id);
            var appointmentAfterDelete = await appointmentController.GetAppointment(appointment.Id);
            //ASSERT
            Xunit.Assert.IsType<NoContentResult>(deleteResponse);
            //kijken of de opgehaalde appointments gelijk zijn aan de appointments "in" de database
            Xunit.Assert.Null(appointmentAfterDelete);
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
