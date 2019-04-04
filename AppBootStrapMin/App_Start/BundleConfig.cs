using System.Web;
using System.Web.Optimization;

namespace AppBootStrapMin
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            /*-- DataTables --------------------------------------------------*/

            bundles.Add(new StyleBundle("~/Content/DataTables/jqueryDataTables").Include(
                "~/Content/DataTables/css/jquery.dataTables.css",
                "~/Content/DataTables/css/dataTables.bootstrap.css",
                "~/Content/DataTables/css/dataTables.uikit.css"
            ));

            bundles.Add(new StyleBundle("~/Content/DataTables/buttonsDataTables").Include(
                "~/Content/DataTables/css/buttons.dataTables.css",
                "~/Content/DataTables/css/buttons.bootstrap.css"
            ));

            bundles.Add(new StyleBundle("~/Content/DataTables/autoFillDataTables").Include(
                "~/Content/DataTables/css/autoFill.dataTables.css",
                "~/Content/DataTables/css/autoFill.bootstrap.css"
            ));

            /*-- css DataTables responsive -----------------------------------*/
            bundles.Add(new StyleBundle("~/Content/DataTables/responsiveDataTables").Include(
                "~/Content/DataTables/css/responsive.dataTables.css",
                "~/Content/DataTables/css/responsive.bootstrap.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/DataTables/jqueryDataTables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js",
                "~/Scripts/DataTables/dataTables.uikit.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/DataTables/buttonsDataTables").Include(
                "~/Scripts/DataTables/dataTables.buttons.js",
                "~/Scripts/DataTables/dataTables.colReorder.js",
                "~/Scripts/DataTables/dataTables.scroller.js",
                "~/Scripts/DataTables/buttons.colVis.js",
                "~/Scripts/DataTables/buttons.flash.js",
                "~/Scripts/DataTables/buttons.foundation.js",
                "~/Scripts/DataTables/buttons.html5.js",
                "~/Scripts/DataTables/buttons.jqueryui.js",
                "~/Scripts/DataTables/buttons.print.js",
                "~/Scripts/DataTables/buttons.bootstrap.js"

            ));

            bundles.Add(new ScriptBundle("~/bundles/DataTables/autoFillDataTables").Include(
                "~/Scripts/DataTables/dataTables.autoFill.js",
                "~/Scripts/DataTables/autoFill.bootstrap.js"
            ));


            /*-- js DataTables responsive ------------------------------------*/
            bundles.Add(new ScriptBundle("~/bundles/DataTables/responsiveDataTables").Include(
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/Scripts/DataTables/responsive.bootstrap.js"
            ));

        }
    }
}
