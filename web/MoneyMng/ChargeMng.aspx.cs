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

public partial class MoneyMng_ChargeMng : Ronaldo.uibase.PageBase
{
    private int nType = 0;
    protected override GridView getGridControl()
    {
        return gvContent;
    }

    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["type"] != null && int.TryParse(Request.Params["type"], out nType))
            {
                nType = nType + 1;
                //rblStatus.SelectedIndex = nType;
            }

            Master.outputRes2JS(
                new string[] { 
                    "MSG_NOSELECTITEM",
                    "MSG_CONFIRMAPPLY",
                    "MSG_CONFIRMCANCEL",
                    "MSG_CONFIRMDELETE",
                    "PAGETYPE"
                },
                new string[]
                {       
                    Resources.Msg.MSG_NOSELECTITEM,
                    Resources.Msg.MSG_CONFIRMAPPLY,
                    Resources.Msg.MSG_CONFIRMCANCEL,
                    Resources.Msg.MSG_CONFIRMDELETE,
                    "CHARGE"
                });
        }
        else
        {
            if (Request.Form["__EVENTARGUMENT"] == "AutoUpdate")
                SearchConditions_Changed(null, null);
        }
        base.Page_Load(sender, e);
    }

    protected override void InitControls()
    {
        base.InitControls();

        rblStatus.Items.Clear();
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_WHOLE, "All"));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_REQUEST, Constants.MONEYINOUT_STATUS_REQUEST.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_COMPLETE, Constants.MONEYINOUT_STATUS_APPLY.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_CANCEL, Constants.MONEYINOUT_STATUS_CANCEL.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_STANDBY, Constants.MONEYINOUT_STATUS_STANDBY.ToString()));

        rblStatus.SelectedIndex = nType;

        if (!string.IsNullOrEmpty(Request.Params["loginid"]))
        {
            tbxLoginID.Text = Request.Params["loginid"];
        }

        ddlSite.Items.Clear();
        ddlSite.Items.Add(new ListItem(Resources.Str.STR_WHOLE, "0"));
        DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST);
        for (int i = 0; i < DataSetUtil.RowCount(dsSite); i++)
        {
            ddlSite.Items.Add(new ListItem(DataSetUtil.RowStringValue(dsSite, "site_name", i), DataSetUtil.RowStringValue(dsSite, "id", i)));
        }
    }

    protected override void LoadData()
    {
        base.LoadData();

        System.Collections.Generic.Dictionary<string, object> dicParams = new System.Collections.Generic.Dictionary<string, object>();
        dicParams.Add("@kind", Constants.MONEYINOUT_MODE_CHARGE);

        if (rblStatus.SelectedValue != "All")
            dicParams.Add("@status", rblStatus.SelectedValue);

        if (!string.IsNullOrEmpty(tbxLoginID.Text))
            dicParams.Add("@loginid", tbxLoginID.Text);
        if (!string.IsNullOrEmpty(tbxNickName.Text))
            dicParams.Add("@nick", tbxNickName.Text);
        if (!string.IsNullOrEmpty(tbxChargeName.Text))
            dicParams.Add("@ownername", tbxChargeName.Text);

        if (!string.IsNullOrEmpty(tbxStartDate.Text))
            dicParams.Add("@sdate", StartDate.ToString("yyyy-MM-dd 00:00:00"));
        if (!string.IsNullOrEmpty(tbxEndDate.Text))
            dicParams.Add("@edate", EndDate.ToString("yyyy-MM-dd 23:59:00"));
        if (ddlSite.SelectedIndex > 0)
            dicParams.Add("@site", ddlSite.SelectedValue);

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS, dicParams);
    }
    protected override void BindData()
    {
        base.BindData();
        long lTotalCount = 0;
        double fTotalMoney = 0;

        if (!DataSetUtil.IsNullOrEmpty(PageDataSource))
        {
            for (int i = 0; i < DataSetUtil.RowCount(PageDataSource); i++)
            {
                int nResult = DataSetUtil.RowIntValue(PageDataSource, "status", i);
                if (nResult == Constants.MONEYINOUT_STATUS_APPLY)
                {
                    lTotalCount++;
                    fTotalMoney += DataSetUtil.RowDoubleValue(PageDataSource, "reqmoney", i);
                }

            }
        }

        ltlTotalCount.Text = string.Format(Resources.Str.STR_MESSEGEUNIT, lTotalCount);
        ltlTotalMoney.Text = string.Format(Resources.Str.STR_MONEYUNIT, fTotalMoney);
    }

    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iStatus = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "status"));

            LinkButton lnkRecharge = e.Row.FindControl("lnkRecharge") as LinkButton;
            if (lnkRecharge != null)
                lnkRecharge.Visible = false;

            if (iStatus != Constants.MONEYINOUT_STATUS_REQUEST && iStatus != Constants.MONEYINOUT_STATUS_STANDBY)
            {
                LinkButton lnkApply = e.Row.FindControl("lnkApply") as LinkButton;
                if (lnkApply != null)
                    lnkApply.Visible = false;

                LinkButton lnkStandby = e.Row.FindControl("lnkStandby") as LinkButton;
                if (lnkStandby != null)
                    lnkStandby.Visible = false;

                if (iStatus == Constants.MONEYINOUT_STATUS_CANCEL)
                {
                    LinkButton lnkCancel = e.Row.FindControl("lnkCancel") as LinkButton;
                    if (lnkCancel != null)
                        lnkCancel.Visible = false;

                    if (lnkRecharge != null)
                        lnkRecharge.Visible = true;
                    //e.Row.Cells[8].Text = "-";
                }
            }
            else if (iStatus == Constants.MONEYINOUT_STATUS_STANDBY)
            {
                LinkButton lnkStandby = e.Row.FindControl("lnkStandby") as LinkButton;
                if (lnkStandby != null)
                    lnkStandby.Visible = false;

            }
            int nInoutType = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "inout_type"));
            string strInfo = " - " + DataBinder.Eval(e.Row.DataItem, "ownername").ToString();
            if (nInoutType == 1000)
                strInfo = "工商银行" + strInfo;
            else if(nInoutType == 1001)
                strInfo = "支付宝" + strInfo;
            else if(nInoutType == 1002)
                strInfo = "腾讯财" + strInfo;
            else if (nInoutType == 1021)
                strInfo = "微信" + strInfo;

            setLiteralValue(e.Row, "ltlChargeInfo", strInfo);
        }
    }

    protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long lID = 0;

        int iTotalCount = 0;
        int iApplyCount = 0;

        try
        {
            if (e.CommandName == "ApplyCommand" || e.CommandName == "RechargeCommand")
            {
                iTotalCount++;

                lID = Convert.ToInt64(e.CommandArgument);

                if (processApply(lID))
                    iApplyCount++;
            }
            else if (e.CommandName == "StandbyCommand")
            {
                iTotalCount++;

                lID = Convert.ToInt64(e.CommandArgument);
                if (processStnadby(lID))
                    iApplyCount++;
            }
            else if (e.CommandName == "CancelCommand")
            {
                iTotalCount++;

                lID = Convert.ToInt64(e.CommandArgument);
                if (processCancel(lID))
                    iApplyCount++;
            }
        }
        catch (Exception ex) 
        {
            writeLog(ex.Message);
        }

        ShowMessageBox(string.Format(Resources.Msg.MSG_NOTICE_PROCESS, iTotalCount, iApplyCount));
        PageDataSource = null;
        BindData();
    }

    protected void SearchConditions_Changed(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbxStartDate.Text))
            StartDate = Convert.ToDateTime(tbxStartDate.Text + " 00:00:00");
        if (!string.IsNullOrEmpty(tbxEndDate.Text))
            EndDate = Convert.ToDateTime(tbxEndDate.Text + " 23:59:00");
        PageDataSource = null;
        BindData();
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (Request.Form["chkNo"] == null)
            return;

        int iTotalCount = 0;
        int iApplyCount = 0;
        string[] arrNo = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < arrNo.Length; i++)
        {
            iTotalCount++;

            long lID = Convert.ToInt64(arrNo[i]);
            if (lID < 1)
                continue;

            if (processApply(lID))
                iApplyCount++;
        }

        ShowMessageBox(string.Format(Resources.Msg.MSG_NOTICE_PROCESS, iTotalCount, iApplyCount));
        PageDataSource = null;
        BindData();
    }

    protected void btnStandby_Click(object sender, EventArgs e)
    {
        if (Request.Form["chkNo"] == null)
            return;

        int iTotalCount = 0;
        int iApplyCount = 0;
        string[] arrNo = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < arrNo.Length; i++)
        {
            iTotalCount++;

            long lID = Convert.ToInt64(arrNo[i]);
            if (lID < 1)
                continue;

            if (processStnadby(lID))
                iApplyCount++;
        }

        ShowMessageBox(string.Format(Resources.Msg.MSG_NOTICE_PROCESS, iTotalCount, iApplyCount));
        PageDataSource = null;
        BindData();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.Form["chkNo"] == null)
            return;

        int iTotalCount = 0;
        int iApplyCount = 0;
        string[] arrNo = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < arrNo.Length; i++)
        {
            iTotalCount++;

            long lID = Convert.ToInt64(arrNo[i]);
            if (lID < 1)
                continue;

            if (processCancel(lID))
                iApplyCount++;
        }

        ShowMessageBox(string.Format(Resources.Msg.MSG_NOTICE_PROCESS, iTotalCount, iApplyCount));
        PageDataSource = null;
        BindData();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.Form["chkNo"] == null)
            return;

        int iTotalCount = 0;
        int iDeleteCount = 0;
        string[] arrNo = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < arrNo.Length; i++)
        {
            iTotalCount++;

            int iID = Convert.ToInt32(arrNo[i]);
            if (iID < 1)
                continue;

            DataSet dsCharge = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
                new string[] { "@id" }, new object[] { iID });
            int iStatus = DataSetUtil.RowIntValue(dsCharge, "status", 0);
            if (iStatus == Constants.MONEYINOUT_STATUS_REQUEST)
                iStatus = Constants.MONEYINOUT_STATUS_STANDBY;
            DBConn.RunStoreProcedure(Constants.SP_UPDATEMONEYINOUTS,
                new string[] { "@id", "@kind", "@status", "@deldate", "@deltype" }, new object[] { iID, Constants.MONEYINOUT_MODE_CHARGE, iStatus, DateTime.Now, Constants.BETDELTYPE_ADMIN });

            iDeleteCount++;
        }

        ShowMessageBox(string.Format(Resources.Msg.MSG_NOTICE_PROCESS, iTotalCount, iDeleteCount));
        PageDataSource = null;
        BindData();
    }

    protected bool processApply(long lID)
    {
        DataSet dsInfo = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
            new string[] { "@id", "@kind" }, new object[] { lID, Constants.MONEYINOUT_MODE_CHARGE });

        if (DataSetUtil.IsNullOrEmpty(dsInfo))
            return false;

        int iStatus = DataSetUtil.RowIntValue(dsInfo, "status", 0);
        if (iStatus == Constants.MONEYINOUT_STATUS_APPLY)
            return false;

        if (Request.Form["txtReqMoney" + lID] == null)
            return false;

        long iUserID = DataSetUtil.RowLongValue(dsInfo, "user_id", 0);
        string strLoginID = DataSetUtil.RowStringValue(dsInfo, "loginid", 0);
        double fReqMoney = Convert.ToDouble(Request.Form["txtReqMoney" + lID].Replace(",", ""));
        string strGid = DataSetUtil.RowLongValue(dsInfo, "id", 0).ToString();

        DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER, new string[] { "@id" }, new object[] { iUserID });

        if (iUserID < 1 || fReqMoney < 1.0f)
            return false;

        if (InsertCash(
                iUserID,
                fReqMoney,
                0,
                string.Format(Resources.Desc.DESC_CHARGE, fReqMoney)))
        {

            DBConn.RunStoreProcedure(Constants.SP_UPDATEMONEYINOUTS,
                new string[] {
                    "@id",
                    "@kind",
                    "@reqmoney",
                    "@status"
                },
                new object[] {
                    lID,
                    Constants.MONEYINOUT_MODE_CHARGE,
                    fReqMoney,
                    Constants.MONEYINOUT_STATUS_APPLY
                });
        }
        else
            return false;

        return true;
    }

    protected bool processStnadby(long lID)
    {
        DataSet dsInfo = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
                    new string[] { "@id", "@kind" }, new object[] { lID, Constants.MONEYINOUT_MODE_CHARGE });

        if (DataSetUtil.IsNullOrEmpty(dsInfo))
            return false;

        int iStatus = DataSetUtil.RowIntValue(dsInfo, "status", 0);
        if (iStatus == Constants.MONEYINOUT_STATUS_APPLY || iStatus == Constants.MONEYINOUT_STATUS_CANCEL)
            return false;

        if (Request.Form["txtReqMoney" + lID] == null)
            return false;

        long iUserID = DataSetUtil.RowLongValue(dsInfo, "user_id", 0);
        double fReqMoney = Convert.ToDouble(Request.Form["txtReqMoney" + lID].Replace(",", ""));

        if (iUserID < 1 || fReqMoney < 1.0f)
            return false;

        DBConn.RunStoreProcedure(Constants.SP_UPDATEMONEYINOUTS,
            new string[] {
                        "@id",
                        "@kind",
                        "@reqmoney",
                        "@status"
                    },
            new object[] {
                        lID,
                        Constants.MONEYINOUT_MODE_CHARGE,
                        fReqMoney,
                        Constants.MONEYINOUT_STATUS_STANDBY
                    });
        return true;
    }

    protected bool processCancel(long lID)
    {
        DataSet dsInfo = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
                    new string[] { "@id", "@kind" }, new object[] { lID, Constants.MONEYINOUT_MODE_CHARGE });
        if (DataSetUtil.IsNullOrEmpty(dsInfo))
            return false;

        int iStatus = DataSetUtil.RowIntValue(dsInfo, "status", 0);
        if (iStatus == Constants.MONEYINOUT_STATUS_CANCEL)
            return false;
        long iUserID = DataSetUtil.RowLongValue(dsInfo, "user_id", 0);
        string strLoginID = DataSetUtil.RowStringValue(dsInfo, "loginid", 0);
        double fReqMoney = DataSetUtil.RowDoubleValue(dsInfo, "reqmoney", 0);

        string strGid = DataSetUtil.RowLongValue(dsInfo, "id", 0).ToString();

        if (iUserID < 1 || fReqMoney < 0)
            return false;

        if (iStatus == Constants.MONEYINOUT_STATUS_REQUEST || iStatus == Constants.MONEYINOUT_STATUS_STANDBY ||
            (iStatus == Constants.MONEYINOUT_STATUS_APPLY && InsertCash(
                iUserID,
                0,
                fReqMoney,
                string.Format(Resources.Desc.DESC_CHARGECANCEL, fReqMoney))))
        {
            DBConn.RunStoreProcedure(Constants.SP_UPDATEMONEYINOUTS,
                new string[] {
                            "@id",
                            "@kind",
                            "@reqmoney",
                            "@status"
                        },
                new object[] {
                            lID,
                            Constants.MONEYINOUT_MODE_CHARGE,
                            fReqMoney,
                            Constants.MONEYINOUT_STATUS_CANCEL
                        });
            return true;
        }
        else
            return false;
    }
}
