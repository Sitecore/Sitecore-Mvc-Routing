using System;
using System.Web.Routing;
using Sitecore.MvcRouting.Handlers;

namespace Sitecore.MvcRouting.Extensions
{
    public static class MvcRoutingExtensions
    {
        public static Route MapSitecoreRoute(this RouteCollection routes, string name, string url, string sitecoreItemPath)
        {
            return MapSitecoreRoute(routes, name, url, null, sitecoreItemPath);
        }

        public static Route MapSitecoreRoute(this RouteCollection routes, string name, string url, object defaults, string sitecoreItemPath)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (sitecoreItemPath == null)
            {
                throw new ArgumentNullException("sitecoreItemPath");
            }

            var route = new Route(url, new MvcRoutingRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
            };

            route.Defaults.Add("sitecoreItemPath", sitecoreItemPath);
            route.Defaults.Add("routeName", name);

            routes.Add(name, route);

            return route;
        }
    }
}