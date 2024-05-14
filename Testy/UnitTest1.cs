using AppCore.Filters;
using AppCore.Models;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using ProjectExam;
using ProjectExam.Controllers;
using System.Net;

namespace Testy
{
    public class UnitTest1
    {
        [Fact]
        public async Task Get_CountryList_MockV()
        {
            var mockCountryService = new Mock<ICountryService>();
            var controle=new CountryController(mockCountryService.Object);

            var expectedCountry = new List<Country>
            {
                new Country{id=1,country_name="argentyna"},
                new Country{id=2,country_name="Chuj"}
            };
            mockCountryService.Setup(service=>service.Get(It.IsAny<CountryFilter>())).ReturnsAsync(expectedCountry);

            var actionRes = await controle.GetCountries(new CountryFilter());

            var okResult = Assert.IsType<OkObjectResult>(actionRes.Result);
            var resultBooks = Assert.IsAssignableFrom<List<Country>>(okResult.Value);
            Assert.Equal(expectedCountry.Count, resultBooks.Count);
        }
        [Fact]
        public async void GetReturnAssert()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetFromJsonAsync<List<Country>>("/api/Country");

            //Assert
            Assert.Equal(5, result.Count);
        }
        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetAsync("/api/Country");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }
    }
}