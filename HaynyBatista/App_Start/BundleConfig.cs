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
                      "~/Plugins/Clamp.js-master/clamp.min.js",
                      "~/Scripts/Layout.js",
                      "~/Scripts/Util.js",
                      "~/Plugins/tinymce/tinymce.min.js",
                      "~/Plugins/tinymce/plugins/advlist/*.js",
                      "~/Plugins/tinymce/plugins/autolink/*.js",
                      "~/Plugins/tinymce/plugins/lists/*.js",
                      "~/Plugins/tinymce/plugins/link/*.js",
                      "~/Plugins/tinymce/plugins/print/*.js",
                      "~/Plugins/tinymce/plugins/preview/*.js",
                      "~/Plugins/tinymce/plugins/anchor/*.js",
                      "~/Plugins/tinymce/plugins/textcolor/*.js",
                      "~/Plugins/tinymce/plugins/searchreplace/*.js",
                      "~/Plugins/tinymce/plugins/visualblocks/*.js",
                      "~/Plugins/tinymce/plugins/code/*.js",
                      "~/Plugins/tinymce/plugins/fullscreen/*.js",
                      "~/Plugins/tinymce/plugins/insertdatetime/*.js",
                      "~/Plugins/tinymce/plugins/table/*.js",
                      "~/Plugins/tinymce/plugins/contextmenu/*.js",
                      "~/Plugins/tinymce/plugins/paste/*.js",
                      "~/Plugins/tinymce/plugins/help/*.js",
                      "~/Plugins/tinymce/plugins/wordcount/*.js",
                      "~/Plugins/tinymce/plugins/charmap/*.js",
                      "~/Plugins/tinymce/langs/*.js",
                      "~/Plugins/tinymce/themes/modern/theme.min.js",
                      "~/Plugins/autosize-master/dist/autosize.min.js",
                      "~/Plugins/TagsInput/tagsinput.js",
                      "~/Plugins/moment.min.js",
                      "~/Plugins/DataTables/datatables.min.js",
                      "~/Plugins/fullcalendar-3.8.2/fullcalendar.min.js",
                      "~/Plugins/fullcalendar-3.8.2/locale-all.js",
                      "~/Plugins/jquery-ui-1.12.1/jquery-ui.min.js"
                      ));
            bundles.Add(new StyleBundle("~/bundles/LayoutCss").Include(
                      "~/Content/Layout.css",
                      "~/Content/Consulta.css",
                      "~/Content/animate.min.css",
                      "~/Plugins/FontAwesome/css/font-awesome.min.css",
                      "~/Plugins/TagsInput/tagsinput.css",
                      "~/Plugins/DataTables/datatables.min.css",
                      "~/Plugins/fullcalendar-3.8.2/fullcalendar.min.css",
                      "~/Plugins/jquery-ui-1.12.1/jquery-ui.min.css"));
        }
    }
}
