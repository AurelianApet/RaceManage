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

public partial class Config_MainConfig : Ronaldo.uibase.PageBase
{
    protected override void LoadData()
    {
        base.LoadData();

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETCONFIG);
    }

    protected override void BindData()
    {
        base.BindData();

        System.Collections.Generic.Dictionary<string, string> dicConfigs = new System.Collections.Generic.Dictionary<string, string>();
        for (int i = 0; i < DataSetUtil.RowCount(PageDataSource); i++)
            dicConfigs.Add(DataSetUtil.RowStringValue(PageDataSource, "conf_name", i), DataSetUtil.RowStringValue(PageDataSource, "conf_value", i));

        tbxSiteTitle.Text = dicConfigs["title"];
        chkMemberJoin.Checked = Convert.ToInt32(dicConfigs["member_join"]) == 1;
        tbxPageRows.Text = dicConfigs["page_rows"];
        tbxLoginMinutes.Text = dicConfigs["login_minutes"];
        tbxInterceptIP.Text = dicConfigs["intercept_ip"];
        tbxManageIP.Text = dicConfigs["manage_ip"];
        tbxProhibitID.Text = dicConfigs["prohibit_id"];

        string[] strTmp = null;
        strTmp = dicConfigs["money_auto_zfb"].Split(';');
        rblZhifubaoUse.SelectedValue = strTmp[0];
        tbxZhifubaoInfo.Text = strTmp[1];
        tbxZhifubaoKey.Text = strTmp[2];
        tbxZhifubaoPartner.Text = strTmp[3];

        strTmp = dicConfigs["money_auto_wx"].Split(';');
        rblWeixinUse.SelectedValue = strTmp[0];
        tbxWeixinAppid.Text = strTmp[1];
        tbxWeixinMchid.Text = strTmp[2];
        tbxWeixinAppsecret.Text = strTmp[3];
        tbxWeixinShopkey.Text = strTmp[4];

        strTmp = dicConfigs["money_bank_gsyh"].Split(';');
        rblBankGsyhUse.SelectedValue = strTmp[0];
        tbxBankGsyhName.Text = strTmp[1];
        tbxBankGsyhNum.Text = strTmp[2];

        strTmp = dicConfigs["money_bank_cft"].Split(';');
        rblBankCftUse.SelectedValue = strTmp[0];
        tbxBankCftName.Text = strTmp[1];
        tbxBankCftNum.Text = strTmp[2];

        strTmp = dicConfigs["money_bank_zfb"].Split(';');
        rblBankZfbUse.SelectedValue = strTmp[0];
        tbxBankZfbName.Text = strTmp[1];
        tbxBankZfbNum.Text = strTmp[2];
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "title", tbxSiteTitle.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "page_rows", int.Parse(tbxPageRows.Text).ToString() });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "member_join", (chkMemberJoin.Checked) ? "1" : "0" });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "login_minutes", int.Parse(tbxLoginMinutes.Text).ToString() });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "intercept_ip", tbxInterceptIP.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "manage_ip", tbxManageIP.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "prohibit_id", tbxProhibitID.Text });
            
            if (rblZhifubaoUse.SelectedValue == "1" && (string.IsNullOrEmpty(tbxZhifubaoInfo.Text) || string.IsNullOrEmpty(tbxZhifubaoKey.Text) || string.IsNullOrEmpty(tbxZhifubaoPartner.Text)))
            {
                ShowMessageBox(Resources.Err.ERR_ZHIFUBAO_INFO);
                return;
            }
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, 
                new object[] { "money_auto_zfb", string.Format("{0};{1};{2};{3}", rblZhifubaoUse.SelectedValue, tbxZhifubaoInfo.Text, tbxZhifubaoKey.Text, tbxZhifubaoPartner.Text) });

            if (rblWeixinUse.SelectedValue == "1" && (string.IsNullOrEmpty(tbxWeixinAppid.Text) || string.IsNullOrEmpty(tbxWeixinMchid.Text) || string.IsNullOrEmpty(tbxWeixinAppsecret.Text) || string.IsNullOrEmpty(tbxWeixinShopkey.Text)))
            {
                ShowMessageBox(Resources.Err.ERR_WEIXIN_INFO);
                return;
            }
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" },
                new object[] { "money_auto_wx", string.Format("{0};{1};{2};{3};{4}", rblWeixinUse.SelectedValue, tbxWeixinAppid.Text, tbxWeixinMchid.Text, tbxWeixinAppsecret.Text, tbxWeixinShopkey.Text) });

            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" },
                new object[] { "money_bank_gsyh", string.Format("{0};{1};{2};", rblBankGsyhUse.SelectedValue, tbxBankGsyhName.Text, tbxBankGsyhNum.Text) });

            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" },
                new object[] { "money_bank_cft", string.Format("{0};{1};{2};", rblBankCftUse.SelectedValue, tbxBankCftName.Text, tbxBankCftNum.Text) });

            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" },
                new object[] { "money_bank_zfb", string.Format("{0};{1};{2};", rblBankZfbUse.SelectedValue, tbxBankZfbName.Text, tbxBankZfbNum.Text) });
            
        }
        catch(Exception ex)
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR + "\\n" + ex.Message);
            return;
        }

        Response.Redirect("/Config/MainConfig.aspx");
    }

}
