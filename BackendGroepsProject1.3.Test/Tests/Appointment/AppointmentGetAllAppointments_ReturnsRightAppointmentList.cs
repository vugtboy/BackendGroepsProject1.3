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
    public sealed class AppointmentGetAllAppointments_ReturnsRightAppointmentList
    {
        [Fact]
        public async Task GetAllAppointments_ReturnsAppointmentList()
        {
            Random rnd = new Random();
            //ARANGE
            //een aantal neppe appointments aanmaken
            var allAppointments = GenerateRandomAppointment(rnd.Next(1, 10), "appointment");

            var appointmentRepository = new Mock<IAppointmentRepository>();
            var authenticationService = new Mock<IAuthenticationService>();
            Guid userid = Guid.NewGuid();
            //de neppe appointments "in de database stoppen"
            appointmentRepository.Setup(x => x.GetAllAppointmentsAsync(userid)).ReturnsAsync(allAppointments);
            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());

            var appointmentController = new AppointmentController(appointmentRepository.Object, authenticationService.Object);
            
            // ACT
            var response = await appointmentController.GetAllAppointments();

            //ASSERT
            Xunit.Assert.NotNull(response);
            //kijken of de opgehaalde appointments gelijk zijn aan de appointments "in" de database
            Xunit.Assert.Equal(allAppointments, response);
        }
        private List<Appointment> GenerateRandomAppointment(int input, string name)
        {
            List<Appointment> createdAppointments = new List<Appointment>();
            var random = new Random();
            for(int i = 0; i < input; i++)
            {
                Appointment appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                    Name = name,
                    StickerId = random.Next(0, 3)
                };
                createdAppointments.Add(appointment);
            }        
            return createdAppointments;
        }
    }
}
