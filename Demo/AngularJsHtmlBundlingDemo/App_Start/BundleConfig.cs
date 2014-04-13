using System.Web.Optimization;
using HtmlBundling;

namespace AngularJsHtmlBundlingDemo
{
    public class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Content/app.js"));
            bundles.Add(new AngularJsHtmlBundle("~/bundles/myapp/html").Include("~/Content/*.html"));
        }
    }
}