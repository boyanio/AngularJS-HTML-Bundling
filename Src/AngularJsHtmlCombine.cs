using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace HtmlBundling
{
    public class AngularJsHtmlCombine : IBundleTransform
    {
        private const string JsContentType = "text/javascript";

        public virtual void Process(BundleContext context, BundleResponse response)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (response == null)
                throw new ArgumentNullException("response");

            if (!context.EnableOptimizations)
            {
                response.Content = string.Empty;
                return;
            }

            string appName = GetAppName(context);
            if (String.IsNullOrWhiteSpace(appName))
            {
                response.Content = "// No or wrong app name defined";
                response.ContentType = JsContentType;
                return;
            }

            var contentBuilder = new StringBuilder();
            contentBuilder.Append("(function(){");
            contentBuilder.AppendFormat("angular.module('{0}').run(['$templateCache',function(t){{", appName);

            foreach (BundleFile file in response.Files)
            {
                string fileId = VirtualPathUtility.ToAbsolute(file.IncludedVirtualPath);
                string filePath = HttpContext.Current.Server.MapPath(file.IncludedVirtualPath);
                string fileContent = File.ReadAllText(filePath);

                contentBuilder.AppendFormat("t.put({0},{1});",
                    JsonConvert.SerializeObject(fileId),
                    JsonConvert.SerializeObject(fileContent));
            }

            contentBuilder.Append("}]);");
            contentBuilder.Append("})();");

            response.Content = contentBuilder.ToString();
            response.ContentType = JsContentType;
        }

        private static string GetAppName(BundleContext context)
        {
            // TODO: maybe use a better way to determine the app name. Query Strings are problematic with bundles
            Match m = Regex.Match(context.BundleVirtualPath, "/([a-zA-Z0-9_]+)/html");
            return m.Success ? m.Groups[1].Value : null;
        }
    }
}