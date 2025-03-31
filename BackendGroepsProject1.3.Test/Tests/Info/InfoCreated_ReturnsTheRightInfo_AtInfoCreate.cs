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
    public sealed class InfoCreated_ReturnsTheRightInfo_AtInfoCreate
    {
        Guid userid = Guid.NewGuid();
        [Fact]
        public async Task CreateInfo_ReturnsCreatedWhenRandomInfoIsCreated()
        {
            //ARANGE
            var newInfo = GenerateRandomInfo("Joepje", "Hassan");

            var infoRepository = new Mock<IInfoRepository>();
            var authenticationService = new Mock<IAuthenticationService>();
            
            infoRepository.Setup(x => x.GetInfoAsync(newInfo.UserId)).ReturnsAsync(newInfo);
            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());

            var InfoController = new InfoController(infoRepository.Object, authenticationService.Object);
            
            // ACT
            var response = await InfoController.CreateInfo(newInfo);

            var createdAtActionResult = Xunit.Assert.IsType<CreatedAtActionResult>(response);
            
            var createdInfo = createdAtActionResult.Value as Info;

            //ASSERT
            Xunit.Assert.NotNull(createdInfo);
            
            Xunit.Assert.Equal(newInfo.UserId, createdInfo.UserId);
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
