using System;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Routing;

namespace Sitecore.MvcRouting.Handlers
{
    public class MvcRoutingRouteHandler : IRouteHandler
    {
        private readonly string _sitecoreItemId;

        public MvcRoutingRouteHandler(string sitecoreItemId)
        {
            if (string.IsNullOrEmpty(sitecoreItemId))
            {
                throw new ArgumentNullException("sitecoreItemId");
            }

            _sitecoreItemId = sitecoreItemId;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            return (new MvcRoutingHttpHandler { RequestContext = requestContext, SitecoreItemId = _sitecoreItemId });
        }
    }
}