using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
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
                var path = string.Empty;

                if (handler is MvcRoutingHttpHandler)
                {
                    handler = handler as MvcRoutingHttpHandler;

                    path = MainUtil.DecodeName(((MvcRoutingHttpHandler)handler).SitecoreItemPath);
                }

                var itm = args.GetItem(path);

                if (itm != null)
                {
                    SetRouteData(args, routeData);
                    return itm;
                }
            }

            return null;
        }

        private static void SetRouteData(HttpRequestArgs args, RouteData routeData)
        {
            var dict = new Dictionary<string, string>();

            if(routeData.Values.Count > 0)
            {
                dict = routeData.Values.ToDictionary(x => x.Key, x => x.Value.ToString());
            }

            args.Context.Items.Add("RouteData", dict);
        }
    }
}