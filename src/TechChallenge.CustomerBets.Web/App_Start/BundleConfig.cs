using System.Web;
using System.Web.Optimization;

namespace TechChallenge.CustomerBets.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/ClientThirdParty/jquery/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/ClientThirdParty/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/ClientThirdParty/datatables/js/jquery.dataTables.js",
                "~/ClientThirdParty/datatables/js/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                "~/Scripts/global.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/ClientThirdParty/bootstrap/css/bootstrap.css",
                      "~/ClientThirdParty/font-awesome/css/font-awesome.css",
                      "~/ClientThirdParty/datatables/css/dataTables.bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
