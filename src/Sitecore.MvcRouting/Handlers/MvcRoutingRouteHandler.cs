using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Routing;

namespace Sitecore.MvcRouting.Handlers
{
    public class MvcRoutingRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var path = requestContext.RouteData.GetRequiredString("sitecoreItemPath");

            if (!string.IsNullOrEmpty(path))
            {
                if (Context.Site != null)
                {
                    requestContext.RouteData.Values["sitecoreItemPath"] = Context.Site.StartPath + path;
                }
            }

            return (new MvcRoutingHttpHandler { RequestContext = requestContext });
        }
    }
}