using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroepsProject1._3Backend.WebApi.Controllers;
using GroepsProject1._3Backend.WebApi;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using GroepsProject1._3Backend.WebApi.Services;

namespace BackendGroepsProject1._3.Test.Tests;

[TestClass]
public class CreateInfo
{
    [TestMethod]
    public async Task CreateInfo_RandomInfo_ReturnsCreated()
    {
        //ARANGE
        var newInfo = GenerateNewInfo("info");

        var infoRepository = new Mock<IInfoRepository>();
        var authenticationService = new Mock<IAuthenticationService>();
        Guid userid = Guid.NewGuid();

        //neppe data aanmaken zodat er een nieuwe appointment kan worden opgehaald
        authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());
        infoRepository.Setup(x => x.GetInfoAsync(userid)).ReturnsAsync(newInfo);
        var infoController = new InfoController(infoRepository.Object, authenticationService.Object);

        // ACT
        var response = await infoController.CreateInfo(newInfo);

        //ASSERT
        Assert.IsInstanceOfType<CreatedAtActionResult>(response);
        var createdAtActionResult = response as CreatedAtActionResult;

        var createdInfo = createdAtActionResult.Value as Info;
       
        Assert.IsNotNull(createdInfo);
       
        Assert.AreEqual(newInfo.UserId, createdInfo.UserId);
    }

    [TestMethod]
    public async Task CreateInfo_DatabaseError_ReturnsBadRequest()
    {
        //ARANGE
        var newInfo = GenerateNewInfo("info");

        var infoRepository = new Mock<IInfoRepository>();
        var authenticationService = new Mock<IAuthenticationService>();
        Guid userid = Guid.NewGuid();

        //neppe data aanmaken zodat er een nieuwe appointment kan worden opgehaald
        authenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userid.ToString());
        var infoController = new InfoController(infoRepository.Object, authenticationService.Object);

        // ACT
        var response = await infoController.CreateInfo(newInfo);

        //ASSERT
        Assert.IsInstanceOfType<BadRequestObjectResult>(response);
        var createdAtActionResult = response as BadRequestObjectResult;

        Assert.AreEqual(400, createdAtActionResult.StatusCode);
        Assert.AreEqual("Ja, mislukt, de info is niet goed aangemaakt :P", createdAtActionResult.Value );

    }

    private Info GenerateNewInfo(string name)
    {
        var random = new Random();
        return new Info
        {
            UserId = Guid.NewGuid(),
            BirthDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0),
            Name = name,
            AvatarId = random.Next(0, 3)
        };
    }


}
