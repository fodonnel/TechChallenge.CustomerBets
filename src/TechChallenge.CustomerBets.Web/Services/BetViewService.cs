using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var bets = (await _betService.GetBetsAsync()).ToArray();

            var customerNameDict = (await _betService.GetCustomersAsync())
                .ToDictionary(t => t.Id);

            var totalReturnStakeDict = bets
                .AsParallel()
                .GroupBy(t => t.CustomerId)
                .Select(t => new
                {
                    t.Key,
                    Total = t.Sum(c => c.ReturnStake * (c.Won ? 1 : -1))
                }).ToDictionary(t => t.Key);

            return bets.Select(bet => new BetViewModel
            {
                TotalReturnStake = totalReturnStakeDict[bet.CustomerId].Total,
                Name = customerNameDict[bet.CustomerId].Name,
                RaceId = bet.RaceId,
                CustomerId = bet.CustomerId,
                ReturnStake = bet.ReturnStake,
                Won = bet.Won
            });
        }
    }
}