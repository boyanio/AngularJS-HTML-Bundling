using System.Web.Optimization;

namespace HtmlBundling
{
    public class AngularJsHtmlBundle : Bundle
    {
        public AngularJsHtmlBundle(string moduleName, string virtualPath)
            : base(virtualPath, null, new[] { (IBundleTransform)new AngularJsHtmlCombine(moduleName) })
        {
        }
    }
}