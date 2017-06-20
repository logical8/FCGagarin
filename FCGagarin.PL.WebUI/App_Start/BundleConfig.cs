using System.Web.Optimization;

namespace FCGagarin.PL.WebUI
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-multiselect.js", 
                      "~/Scripts/Custom/multiselect.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/locales/bootstrap-datepicker.ru.min.js",
                      "~/Scripts/Custom/datepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Slate/bootstrap.min.css",
                      "~/Content/bootstrap-multiselect.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/jquery.fileupload-ui-noscript.css",
                      "~/Content/jquery.fileupload-ui.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                "~/Scripts/dropzone/basic.css",
                "~/Scripts/dropzone/dropzone.css"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-file-upload").Include(
                      "~/Scripts/jquery-file-upload/jquery.ui.widget.js",
                      "~/Scripts/jquery-file-upload/jquery.fileupload.js",
                      "~/Scripts/jquery-file-upload/jquery.iframe-transport.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                "~/Scripts/dropzone/dropzone.js"));
        }
    }
}
