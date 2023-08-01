using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataAccess;
using Ronaldo.common;

public partial class ManageMaster : Ronaldo.uibase.MasterPageBase
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initControls();
        }
    }
    protected void initControls()
    {
        ltlNickName.Text = string.Format(Resources.Msg.MSG_WELCOMEUSER, "<font color='red'><b>" + CurrentPage.AuthUser.NickName + "</b></font>");

        System.Data.DataSet dsReq = CurrentPage.DBConn.RunStoreProcedure(Constants.SP_GETUNREADREQUEST);
        ltlChargeRequest.Text = DataSetUtil.RowIntValue(dsReq, "charge", 0).ToString();
        ltlChargeStandby.Text = DataSetUtil.RowIntValue(dsReq, "chargestandby", 0).ToString();
        ltlChargeComplete.Text = DataSetUtil.RowIntValue(dsReq, "chargecomplete", 0).ToString();
        ltlDischargeRequest.Text = DataSetUtil.RowIntValue(dsReq, "discharge", 0).ToString();
        ltlDischargeStandby.Text = DataSetUtil.RowIntValue(dsReq, "dischargestandby", 0).ToString();
        ltlDischargeComplete.Text = DataSetUtil.RowIntValue(dsReq, "dischargecomplete", 0).ToString();
        ltlMember.Text = DataSetUtil.RowIntValue(dsReq, "memtotal", 0).ToString();
        ltlMemberNew.Text = DataSetUtil.RowIntValue(dsReq, "memnew", 0).ToString();
    }
}
