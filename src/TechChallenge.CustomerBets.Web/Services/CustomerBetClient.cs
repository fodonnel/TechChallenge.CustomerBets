using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Services
{
    public interface ICustomerBetClient
    {
         Task<HttpResponseMessage> GetAsync(string requestUri);
    }

    public class CustomerBetClient : HttpClient, ICustomerBetClient
    {
        public CustomerBetClient(ISettings settings)
        {
            BaseAddress = new Uri(settings.ApiBaseAddress);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}