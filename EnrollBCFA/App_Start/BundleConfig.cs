using System.Web;
using System.Web.Optimization;

namespace EnrollBCFA
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(

                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(

                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/bootstrap-theme.min.css",
                      "~/Content/css/jquery-ui.min.css",

                      // Programmer's CSS

                      "~/Content/css/navbar.css", // Navbar CSS
                      "~/Content/css/login.css", // Login View CSS
                      "~/Content/css/Site.css", // Shared Views CSS
                      "~/Content/css/subject.css",
                      "~/Content/css/teacher.css",
                      "~/Content/css/dashboard.css",
                      "~/Content/css/student.css",
                      "~/Content/css/academicyear.css",
                      "~/Content/css/schedules.css",
                      "~/Content/css/section.css",
                      "~/Content/css/enrollment.css",
                      "~/Content/css/enrolledstudent.css",
                      "~/Content/css/classlist.css",
                      "~/Content/css/requirement.css"
                      ));
        }
    }
}
