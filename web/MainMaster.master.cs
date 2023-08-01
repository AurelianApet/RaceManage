using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MainMaster : Ronaldo.uibase.MasterPageBase
{
    public override void outputRes2JS(string[] strNames, string[] strValues)
    {
        if (strNames.Length != strValues.Length)
            return;

        string strRet = "<script language=\"javascript\" type=\"text/javascript\">\n";
        for (int i = 0; i < strNames.Length; i++)
        {
            strRet += "var " + strNames[i] + " = \"" + strValues[i] + "\";\n";
        }
        strRet += "</script>";

        ltlScript.Text += strRet;
    }
}
