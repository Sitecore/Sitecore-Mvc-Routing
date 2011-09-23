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

            if (((Context.Item != null) || (Context.Database == null)) || (args.Url.ItemPath.Length == 0)) return;

            var item = ResolveRouteTable(args);

            if (item == null)
            {
                return;
            }

            Context.Item = item;
        }

        private static Item ResolveRouteTable(HttpRequestArgs args)
        {
            var httpContextWrapper = new HttpContextWrapper(args.Context);
            var routeData = RouteTable.Routes.GetRouteData(httpContextWrapper);

            if (routeData == null)
            {
                return null;
            }

            if (!routeData.Values.ContainsKey("sitecoreItemPath"))
            {
                return null;
            }

            var handler = routeData.RouteHandler.GetHttpHandler(new RequestContext(httpContextWrapper, routeData));

            if (handler != null)
            {
                var path = string.Empty;

                if (handler.GetType() == typeof(MvcRoutingHttpHandler))
                {
                    handler = handler as MvcRoutingHttpHandler;

                    if (handler == null) return null;

                    path = MainUtil.DecodeName(((MvcRoutingHttpHandler)handler).RequestContext.RouteData.GetRequiredString("sitecoreItemPath"));
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