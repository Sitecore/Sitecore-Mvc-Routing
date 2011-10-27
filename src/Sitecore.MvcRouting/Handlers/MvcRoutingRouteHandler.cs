using System;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Routing;

namespace Sitecore.MvcRouting.Handlers
{
    public class MvcRoutingRouteHandler : IRouteHandler
    {
        private readonly string _sitecoreItemPath;

        public MvcRoutingRouteHandler(string sitecoreItemPath)
        {
            if (string.IsNullOrEmpty(sitecoreItemPath))
            {
                throw new ArgumentNullException("sitecoreItemPath");
            }

            _sitecoreItemPath = sitecoreItemPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (Context.Site == null)
            {
                throw new ArgumentException("Sitecore not initialised.");
            }

            return (new MvcRoutingHttpHandler { RequestContext = requestContext, SitecoreItemPath = Context.Site.StartPath + _sitecoreItemPath });
        }
    }
}