using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private readonly ICustomerBetClient _client;
        private readonly ISettings _settings;

        public CustomerBetService(ICustomerBetClient client, ISettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var path = $"api/GetCustomers?code={_settings.ApiCode}&name={_settings.ApiUser}";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CustomerDto>>();
            }

            throw new Exception("Failed to get the customers");
        }


        public async Task<IEnumerable<BetDto>> GetBetsAsync()
        {
            var path = $"api/GetBets?code={_settings.ApiCode}&name={_settings.ApiUser}";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<BetDto>>();
            }

            throw new Exception("Failed to get the bets");
        }
    }
}