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
    public sealed class InfoUpdateInfo_UpdatesInfo_AfterUpdating
    {
        Guid userid = Guid.NewGuid();
        [Fact]
        public async Task UpdatingAnInfo_ChangesInfoToNewInfo()
        {
            Random rnd = new Random();
            //ARANGE
            var newInfo = GenerateRandomInfo("Michiel", "Oetker");

            var infoRepository = new Mock<IInfoRepository>();
            var authenticationService = new Mock<IAuthenticationService>();

            

            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());
            infoRepository.Setup(x => x.GetInfoAsync(userid)).ReturnsAsync(newInfo);

            var infoController = new InfoController(infoRepository.Object, authenticationService.Object);

            //ACT
            var savedInfo = await infoController.SaveInfo(newInfo);

            var response = await infoController.GetInfo();

            //ASSERT
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(newInfo, response);
            Xunit.Assert.IsType<CreatedResult>(savedInfo);
        }
        private Info GenerateRandomInfo(string name, string nameDoctor)
        {
            var random = new Random();
            return new Info
            {
                UserId = userid,
                BirthDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                Name = name,
                Route = false,
                NameDocter = nameDoctor,
                AvatarId = random.Next(0, 3)
            };
        }
    }
}
