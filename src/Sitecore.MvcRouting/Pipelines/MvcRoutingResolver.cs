using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.MvcRouting.Handlers;
using Sitecore.Pipelines.HttpRequest;

namespace Sitecore.MvcRouting.Pipelines
{
    public class MvcRoutingResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (((Context.Database == null)) || (args.Url.ItemPath.Length == 0)) return;

            var routedItem = ResolveRouteTable(args);

            //Route matches but no sitecore item exists for that path
            if (Context.Item == null && routedItem != null)
            {
                Context.Item = routedItem;
            }
        }

        private static Item ResolveRouteTable(HttpRequestArgs args)
        {
            var httpContextWrapper = new HttpContextWrapper(args.Context);

            ResolveSitecoreRoutePaths(RouteTable.Routes);

            var routeData = RouteTable.Routes.GetRouteData(httpContextWrapper);

            if (routeData == null)
            {
                return null;
            }

            if (routeData.RouteHandler is StopRoutingHandler)
            {
                return null; //Do not process this route
            }

            var handler = routeData.RouteHandler.GetHttpHandler(new RequestContext(httpContextWrapper, routeData));

            if (handler != null)
            {
                var id = string.Empty;

                var mvcRoutingHttpHandler = handler as MvcRoutingHttpHandler;
                
                if (mvcRoutingHttpHandler != null)
                {
                    id = (mvcRoutingHttpHandler).SitecoreItemId;
                }

                var itm = args.GetItem(id);

                if (itm != null)
                {
                    SetRouteData(args, routeData);
                    return itm;
                }
            }

            return null;
        }

        /// <summary>
        /// Looks for a GUID at the start of the route and resolves it to its correct Sitecore path.
        /// </summary>
        /// <param name="routes"></param>
        private static void ResolveSitecoreRoutePaths(RouteCollection routes)
        {
            var ignorePaths = new List<string>();

            foreach (Route route in routes)
            {
                if (!route.Url.StartsWith("{"))
                {
                    continue;
                }

                var elements = route.Url.Split('/');

                if(elements.Length == 0)
                {
                    continue;
                }

                Guid guid;

                if (!Guid.TryParse(elements[0], out guid)) continue;

                var pathItem = Sitecore.Context.Database.GetItem(guid.ToString());

                if (pathItem != null)
                {
                    var options = UrlOptions.DefaultOptions;
                    options.AddAspxExtension = false;
                    options.LanguageEmbedding = LanguageEmbedding.Never;
                    var path = LinkManager.GetItemUrl(pathItem, options).TrimStart('/');
                    route.Url = route.Url.Replace(elements[0], path);

                    path = path + "/";

                    if (!ignorePaths.Contains(path))
                    {
                        ignorePaths.Add(path);
                    }
                }
            }

            var addIgnore = Sitecore.Configuration.Settings.GetSetting("MvcRouting.AddBaseIgnoreForRoutes");
            
            if(!string.IsNullOrEmpty(addIgnore) && addIgnore == "true")
            {
                foreach (var ignorePath in ignorePaths)
                {
                    routes.Insert(0, new Route(ignorePath,new StopRoutingHandler()));
                }
            }
        }

        private static void SetRouteData(HttpRequestArgs args, RouteData routeData)
        {
            var dict = new Dictionary<string, string>();

            if (routeData.Values.Count > 0)
            {
                dict = routeData.Values.ToDictionary(x => x.Key, x => x.Value.ToString());
            }

            args.Context.Items.Add("RouteData", dict);
        }
    }
}