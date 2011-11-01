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
* To use with .NET 3.5 ASP.NET MVC **must** be installed (as it needs `System.Web.Routing` to run).

Usage Example
-----

###Global.asax###
This is where the magic happens, with MVC you define routes by using `MapRoute`. 
To work with Sitecore we have `MapSitecoreRoute` ..

    routes.MapSitecoreRoute("books", "books/{bookname}/{chapter}/{page}", "/books");

.. the main difference here is we have to tell Sitecore which item we want to map these values to, so we provide the Sitecore item path as the last parameter.

We are still able to provide defaults for the matched values..

    routes.MapSitecoreRoute(
  	  	"books",
			  "books/{bookname}/{chapter}/{page}",
			  "/books",
			  new { 
				  bookname = string.Empty,
				  chapter = string.Empty,
				  page = string.Empty
				}
			);
      
You can also add `IgnoreRoute` paths, the routing resolver will not run if these paths do not match.

    routes.Ignore("books/");
    
###Retreiving values###

####XSLT####

You can retrieve individual values using `RouteDataValue(<name>)`  

    <div>
      Book Name : <xsl:value-of select="scr:RouteDataValue('bookname')"/> <br />
      Book Chapter : <xsl:value-of select="scr:RouteDataValue('chapter')"/> <br />
      Book Page : <xsl:value-of select="scr:RouteDataValue('page')"/> <br />
    </div>

You can retrieve the RouteData as an XPathNodeIterator using `RouteData()`

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

You can also retrieve the name of the currently matched route using `MatchedRoute()`

    <xsl:when test="scr:MatchedRoute() = 'books'">

See the `\doc\Example` directory for these quick examples.


Todo
-----
* Add user controls to match the XSLT extension methods