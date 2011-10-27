<%@ Import Namespace="Sitecore.MvcRouting.Extensions" %>
<%@ Application Language='C#' %>

<script RunAt="server">

    public void Application_Start(object sender, EventArgs e)
    {
        RegisterRoutes(RouteTable.Routes);
    }

    private static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapSitecoreRoute("booksExt", "books/{bookname}/{chapter}/{page}.{extension}", "/books", new { bookname = string.Empty, chapter = string.Empty, page = string.Empty, extension = string.Empty });
        routes.MapSitecoreRoute("books", "books/{bookname}/{chapter}/{page}", "/books", new { bookname = string.Empty, chapter = string.Empty, page = string.Empty, extension = string.Empty });                                                                    //Sitecore 
    }

</script>
