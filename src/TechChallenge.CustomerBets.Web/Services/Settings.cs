using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Services
{
    public interface ISettings
    {
        string ApiCode { get; }
        string ApiUser { get; }
        string ApiBaseAddress { get; }
    }

    public class Settings : ISettings
    {
        public string ApiCode => ConfigurationManager.AppSettings[nameof(ApiCode)];
        public string ApiUser => ConfigurationManager.AppSettings[nameof(ApiUser)];
        public string ApiBaseAddress => ConfigurationManager.AppSettings[nameof(ApiBaseAddress)];
    }
}