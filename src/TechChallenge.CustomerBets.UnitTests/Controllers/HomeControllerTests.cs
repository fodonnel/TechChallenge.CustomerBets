using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TechChallenge.CustomerBets.Web.Controllers;
using TechChallenge.CustomerBets.Web.Models;
using TechChallenge.CustomerBets.Web.Services;
using TechChallenge.CustomerBets.Web.Services.Models;

namespace TechChallenge.CustomerBets.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public async Task Get_Index_Should_Return_Index_View_With_Model()
        {
            var betService = new Mock<ICustomerBetService>();

            var target = new HomeController(betService.Object);
            var result = await target.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index"));

            var model = result.Model as IEnumerable<BetViewModel>;
            Assert.That(model, Is.Not.Null);
        }

        [Test]
        public async Task Get_Index_Should_Return_ViewModel_Per_Bet()
        {
            var betService = new Mock<ICustomerBetService>();
            betService.Setup(t => t.GetCustomersAsync())
                .ReturnsAsync(new[] {new CustomerDto {Id = 1, Name = "name1"}});

            betService.Setup(t => t.GetBetsAsync())
                .ReturnsAsync(new[]
                {
                    new BetDto {CustomerId = 1, RaceId = 100},
                    new BetDto {CustomerId = 1, RaceId = 200}
                });


            var target = new HomeController(betService.Object);
            var result = (ViewResult) await target.Index();
            var model = ((IEnumerable<BetViewModel>) result.Model).ToArray();

            Assert.That(model, Has.Length.EqualTo(2));
            Assert.That(model[0].RaceId, Is.EqualTo(100));
            Assert.That(model[1].RaceId, Is.EqualTo(200));
        }

        [Test]
        public async Task Get_Index_Should_Map_Bet_To_ViewModel()
        {
            var betService = new Mock<ICustomerBetService>();
            betService.Setup(t => t.GetCustomersAsync())
                .ReturnsAsync(new[] { new CustomerDto { Id = 1, Name = "name1" } });

            betService.Setup(t => t.GetBetsAsync()).ReturnsAsync(new[]
            {
                new BetDto
                {
                    CustomerId = 1,
                    RaceId = 100,
                    ReturnStake = 500,
                    Won = true
                }
            });

            var target = new HomeController(betService.Object);
            var result = (ViewResult)await target.Index();
            var model = ((IEnumerable<BetViewModel>)result.Model).ToArray();

            Assert.That(model, Has.Length.EqualTo(1));
            Assert.That(model[0].RaceId, Is.EqualTo(100));
            Assert.That(model[0].CustomerId, Is.EqualTo(1));
            Assert.That(model[0].ReturnStake, Is.EqualTo(500));
            Assert.That(model[0].Won, Is.True);
        }

        [Test]
        public async Task Get_Index_Should_Map_Customer_name()
        {
            var betService = new Mock<ICustomerBetService>();
            betService.Setup(t => t.GetCustomersAsync())
                .ReturnsAsync(new[] { new CustomerDto { Id = 1, Name = "name1" } });

            betService.Setup(t => t.GetBetsAsync())
                .ReturnsAsync(new[] {new BetDto {CustomerId = 1, RaceId = 100}});

            var target = new HomeController(betService.Object);
            var result = (ViewResult)await target.Index();
            var model = ((IEnumerable<BetViewModel>)result.Model).ToArray();

            Assert.That(model, Has.Length.EqualTo(1));
            Assert.That(model[0].Name, Is.EqualTo("name1"));
        }
    }
}
