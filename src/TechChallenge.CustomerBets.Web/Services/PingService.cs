using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Services
{
    public interface IPingService
    {
        string Ping();
    }

    public class PingService : IPingService
    {
        public string Ping()
        {
            return "Pong";
        }
    }
}