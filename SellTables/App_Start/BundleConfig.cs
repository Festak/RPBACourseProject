using System.Web;
using System.Web.Optimization;

namespace SellTables
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.js",
                        "~/Scripts/scripts/jquery.tabslideout.v1.2.js",
                 "~/Scripts/scripts/tagcloud.js","~/Scripts/scripts/tagscloud.js",
                      "~/Scripts/scripts/counter.js",
                 "~/Scripts/scripts/jqmenu.js",
                  "~/Scripts/scripts/registerLoginMenu.js"

                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scroll").Include(
                       "~/Scripts/scripts/scroll.js"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                          "~/Scripts/angular.js",
                          "~/Scripts/angular-route.js",
                          "~/Scripts/angular-cookies.js",
                          "~/Scripts/ng-inline-edit.js"
                          )); 

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/scripts/ng-sortable.js",
                "~/Scripts/scripts/Sortable.js",
                "~/app/main.js",
              "~/app/controllers/tag.js",
               "~/app/controllers/cookies.js",
              "~/app/controllers/admin.js",
                "~/app/controllers/creative.js",
                "~/app/controllers/user.js",
                "~/app/controllers/widthChange.js",
                "~/app/controllers/tag.js",
                 "~/app/controllers/chapter.js",
                "~/Scripts/scripts/clean-blog.js",
                "~/Scripts/scripts/star-rating.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/scripts/redipadding.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/loginRegister.css",
                      "~/Content/counter.css",
                      "~/Content/circlemenu.css",
                      "~/Content/font-awesome.min.css",
                       "~/Content/font-awesome-animation.css",
                      "~/Content/summernote.css",
                      "~/Content/clean-blog.css",
                      "~/Content/star-rating.css",
                      "~/Content/creatives.css"

                      ));

            bundles.Add(new StyleBundle("~/Content/somestyles").Include(
                      "~/Content/somestyles/userpage.css"    

                      ));

            bundles.Add(new ScriptBundle("~/bundles/tags").Include(
                      "~/Scripts/scripts/TagsScript.js"
                      
                      ));
            bundles.Add(new StyleBundle("~/Content/tags").Include(
                      "~/Content/somestyles/TagsStyles.css"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/css/basic.css",
                     "~/Scripts/dropzone/css/dropzone.css"));
        }
    }
}
