using System;
using System.Web.Routing;
using Sitecore.MvcRouting.Handlers;

namespace Sitecore.MvcRouting.Extensions
{
    public static class MvcRoutingExtensions
    {
        /// <summary>Provides a way to define routes for Sitecore applications.</summary>
        /// <returns>The route that is added to the route collection.</returns>
        /// <param name="routes">The route collection.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="sitecoreItemPath">The path to the sitecore item.</param>
        public static Route MapSitecoreRoute(this RouteCollection routes, string routeName, string routeUrl, string sitecoreItemPath)
        {
            return routes.MapSitecoreRoute(routeName, routeUrl, sitecoreItemPath, null, null, null);
        }

        /// <summary>Provides a way to define routes for Sitecore applications.</summary>
        /// <returns>The route that is added to the route collection.</returns>
        /// <param name="routes">The route collection.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="sitecoreItemPath">The path to the sitecore item.</param>
        /// <param name="defaults">Default values for the route parameters.</param>
        public static Route MapSitecoreRoute(this RouteCollection routes, string routeName, string routeUrl, string sitecoreItemPath, object defaults)
        {
            return routes.MapSitecoreRoute(routeName, routeUrl, sitecoreItemPath, defaults, null, null);
        }

        /// <summary>Provides a way to define routes for Sitecore applications.</summary>
        /// <returns>The route that is added to the route collection.</returns>
        /// <param name="routes">The route collection.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="sitecoreItemPath">The path to the sitecore item.</param>
        /// <param name="defaults">Default values for the route parameters.</param>
        /// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
        public static Route MapSitecoreRoute(this RouteCollection routes, string routeName, string routeUrl, string sitecoreItemPath, object defaults, object constraints)
		{
			return routes.MapSitecoreRoute(routeName, routeUrl, sitecoreItemPath, defaults, constraints, null);
		}

        /// <summary>Provides a way to define routes for Sitecore applications.</summary>
        /// <returns>The route that is added to the route collection.</returns>
        /// <param name="routes">The route collection.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="routeUrl">The URL pattern for the route.</param>
        /// <param name="sitecoreItemPath">The path to the sitecore item.</param>
        /// <param name="defaults">Default values for the route parameters.</param>
        /// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
        /// <param name="dataTokens">Values that are associated with the route that are not used to determine whether a route matches a URL pattern.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="routeUrl" /> parameter is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="sitecoreItemPath" /> parameter is null.</exception>
        public static Route MapSitecoreRoute(this RouteCollection routes, string routeName, string routeUrl, string sitecoreItemPath, object defaults, object constraints, object dataTokens)
        {
            if (routeUrl == null)
            {
                throw new ArgumentNullException("routeUrl");
            }

            if (sitecoreItemPath == null)
            {
                throw new ArgumentNullException("sitecoreItemPath");
            }

            var route = new Route(routeUrl, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new RouteValueDictionary(dataTokens), new MvcRoutingRouteHandler(sitecoreItemPath));
            route.Defaults.Add("routeName", routeName);
            routes.Add(routeName, route);
            return route;
        }
    }
}
