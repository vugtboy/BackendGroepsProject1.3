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
    public sealed class AppointmentGetAllAppointments_ReturnsRightAppointmentListAppointmentUpdateAppointment_UpdatesAppointment_AfterUpdating
    {
        [Fact]
        public async Task UpdatingAnAppointment_ChangesAppointmentToNewAppointment()
        {
            Random rnd = new Random();
            //ARANGE
            Guid appointmentId = Guid.NewGuid();
            var newAppointment = GenerateRandomAppointment("appointment", appointmentId);

            var appointmentRepository = new Mock<IAppointmentRepository>();
            var authenticationService = new Mock<IAuthenticationService>();

            Guid userid = Guid.NewGuid();

            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());
            appointmentRepository.Setup(x => x.GetAppointmentAsync(appointmentId)).ReturnsAsync(newAppointment);

            var appointmentController = new AppointmentController(appointmentRepository.Object, authenticationService.Object);

            //ACT
            var savedAppointment = await appointmentController.SaveAppointment(newAppointment);

            var response = await appointmentController.GetAppointment(appointmentId);

            //ASSERT
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(newAppointment, response);
            Xunit.Assert.IsType<CreatedResult>(savedAppointment);
        }
        private Appointment GenerateRandomAppointment(string name, Guid id)
        {
            var random = new Random();
            return new Appointment
            {
                Id = id,
                Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                Name = name,
                StickerId = random.Next(0, 3)
            };
        }
    }
}
