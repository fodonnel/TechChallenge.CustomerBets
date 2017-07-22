using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechChallenge.CustomerBets.Web.Services;

namespace TechChallenge.CustomerBets.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPingService _pingService;

        public HomeController(IPingService pingService)
        {
            _pingService = pingService;
        }

        public ActionResult Index()
        {
            var message = _pingService.Ping();
            ViewBag.Message = $"Ping service said: {message}";

            return View();
        }
    }
}