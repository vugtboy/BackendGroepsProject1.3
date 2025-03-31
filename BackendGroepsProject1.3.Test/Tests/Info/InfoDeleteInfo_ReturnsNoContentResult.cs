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
    public sealed class InfoDeleteInfo_ReturnsNoContentResult
    {
        [Fact]
        public async Task DeleteInfo_DeletesInfo()
        {
            //ARANGE
            var info = GenerateRandomInfo("Michiel", "Oetker");
            //ARANGE

            var infoRepository = new Mock<IInfoRepository>();
            var authenticationService = new Mock<IAuthenticationService>();

            Guid userid = Guid.NewGuid();
            //de neppe appointments "in de database stoppen"
            infoRepository.Setup(x => x.GetInfoAsync(info.UserId)).ReturnsAsync(info);
            infoRepository.Setup(x => x.DeleteInfoAsync(info.UserId)).Returns(Task.CompletedTask);
            infoRepository.Setup(x => x.GetInfoAsync(It.IsAny<Guid>())).ReturnsAsync((Info)null);

            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());

            var infoController = new InfoController(infoRepository.Object, authenticationService.Object);

            // ACT
            var deleteResponse = await infoController.DeleteInfo();
            var appointmentAfterDelete = await infoController.GetInfo();
            //ASSERT
            Xunit.Assert.IsType<NoContentResult>(deleteResponse);
            //kijken of de opgehaalde appointments gelijk zijn aan de appointments "in" de database
            Xunit.Assert.Null(appointmentAfterDelete);
        }
        private Info GenerateRandomInfo(string name, string nameDoctor)
        {
            var random = new Random();
            return new Info
            {
                UserId = Guid.NewGuid(),
                BirthDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                Name = name,
                Route = false,
                NameDocter = nameDoctor,
                AvatarId = random.Next(0, 3)
            };
        }
    }
}
