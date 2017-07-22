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
            var betViewService = new Mock<IBetViewService>();

            var target = new HomeController(betViewService.Object);
            var result = await target.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index"));

            var model = result.Model as IEnumerable<BetViewModel>;
            Assert.That(model, Is.Not.Null);
        }
    }
}
