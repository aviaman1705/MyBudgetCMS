using System.Web;
using System.Web.Optimization;

namespace MyBudgetCMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            //Render admin area css files
            bundles.Add(new StyleBundle("~/bundles/admin/css").Include(
                                                        "~/Content/bootstrap.min.css",
                                                        "~/Content/font-awesome.min.css",
                                                        "~/Content/dataTables.bootstrap4.css",
                                                        "~/Content/sb-admin.css"
                                                        ));

            //Render modernizr scripts
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            //Render admin area js files
            bundles.Add(new ScriptBundle("~/bundles/admin/scripts").Include(
                                                //"~/Scripts/jquery-1.10.2.min.js",
                                                 "~/Scripts/bootstrap.min.js",
                                                "~/Scripts/admin.js",
                                                "~/Scripts/sb-admin.min.js"
                                             ));

            //Render login view css files
            bundles.Add(new StyleBundle("~/login/css").Include(
                                                         "~/Content/login.css",
                                                         "~/Content/bootstrap.min.css"
                                                     ));
            //Render login view js files
            bundles.Add(new ScriptBundle("~/login/scripts").Include(
                                                      "~/Scripts/jquery-3.3.1.slim.min.js",
                                                      "~/Scripts/popper.min.js",
                                                      "~/Scripts/bootstrap.min.js"
                                                  ));
        }
    }
}
