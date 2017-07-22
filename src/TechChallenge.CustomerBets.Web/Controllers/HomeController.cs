using System.Threading.Tasks;
using System.Web.Mvc;
using TechChallenge.CustomerBets.Web.Services;

namespace TechChallenge.CustomerBets.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBetViewService _betViewService;

        public HomeController(IBetViewService betViewService)
        {
            _betViewService = betViewService;
        }

        public async Task<ActionResult> Index()
        {
            var models = await _betViewService.GetBets();
            return View("Index", models);
        }
    }
}