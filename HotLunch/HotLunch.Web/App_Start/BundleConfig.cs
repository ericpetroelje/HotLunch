using System.Web;
using System.Web.Optimization;

namespace HotLunch.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").IncludeDirectory("~/Content/js", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/css").IncludeDirectory("~/Content/css", "*.css"));
        }
    }
}
