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

using Ronaldo.common;
using DataAccess;

public partial class popMemberInfo : Ronaldo.uibase.PageBase
{
    DataSet chargeDataSource = null;
    int chargePageNumber = 0;
    DataSet dischargeDataSource = null;
    int dischargePageNumber = 0;
    DataSet loginDataSource = null;
    int loginPageNumber = 0;

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!IsPostBack)
            Master.outputRes2JS(
                new string[] { 
                    "MSG_CONFIRMDELETE"
                },
                new string[]
                {       
                    Resources.Msg.MSG_CONFIRMDELETE
                });
    }

    protected override void InitControls()
    {
        base.InitControls();

        DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST);
        ddlSite.Items.Clear();
        ddlSite.Items.Add(new ListItem("---", "0"));
        for (int i = 0; i < DataSetUtil.RowCount(dsSite); i++)
            ddlSite.Items.Add(new ListItem(DataSetUtil.RowStringValue(dsSite, "site_name", i), DataSetUtil.RowStringValue(dsSite, "id", i)));

        gvChargeHist.PageSize = 7;
        gvDischargeHist.PageSize = 7;
        gvLoginHist.PageSize = 7;

    }

    protected override void LoadData()
    {
        base.LoadData();

        long lID = 0;
        if (Request.Params["uid"] == null || !long.TryParse(Request.Params["uid"], out lID))
        {
            ShowMessageBox(Resources.Msg.MSG_NOEXISTDATA, "MemberMng.aspx?page=" + PageNumber);
            return;
        }

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
            new string[] { "@id" }, new object[] { lID });

        if (DataSetUtil.IsNullOrEmpty(PageDataSource))
        {
            ShowMessageBox(Resources.Msg.MSG_NOEXISTDATA, "MemberMng.aspx?page=" + PageNumber);
            return;
        }

        chargeDataSource = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
            new string[] { "@user_id", "@kind" }, new object[] { lID, Constants.MONEYINOUT_MODE_CHARGE });

        dischargeDataSource = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
            new string[] { "@user_id", "@kind" }, new object[] { lID, Constants.MONEYINOUT_MODE_DISCHARGE });

        loginDataSource = DBConn.RunStoreProcedure(Constants.SP_GETLOGINHISTS,
            new string[] { "@user_id" }, new object[] { lID });

        long lChargeTotalMoney = 0;
        long lDischargeTotalMoney = 0;

        if (!DataSetUtil.IsNullOrEmpty(chargeDataSource))
        {
            object objTotal = chargeDataSource.Tables[0].Compute("SUM(reqmoney)", "status=" + Constants.MONEYINOUT_STATUS_APPLY);

            lChargeTotalMoney = (objTotal == DBNull.Value) ? 0 : Convert.ToInt64(objTotal);
        }
        if (!DataSetUtil.IsNullOrEmpty(dischargeDataSource))
        {
            object objTotal = dischargeDataSource.Tables[0].Compute("SUM(reqmoney)", "status=" + Constants.MONEYINOUT_STATUS_APPLY);

            lDischargeTotalMoney = (objTotal == DBNull.Value) ? 0 : Convert.ToInt64(objTotal);
        }
        ltlTotalCharge.Text = string.Format(Resources.Str.STR_MONEYUNIT, lChargeTotalMoney);
        ltlTotalDischarge.Text = string.Format(Resources.Str.STR_MONEYUNIT, lDischargeTotalMoney);
    }

    protected override void BindData()
    {
        base.BindData();

        hdID.Value = DataSetUtil.RowLongValue(PageDataSource, "id", 0).ToString();
        tbxLoginID.Text = DataSetUtil.RowStringValue(PageDataSource, "loginid", 0);
        tbxNickName.Text = DataSetUtil.RowStringValue(PageDataSource, "nick", 0);
        tbxLoginPwd.Text = CryptSHA256.Decrypt(DataSetUtil.RowStringValue(PageDataSource, "loginpwd", 0));
        tbxTelNo.Text = DataSetUtil.RowStringValue(PageDataSource, "hp", 0);
        ddlSite.SelectedValue = DataSetUtil.RowStringValue(PageDataSource, "site", 0);

        tbxBankName.Text = DataSetUtil.RowStringValue(PageDataSource, "bankname", 0);
        tbxBankNum.Text = DataSetUtil.RowStringValue(PageDataSource, "banknum", 0);
        tbxBankOwner.Text = DataSetUtil.RowStringValue(PageDataSource, "bankowner", 0);

        ltlUserMoney.Text = string.Format("{0:F2}元", DataSetUtil.RowDoubleValue(PageDataSource, "cash", 0));
        
        ltlRegdate.Text = DataSetUtil.RowDateTimeValue(PageDataSource, "regdate", 0);
        ltlLastConn.Text = DataSetUtil.RowDateTimeValue(PageDataSource, "logindate", 0);
        tbxDeleteDate.Text = DataSetUtil.RowDateTimeValue(PageDataSource, "leavedate", 0, "yyyy-MM-dd");
        tbxInterceptDate.Text = DataSetUtil.RowDateTimeValue(PageDataSource, "interceptdate", 0, "yyyy-MM-dd");
        
        if (AuthUser.ID.ToString() == hdID.Value)
        {
            tbxDeleteDate.Enabled = false;
            chkInitLeaveDate.Enabled = false;

            tbxInterceptDate.Enabled = false;
            chkInitBlock.Enabled = false;
        }

        DataView dvCharge = null;
        if (chargeDataSource != null && chargeDataSource.Tables.Count > 0)
            dvCharge = chargeDataSource.Tables[0].DefaultView;

        if (DataSetUtil.IsNullOrEmpty(chargeDataSource))
            chargePageNumber = 0;
        else
        {
            int iPageCount = chargeDataSource.Tables[0].Rows.Count / gvChargeHist.PageSize;
            if (iPageCount < chargePageNumber)
            {
                chargePageNumber = iPageCount;
            }
        }

        if (dvCharge != null)
        {
            gvChargeHist.PageIndex = chargePageNumber;
            gvChargeHist.DataSource = dvCharge;
            gvChargeHist.DataBind();
        }

        DataView dvDischarge = null;
        if (dischargeDataSource != null && dischargeDataSource.Tables.Count > 0)
            dvDischarge = dischargeDataSource.Tables[0].DefaultView;

        if (DataSetUtil.IsNullOrEmpty(dischargeDataSource))
            dischargePageNumber = 0;
        else
        {
            int iPageCount = dischargeDataSource.Tables[0].Rows.Count / gvDischargeHist.PageSize;
            if (iPageCount < dischargePageNumber)
            {
                dischargePageNumber = iPageCount;
            }
        }

        if (dvDischarge != null)
        {
            gvDischargeHist.PageIndex = dischargePageNumber;
            gvDischargeHist.DataSource = dvDischarge;
            gvDischargeHist.DataBind();
        }

        DataView dvLogin = null;
        if (loginDataSource != null && loginDataSource.Tables.Count > 0)
            dvLogin = loginDataSource.Tables[0].DefaultView;

        if (DataSetUtil.IsNullOrEmpty(loginDataSource))
            loginPageNumber = 0;
        else
        {
            int iPageCount = loginDataSource.Tables[0].Rows.Count / gvLoginHist.PageSize;
            if (iPageCount < loginPageNumber)
            {
                loginPageNumber = iPageCount;
            }
        }

        if (dvLogin != null)
        {
            gvLoginHist.PageIndex = loginPageNumber;
            gvLoginHist.DataSource = dvLogin;
            gvLoginHist.DataBind();
        }

    }

    protected void gvChargeHist_PageIndexChange(object sender, GridViewPageEventArgs e)
    {
        chargePageNumber = e.NewPageIndex;
        BindData();
    }

    protected void gvDischargeHist_PageIndexChange(object sender, GridViewPageEventArgs e)
    {
        dischargePageNumber = e.NewPageIndex;
        BindData();
    }

    protected void gvLoginHist_PageIndexChange(object sender, GridViewPageEventArgs e)
    {
        loginPageNumber = e.NewPageIndex;
        BindData();
    }

    protected void btnCreditDo_Click(object sender, EventArgs e)
    {
        string strDesc = tbxCreditDesc.Text;
        double fMoney = 0;
        /*
        if (string.IsNullOrEmpty(strDesc))
        {
            ShowMessageBox(Resources.Err.ERR_CONTENT_INPUT);
            return;
        }*/
        if (!double.TryParse(tbxCreditMoney.Text, out fMoney))
        {
            ShowMessageBox(Resources.Err.ERR_MONEY_INPUT);
            return;
        }
        long lUserId = Convert.ToInt64(hdID.Value);
        if (fMoney > 0)
        {
            if (!InsertCash(
                    lUserId,
                    fMoney,
                    0,
                    string.Format(Resources.Desc.DESC_MANAGEADD, tbxCreditDesc.Text)))
            {
                ShowMessageBox(Resources.Err.ERR_DBERROR);
                return;
            }
        }
        else
        {
            if (!InsertCash(
                    lUserId,
                    0,
                    Math.Abs(fMoney),
                    string.Format(Resources.Desc.DESC_MANAGEMINUS, tbxCreditDesc.Text)))
            {
                ShowMessageBox(Resources.Err.ERR_DBERROR);
                return;
            }
        }

        tbxCreditDesc.Text = "";
        tbxCreditMoney.Text = "";

        PageDataSource = null;
        BindData();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            DBConn.RunStoreProcedure(Constants.SP_UPDATEUSER,
                new string[] {
                    "@id",
                    "@loginpwd",
                    "@nick",
                    "@site",
                    "@tel",
                    "@bankname",
                    "@bankowner",
                    "@banknum"
                },
                new object[] {
                    Convert.ToInt64(hdID.Value),
                    CryptSHA256.Encrypt(tbxLoginPwd.Text),
                    tbxNickName.Text,
                    ddlSite.SelectedValue.ToString(),
                    tbxTelNo.Text,
                    tbxBankName.Text,
                    tbxBankOwner.Text,
                    tbxBankNum.Text
                });
        }
        catch (Exception ex)
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR + "\\n" + ex.Message);
            return;
        }

        BindData();
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Write("<script lanuage=\"javascript\" type=\"text/javascript\">\n" +
                            "window.opener.location.href=window.opener.location.href;\n" +
                            "window.close();\n" +
                            "</script>");
        Response.End();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DBConn.RunStoreProcedure(Constants.SP_UPDATEUSERINFO,
            new string[] {
                "@id",
                "@leavedate"
            },
            new object[] {
                Convert.ToInt64(hdID.Value),
                CurrentDate
            });

        AlertAndClose("삭제되였습니다.");
    }
}
