<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version = "1.0" xmlns:xsl = "http://www.w3.org/1999/XSL/Transform">
  <xsl:template match = "/">
    <!-- HTML tags 
         Used for formatting purpose. Processor will skip them and browser 
            will simply render them. 
      -->
    <xsl:variable name="DueDate">
      Not Set
    </xsl:variable>

    <head>
      <style>
        [[InlineCSS]]
      </style>
    </head>
    <html>
      <body>
        <h2>
          Task Completed in the <xsl:value-of select="/Work/Project"/> project
        </h2>
        <table border="0" align="center" width="700">
          <tr>
            <td width="300">
              <b>Task Name :</b>
              <xsl:value-of select="/Work/Name"/>
            </td>
            <td width="400">
              <b>Completed by:</b>
              <xsl:value-of select="/Work/Owner"/>
            </td>
          </tr>
          <tr>
            <td width="300">
              <b>Created :</b>
              <xsl:value-of select="/Work/Created"/>
            </td>
            <td width="400">
              <b>Due :</b>
              <xsl:value-of select="/Work/DueDate"/>
            </td>
          </tr>
        </table>
        <h3>Description</h3>
        <table align="center" width="700">
          <tr>
            <TD></TD>
          </tr>
          <tr>
            <td>
              <xsl:value-of select="/Work/Description"/>
            </td>
          </tr>
        </table>
        <h3>Comments</h3>
        <table align="center" width="700">
          <xsl:for-each select="/Work/Comments/Comment">
            <tr>
              <td>
                <b>
                  <xsl:value-of select = "Owner"/> :
                </b>
                <xsl:value-of select = "Date"/>
              </td>
            </tr>
            <tr>
              <td width="700">
                <xsl:value-of select = "Comment"/>
              </td>
            </tr>
            <tr>
              <td></td>
              <td></td>
            </tr>
          </xsl:for-each>
        </table>
        <br></br>
        <table align="center" width="700">
          <tr>
            <TD>
              <a href="www.yourexecutionsolution.com">
                <i>Powered by Your Execution Solution</i>
              </a>
            </TD>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
