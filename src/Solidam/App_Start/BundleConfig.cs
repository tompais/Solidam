using System.Web.Optimization;

namespace Solidam
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
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css", "~/Content/fontawesome/css/all.min.css", "~/Content/alertifyjs/alertify.min.css", "~/Content/alertifyjs/themes/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include("~/Scripts/moment.min.js", "~/Scripts/moment-timezone-with-data.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include("~/Scripts/umd/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.mousewheel").Include("~/Scripts/jquery-mousewheel/jquery.mousewheel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/base64").Include("~/Scripts/js-base64/base64.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include("~/Scripts/fontawesome/js/all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/consts").Include("~/Scripts/consts.js"));

            bundles.Add(new ScriptBundle("~/bundles/utils").Include("~/Scripts/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/baseLayout").Include("~/Scripts/Layouts/BaseLayout.js"));

            bundles.Add(new StyleBundle("~/bundles/dataTablesStyles").Include("~/Content/DataTables/css/dataTables.bootstrap4.min.css", "~/Content/DataTables/css/responsive.bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTablesScripts").Include("~/Scripts/DataTables/jquery.dataTables.min.js", "~/Scripts/DataTables/dataTables.bootstrap4.min.js"));
        }
    }
}