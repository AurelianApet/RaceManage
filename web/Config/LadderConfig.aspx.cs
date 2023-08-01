using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using DataAccess;
using Ronaldo.common;

public partial class Config_LadderConfig : Ronaldo.uibase.PageBase
{
    protected override void LoadData()
    {
        base.LoadData();

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETCONFIG, new string[] { "@type" }, new object[] { Constants.GAMETYPE_LADDER });
    }

    protected override void BindData()
    {
        base.BindData();

        System.Collections.Generic.Dictionary<string, string> dicConfigs = new System.Collections.Generic.Dictionary<string, string>();
        for (int i = 0; i < DataSetUtil.RowCount(PageDataSource); i++)
            dicConfigs.Add(DataSetUtil.RowStringValue(PageDataSource, "conf_name", i), DataSetUtil.RowStringValue(PageDataSource, "conf_value", i));

        string[] strTmp = null;

        rblGameResult.SelectedValue = dicConfigs["ladder_result_mode"];
        strTmp = dicConfigs["ladder_betratio"].Split(';');
        tbxRatio_statrpos.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_34.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio_oddeven.Text = string.Format("{0:F2}", strTmp[2]);
        tbxMultiRatio.Text = string.Format("{0:F2}", strTmp[3]);
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "ladder_result_mode", rblGameResult.SelectedValue });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "ladder_betratio",
                string.Format("{0};{1};{2};{3}", tbxRatio_statrpos.Text, tbxRatio_34.Text, tbxRatio_oddeven.Text, tbxMultiRatio.Text) });
        }
        catch (Exception ex)
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR + "\\n" + ex.Message);
            return;
        }

        Response.Redirect("/Config/LadderConfig.aspx");
    }
}
