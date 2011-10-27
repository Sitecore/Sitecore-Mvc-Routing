using System.Web;
using System.Web.Routing;

namespace Sitecore.MvcRouting.Handlers
{
    public class MvcRoutingHttpHandler : IHttpHandler
    {
        public RequestContext RequestContext { get; set; }
        public string SitecoreItemPath { get; set; }
   
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            return;
        }

        #endregion
    }
}