using System.Web;
using System.Web.Optimization;

namespace Businessdevweb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/Material").Include(
                      "~/Scripts/material.js",
                      "~/Scripts/jquery.treegrid.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/Slider").Include(
          "~/Scripts/slick/slick.min.js",
          "~/Scripts/slick/jquery-migrate-1.2.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/material-fa.css",
                "~/Content/material.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/material-fa.css",
                "~/Content/material.css",
                 "~/Content/jquery.treegrid.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/Slider").Include(
          "~/Content/slick/slick.css",
          "~/Content/slick/slick-theme.css"));

                  // Code removed for clarity.
                  BundleTable.EnableOptimizations = false;
        }
    }
}
