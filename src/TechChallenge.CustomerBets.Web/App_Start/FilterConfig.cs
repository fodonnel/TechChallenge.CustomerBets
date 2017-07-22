using System.Web;
using System.Web.Mvc;

namespace TechChallenge.CustomerBets.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
