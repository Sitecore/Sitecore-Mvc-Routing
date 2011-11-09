<%@ Import Namespace="Sitecore.MvcRouting.Extensions" %>
<%@ Application Language='C#' %>

<script RunAt="server">

    public void Application_Start(object sender, EventArgs e)
    {
        RegisterRoutes(RouteTable.Routes);
    }

    private static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapSitecoreRoute("booksExt", "{17E30F07-67D0-457F-9B18-B0B2DB435081}/{bookName}/{chapterName}/{pageNumber}.{extension}", new { bookName = string.Empty, chapterName = string.Empty, pageNumber = string.Empty, extension = string.Empty });
        routes.MapSitecoreRoute("books", "{17E30F07-67D0-457F-9B18-B0B2DB435081}/{bookName}/{chapterName}/{pageNumber}", new { bookName = string.Empty, chapterName = string.Empty, pageNumber = string.Empty });                                                                    //Sitecore 
    }

</script>
