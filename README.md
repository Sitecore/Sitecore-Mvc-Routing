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

Usage
-----
See the `\doc\Example` directory for a quick example. (More detailed example coming soon).

Todo
-----
* Add user controls to match the XSLT extension methods