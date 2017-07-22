using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TechChallenge.CustomerBets.Web.Services;
using TechChallenge.CustomerBets.Web.Services.Models;

namespace TechChallenge.CustomerBets.UnitTests.Services
{
    [TestFixture]
    public class CustomerBetServiceTests
    {
        private Mock<ISettings> _settings;

        [SetUp]
        public void SetUp()
        {
            _settings = new Mock<ISettings>();
            _settings.Setup(t => t.ApiCode).Returns("code123");
            _settings.Setup(t => t.ApiUser).Returns("user123");
        }

        [Test]
        public async Task GetCustomers_Can_Get_All_Customers()
        {
            var client = new Mock<ICustomerBetClient>();
            client.Setup(t => t.GetAsync("api/GetCustomers?code=code123&name=user123"))
                .ReturnsAsync(GenerateResponse<IEnumerable<CustomerDto>>(new[]
                {
                    new CustomerDto {Id = 1, Name = "Bob"},
                    new CustomerDto {Id = 2, Name = "Rodger"},
                }, HttpStatusCode.OK));

            var target = new CustomerBetService(client.Object, _settings.Object);
            var result = (await target.GetCustomersAsync()).ToArray();

            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Bob"));
            Assert.That(result[1].Name, Is.EqualTo("Rodger"));
        }

        [Test]
        public void GetCustomers_Should_Throw_Exception_If_Status_Not_Ok()
        {
            var client = new Mock<ICustomerBetClient>();
            client.Setup(t => t.GetAsync("api/GetCustomers?code=code123&name=user123"))
                .ReturnsAsync(GenerateResponse<IEnumerable<CustomerDto>>(null, HttpStatusCode.NotFound));

            var target = new CustomerBetService(client.Object, _settings.Object);

            var ex = Assert.ThrowsAsync<Exception>(() => target.GetCustomersAsync());
            Assert.That(ex.Message, Is.EqualTo("Failed to get the customers"));
        }

        [Test]
        public async Task GetBet_Can_Get_All_Bets()
        {
            var client = new Mock<ICustomerBetClient>();
            client.Setup(t => t.GetAsync("api/GetBets?code=code123&name=user123"))
                .ReturnsAsync(GenerateResponse<IEnumerable<BetDto>>(new[]
                {
                    new BetDto {RaceId = 100},
                    new BetDto {RaceId = 200},
                }, HttpStatusCode.OK));


            var target = new CustomerBetService(client.Object, _settings.Object);
            var result = (await target.GetBetsAsync()).ToArray();

            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0].RaceId, Is.EqualTo(100));
            Assert.That(result[1].RaceId, Is.EqualTo(200));
        }

        [Test]
        public void GetBets_Should_Throw_Exception_If_Status_Not_Ok()
        {
            var client = new Mock<ICustomerBetClient>();
            client.Setup(t => t.GetAsync("api/GetBets?code=code123&name=user123"))
                .ReturnsAsync(GenerateResponse<IEnumerable<BetDto>>(null, HttpStatusCode.NotFound));

            var target = new CustomerBetService(client.Object, _settings.Object);

            var ex = Assert.ThrowsAsync<Exception>(() => target.GetBetsAsync());
            Assert.That(ex.Message, Is.EqualTo("Failed to get the bets"));
        }

        private HttpResponseMessage GenerateResponse<T>(T content, HttpStatusCode status)
        {
            return new HttpResponseMessage(status)
            {
                Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter())
            };
        }

    }
}
