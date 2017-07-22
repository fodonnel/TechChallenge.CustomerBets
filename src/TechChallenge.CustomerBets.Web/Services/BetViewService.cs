using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TechChallenge.CustomerBets.Web.Models;

namespace TechChallenge.CustomerBets.Web.Services
{
    public interface IBetViewService
    {
        Task<IEnumerable<BetViewModel>> GetBets();
    }

    public class BetViewService : IBetViewService
    {
        private readonly ICustomerBetService _betService;

        public BetViewService(ICustomerBetService betService)
        {
            _betService = betService;
        }

        public async Task<IEnumerable<BetViewModel>> GetBets()
        {
            var customerDict = (await _betService.GetCustomersAsync()).ToDictionary(t => t.Id);
            var bets = await _betService.GetBetsAsync();

            return bets.Select(bet => new BetViewModel
            {
                Name = customerDict[bet.CustomerId].Name,
                RaceId = bet.RaceId,
                CustomerId = bet.CustomerId,
                ReturnStake = bet.ReturnStake,
                Won = bet.Won
            });
        }
    }
}