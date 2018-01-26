using System.Web;
using System.Web.Optimization;

namespace HaynyBatista
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/umd/popper.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/LayoutJs").Include(
                      "~/Scripts/Layout.js",
                      "~/Scripts/Util.js",
                      "~/Plugins/tinymce/tinymce.min.js",
                      "~/Plugins/autosize-master/dist/autosize.min.js",
                      "~/Plugins/TagsInput/tagsinput.js",
                      "~/Plugins/ftellipsis-master/build/ftellipsis.min.js"));
            bundles.Add(new StyleBundle("~/bundles/LayoutCss").Include(
                      "~/Content/Layout.css",
                      "~/Content/animate.min.css",
                      "~/Plugins/FontAwesome/css/font-awesome.min.css",
                      "~/Plugins/TagsInput/tagsinput.css"));
        }
    }
}
