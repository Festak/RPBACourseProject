﻿using System.Web;
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
                 "~/Scripts/scripts/tagcloud.js",
                      "~/Scripts/scripts/counter.js",
                 "~/Scripts/scripts/jqmenu.js",
                  "~/Scripts/scripts/registerLoginMenu.js"

                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

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
                
                "~/app/main.js",
              "~/app/controllers/tag.js",
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
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/loginRegister.css",
                      "~/Content/counter.css",
                      "~/Content/jqmenu.css",
                      "~/Content/font-awesome.min.css",
                       "~/Content/font-awesome-animation.css",
                      "~/Content/summernote.css",
                      "~/Content/clean-blog.css",
                      "~/Content/star-rating.css"

                      ));
        }
    }
}
