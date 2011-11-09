using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using Sitecore.Xml.Xsl;

namespace Sitecore.MvcRouting.Extensions
{
    public class MvcRoutingXslExtensions : XslHelper
    {
        public string RouteDataValue(string key)
        {
            var routeData = HttpContext.Current.Items["RouteData"];

            if (routeData == null) return (string.Empty);

            var routeDataDict = (Dictionary<string, string>)routeData;

            return routeDataDict.ContainsKey(key) ? routeDataDict[key] : string.Empty;
        }

        public string MatchedRoute()
        {
            var routeData = HttpContext.Current.Items["RouteData"];
            if (routeData == null) return string.Empty;
            var routeDataDict = (Dictionary<string, string>)routeData;
            return routeDataDict.ContainsKey("routeName") ? routeDataDict["routeName"] : string.Empty;
        }

        public XPathNodeIterator RouteData()
        {
            var packet = new XDocument();
            packet.Add(new XElement("values"));

            var routeData = HttpContext.Current.Items["RouteData"];

            if (routeData == null) return GetChildIterator(packet);

            foreach (var kv in (Dictionary<string,string>)routeData)
            {
                var element = new XElement("value");
                element.SetAttributeValue(kv.Key, kv.Value);
                packet.Descendants("values").Single().Add(element);    
            }

            return GetChildIterator(packet);
        }

        private XPathNodeIterator GetChildIterator(XDocument packet)
        {
            var xpathNavigator = packet.CreateNavigator();
            xpathNavigator.MoveToRoot();
            xpathNavigator.MoveToFirstChild();
            return xpathNavigator.SelectChildren(XPathNodeType.Element);
        }
    }
}