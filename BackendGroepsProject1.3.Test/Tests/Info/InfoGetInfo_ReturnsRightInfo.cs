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
    public sealed class InfoGetInfo_ReturnsRightInfo
    {
        Guid userid = Guid.NewGuid();
        [Fact]
        public async Task GetInfo_ReturnTheRightInfoObject()
        {
            Random rnd = new Random();
            //ARANGE
            //een aantal neppe appointments aanmaken
            var allInfo = GenerateRandomInfo(rnd.Next(1, 10), "Joep", "Meloen");

            var infoRepository = new Mock<IInfoRepository>();
            var authenticationService = new Mock<IAuthenticationService>();
            
            //de neppe appointments "in de database stoppen"
            infoRepository.Setup(x => x.GetInfoAsync(userid)).ReturnsAsync(allInfo.FirstOrDefault(a => a.UserId == userid));
            authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());

            var infoController = new InfoController(infoRepository.Object, authenticationService.Object);

            // ACT
            var response = await infoController.GetInfo();

            //ASSERT
            Xunit.Assert.NotNull(response);
            //kijken of de opgehaalde appointments gelijk zijn aan de appointments "in" de database
            Xunit.Assert.Equal(allInfo.FirstOrDefault(a => a.UserId == userid), response);
        }
        private List<Info> GenerateRandomInfo(int input, string name, string nameDoctor)
        {
            List<Info> createdInfo = new List<Info>();
            var random = new Random();
            for (int i = 0; i < input; i++)
            {
                Info info = new Info
                {
                    UserId = Guid.NewGuid(),
                    BirthDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                    Name = name,
                    Route = false,
                    NameDocter = nameDoctor,
                    AvatarId = random.Next(0, 3)
                };
                createdInfo.Add(info);
            }
            Info rightOne = new Info
            {
                UserId = userid,
                BirthDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
                Name = name,
                Route = false,
                NameDocter = nameDoctor,
                AvatarId = random.Next(0, 3)
            };
            createdInfo.Add(rightOne);
            return createdInfo;
        }
    }
}
