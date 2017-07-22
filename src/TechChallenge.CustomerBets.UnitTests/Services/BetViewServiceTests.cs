using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TechChallenge.CustomerBets.Web.Services;
using TechChallenge.CustomerBets.Web.Services.Models;

namespace TechChallenge.CustomerBets.UnitTests.Services
{
    [TestFixture]
    public class BetViewServiceTests
    {
        [Test]
        public async Task GetBets_Should_Return_ViewModel_Per_Bet()
        {
            var betService = new Mock<ICustomerBetService>();
            betService.Setup(t => t.GetCustomersAsync())
                .ReturnsAsync(new[] { new CustomerDto { Id = 1, Name = "name1" } });

            betService.Setup(t => t.GetBetsAsync())
                .ReturnsAsync(new[]
                {
                    new BetDto {CustomerId = 1, RaceId = 100},
                    new BetDto {CustomerId = 1, RaceId = 200}
                });


            var target = new BetViewService(betService.Object);

            var result = (await target.GetBets()).ToArray();

            Assert.That(result, Has.Length.EqualTo(2));
            Assert.That(result[0].RaceId, Is.EqualTo(100));
            Assert.That(result[1].RaceId, Is.EqualTo(200));
        }

        [Test]
        public async Task GetBets_Should_Map_Bet_To_ViewModel()
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

            var target = new BetViewService(betService.Object);
            var result = (await target.GetBets()).ToArray();

            Assert.That(result, Has.Length.EqualTo(1));
            Assert.That(result[0].RaceId, Is.EqualTo(100));
            Assert.That(result[0].CustomerId, Is.EqualTo(1));
            Assert.That(result[0].ReturnStake, Is.EqualTo(500));
            Assert.That(result[0].Won, Is.True);
        }

        [Test]
        public async Task GetBets_Should_Map_Customer_name()
        {
            var betService = new Mock<ICustomerBetService>();
            betService.Setup(t => t.GetCustomersAsync())
                .ReturnsAsync(new[] { new CustomerDto { Id = 1, Name = "name1" } });

            betService.Setup(t => t.GetBetsAsync())
                .ReturnsAsync(new[] { new BetDto { CustomerId = 1, RaceId = 100 } });

            var target = new BetViewService(betService.Object);
            var result = (await target.GetBets()).ToArray();

            Assert.That(result, Has.Length.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("name1"));
        }

        [Test]
        public async Task GetBets_Should_Calculate_TotalReturnStake()
        {
            var betService = new Mock<ICustomerBetService>();
            betService.Setup(t => t.GetCustomersAsync())
                .ReturnsAsync(new[]
                {
                    new CustomerDto { Id = 1, Name = "name1" },
                    new CustomerDto { Id = 2, Name = "name2" }
                });

            betService.Setup(t => t.GetBetsAsync())
                .ReturnsAsync(new[]
                {
                    new BetDto {CustomerId = 1, ReturnStake = 100, Won = true},
                    new BetDto {CustomerId = 1, ReturnStake = 200, Won = true},
                    new BetDto {CustomerId = 1, ReturnStake = 50, Won = false},

                    new BetDto {CustomerId = 2, ReturnStake = 200, Won = true},
                    new BetDto {CustomerId = 2, ReturnStake = 300, Won = true},
                    new BetDto {CustomerId = 2, ReturnStake = 25, Won = false},
                });

            var target = new BetViewService(betService.Object);
            var result = (await target.GetBets()).ToArray();

            var cust1Bet = result.First(t => t.CustomerId == 1);
            Assert.That(cust1Bet.TotalReturnStake, Is.EqualTo(250));

            var cust2Bet = result.First(t => t.CustomerId == 2);
            Assert.That(cust2Bet.TotalReturnStake, Is.EqualTo(475));
        }
    }
}
