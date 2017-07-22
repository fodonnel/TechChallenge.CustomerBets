using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TechChallenge.CustomerBets.Web.Services.Models;

namespace TechChallenge.CustomerBets.Web.Services
{
    public interface ICustomerBetService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<IEnumerable<BetDto>> GetBetsAsync();
    }

    public class CustomerBetService : ICustomerBetService
    {
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<BetDto>> GetBetsAsync()
        {
            throw new NotImplementedException();
        }
    }
}