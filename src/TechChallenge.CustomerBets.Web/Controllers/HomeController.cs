using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechChallenge.CustomerBets.Web.Models;
using TechChallenge.CustomerBets.Web.Services;

namespace TechChallenge.CustomerBets.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerBetService _betService;

        public HomeController(ICustomerBetService betService)
        {
            _betService = betService;
        }

        public async Task<ActionResult> Index()
        {
            var customerDict = (await _betService.GetCustomersAsync()).ToDictionary(t => t.Id);
            var bets = await _betService.GetBetsAsync();

            var models = bets.Select(bet => new BetViewModel
            {
                Name = customerDict[bet.CustomerId].Name,
                RaceId = bet.RaceId,
                CustomerId = bet.CustomerId,
                ReturnStake = bet.ReturnStake,
                Won = bet.Won
            });

            return View("Index", models);
        }
    }
}