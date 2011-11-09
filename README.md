MVC Routing with Sitecore
=========================
This project allows you to use `System.Web.Routing` routing engine (ala ASP.NET MVC) with Sitecore.

Building
--------
* Place your `Sitecore.Kernel.dll` in the '\lib' directory.
* Open the solution and build! 

Installation
------------
* Copy the include file from `\conf\App_Config\Include\MvcRouting.config` to the `\App_config\Includes` directory of your Sitecore installation.
* Copy the `Sitecore.MvcRouting.dll` to the `\bin` directory of your Sitecore installation.

Requirements
------------
* This code does not require ASP.NET MVC to be installed to be able to use the routing functionality.
* If its not installed the website must be ran under **.NET 4.0**.
* To use with .NET 3.5, ASP.NET MVC **must** be installed (as it needs `System.Web.Routing` to run).

Usage Example
-----

###Global.asax###
This is where the magic happens, with MVC you define routes by using `MapRoute`. 
To work with Sitecore we have `MapSitecoreRoute` ..

    private static void RegisterRoutes(RouteCollection routes)
    {
		routes.MapSitecoreRoute("books", "{17E30F07-67D0-457F-9B18-B0B2DB435081}/{bookname}/{chapter}/{page}");
    }

.. the main difference here is we have to tell Sitecore which item we want to map these values to, also, we also dont want to hard-code a path incase a user decided to rename an item.
This is why we provide a GUID as the first part of the URL. It's a little ugly but it provides better protection against an item being moved or renamed.

We are still able to provide defaults for the matched values..

    routes.MapSitecoreRoute(
  	  	"books",
			  "{17E30F07-67D0-457F-9B18-B0B2DB435081}/{bookname}/{chapter}/{page}",
			  "/books",
			  new { 
				  bookname = string.Empty,
				  chapter = string.Empty,
				  page = 1
				}
			);
      
You can also add regular `Ignore` paths, the routing resolver will not run if these paths match.

    routes.Ignore("books/");

You can add `Ignore` paths automatically by using this setting (default is `false`).

	<!-- Should the resolver add an automatic Ignore path for each matched route -->
    <setting name="MvcRouting.AddBaseIgnoreForRoutes" value="false" />
    
If this is set to `true` then, in this example, an Ignore of "books/" would be added to the `RouteCollection`.
This is very much dependant on how you want to set your application up.

###Retreiving values###

####XSLT####

**NOTE** : Remember to add the namespace `xmlns:scr="http://www.sitecore.net/mvc/routing"` at the top of your XSLT file
and add `scr` into the `exclude-result-prefixes` list.

You can retrieve individual values using `RouteDataValue(<name>)`  

    <div>
      Book Name : <xsl:value-of select="scr:RouteDataValue('bookname')"/> <br />
      Book Chapter : <xsl:value-of select="scr:RouteDataValue('chapter')"/> <br />
      Book Page : <xsl:value-of select="scr:RouteDataValue('page')"/> <br />
    </div>

You can retrieve the RouteData as an XPathNodeIterator using `RouteData()`

    <xsl:if test="scr:MatchedRoute()">
      <table border="1px">
        <tr>
          <th>Key</th>
          <th>Value</th>
        </tr>
        <xsl:for-each select="scr:RouteData()/@*">
          <tr>
            <td>
              <xsl:value-of select="name()" />
            </td>
            <td>
              <xsl:value-of select="." />
            </td>
          </tr>
        </xsl:for-each>
      </table>
    </xsl:if>

You can also retrieve the name of the currently matched route using `MatchedRoute()`

    <xsl:when test="scr:MatchedRoute() = 'books'">

See the `\doc\Example` directory for these quick examples.

Fork Me! / Todo
-----
I would like this to be a community effort so please feel free to fork this project and send me some pull requests for any improvements.

Things to be done:
 
* Add user controls to match the XSLT extension methods - Currently no support for using the RouteData in C# User Controls .. go on .. contrib something :D