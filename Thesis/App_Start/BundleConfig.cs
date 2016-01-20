﻿using System.Web;
using System.Web.Optimization;

namespace Thesis
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.js",
                        "~/Scripts/jquery-2.1.4.min.js",
                              "~/Scripts/jquery-ui.js",
                          "~/Scripts/jquery-ui.min.js",
                                  "~/Scripts/jquery-ui-1.11.4.js",
                         "~/Scripts/jquery-ui-1.11.4.min.js",
                                            "~/Scripts/jquery.validate.js",
                            "~/Scripts/jquery.validate.min.js",
                          "~/Scripts/jquery.validate-vsdoc.js",

                            "~/Scripts/jquery.validate.unobtrusive.js",
                            "~/Scripts/jquery.validate.unobtrusive.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Scripts/css").Include(
                     "~/Scripts/*.css"));
            bundles.Add(new StyleBundle("~/fonts/fontAwesome").Include("~/fonts/font-awesome.min.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
