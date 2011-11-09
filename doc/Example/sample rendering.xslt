<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:sc="http://www.sitecore.net/sc"
  xmlns:sql="http://www.sitecore.net/sql"
  xmlns:scr="http://www.sitecore.net/mvc/routing"
  exclude-result-prefixes="sc sql scr">

  <!-- output directives -->
  <xsl:output method="html" indent="no" encoding="UTF-8"  />

  <!-- sitecore parameters -->
  <xsl:param name="lang" select="'en'"/>
  <xsl:param name="id" select="''"/>
  <xsl:param name="sc_item"/>
  <xsl:param name="sc_currentitem"/>

  <!-- entry point -->
  <xsl:template match="*">
    <div>
      <h1>
        <sc:text field="title"/>
      </h1>
      <div>
        <sc:text field="text" />
      </div>
    </div>
	<hr />
    <div>
      Book Name : <xsl:value-of select="scr:RouteDataValue('bookName')"/> <br />
      Book Chapter : <xsl:value-of select="scr:RouteDataValue('chapterName')"/> <br />
      Book Page : <xsl:value-of select="scr:RouteDataValue('pageNumber')"/> <br />
    </div>
    <hr />
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

  </xsl:template>

</xsl:stylesheet>
