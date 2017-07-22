using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Services
{
    public interface ISettings
    {
        string ApiCode { get; set; }
        string ApiUser { get; set; }
        string ApiBaseAddress { get; set; }
    }

    public class Settings : ISettings
    {
        public string ApiCode { get; set; }
        public string ApiUser { get; set; }
        public string ApiBaseAddress { get; set; }
    }
}