using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class MemberMng_MemberMng : Ronaldo.uibase.PageBase
{
    private int nType = 0;
    protected override GridView getGridControl()
    {
        return gvContent;
    }
    protected override void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(SortColumn))
        {
            SortColumn = "regdate";
            SortDirection = SortDirection.Descending;
        }
        if (Request.Params["type"] != null && int.TryParse(Request.Params["type"], out nType))
        {
            nType = nType + 1;
        }
        base.Page_Load(sender, e);

        if (!IsPostBack)
            Master.outputRes2JS(
                new string[] {  
                    "MSG_NOSELECTITEM",
                    "MSG_CONFIRMDELETE",
                    "MSG_CONFIRMAPPLY"
                },
                new string[]
                {         
                    Resources.Msg.MSG_NOSELECTITEM,
                    Resources.Msg.MSG_CONFIRMDELETE,
                    Resources.Msg.MSG_CONFIRMAPPLY
                });
    }

    protected override void InitControls()
    {
        base.InitControls();

        rblStatus.Items.Clear();
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_WHOLE, Constants.USERSTATUS_ALL.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_NORMAL, Constants.USERSTATUS_COMMON.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_NEW, Constants.USERSTATUS_NEW.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_BLOCK, Constants.USERSTATUS_INTERCEPT.ToString()));
        rblStatus.Items.Add(new ListItem(Resources.Str.STR_LEAVE, Constants.USERSTATUS_LEAVE.ToString()));
        rblStatus.SelectedIndex = nType;

        ddlSearchKind.Items.Clear();
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "loginid"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_NICKNAME, "nick"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_TELNO, "tel"));

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
        Dictionary<string, object> dicParams = new Dictionary<string, object>();
        if (rblStatus.SelectedValue == Constants.USERSTATUS_COMMON.ToString())
            dicParams.Add("@correct", "1");
        else if (rblStatus.SelectedValue == Constants.USERSTATUS_NEW.ToString())
            dicParams.Add("@new", "1");
        else if (rblStatus.SelectedValue == Constants.USERSTATUS_INTERCEPT.ToString())
            dicParams.Add("@intercept", "1");
        else if (rblStatus.SelectedValue == Constants.USERSTATUS_LEAVE.ToString())
            dicParams.Add("@leave", "1");
        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
            dicParams.Add("@" + ddlSearchKind.SelectedValue, tbxSearchValue.Text);
        
        if (ddlSite.SelectedIndex > 0)
            dicParams.Add("@site", ddlSite.SelectedValue);

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETUSERLIST, dicParams);

        DBConn.RunStoreProcedure(Constants.SP_UPDATEUSER,
            new string[] { "@chkdate" }, new object[] { CurrentDate });
    }

    protected override void BindData()
    {
        base.BindData();

        long lTotalCount = 0;
        long lBlockCount = 0;
        long lLeaveCount = 0;

        if (!DataSetUtil.IsNullOrEmpty(PageDataSource))
        {
            lTotalCount = PageDataSource.Tables[0].Rows.Count;

            for (int i = 0; i < PageDataSource.Tables[0].Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(DataSetUtil.RowStringValue(PageDataSource, "interceptdate", i)))
                    lBlockCount++;
                if (!string.IsNullOrEmpty(DataSetUtil.RowStringValue(PageDataSource, "leavedate", i)))
                    lLeaveCount++;
            }
        }

        ltlUserCount.Text = string.Format(Resources.Str.STR_USERCOUNT, lTotalCount, lBlockCount, lLeaveCount);
    }

    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long lID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "id"));
            int iUserLevel = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ulevel"));
            string strLoginID = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "loginid"));
            string strNick = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nick"));
            int nUserSite = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "site"));
            DateTime dtIntercept = DateTime.Now;
            bool bIsIntercept = (DataBinder.Eval(e.Row.DataItem, "interceptdate") == null);
            if (!bIsIntercept)
                bIsIntercept = DateTime.TryParse(DataBinder.Eval(e.Row.DataItem, "interceptdate").ToString(), out dtIntercept);

            DateTime dtLeave = DateTime.Now;
            bool bIsLeave = (DataBinder.Eval(e.Row.DataItem, "leavedate") == null);
            if (!bIsLeave)
                bIsLeave = DateTime.TryParse(DataBinder.Eval(e.Row.DataItem, "leavedate").ToString(), out dtLeave);

            DateTime dtRegDate;

            DateTime.TryParse(DataBinder.Eval(e.Row.DataItem, "regdate").ToString(), out dtRegDate);
            string strLoginIDColor = "#000000";
            string strNickColor = "#000000";
            string strOwnerColor = "#000000";
            DateTime dtNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (dtRegDate.AddDays(30) > dtNow)
            {
                strLoginIDColor = "#000000";
            }
            
            
            if (bIsLeave)
                e.Row.Style[HtmlTextWriterStyle.BackgroundColor] = "yellow";
            else if (bIsIntercept)
                e.Row.Style[HtmlTextWriterStyle.BackgroundColor] = "#069dd5";

            setLiteralValue(e.Row, "ltlLoginID", "<font color='" + strLoginIDColor + "'>" + strLoginID + "</font>");
            setLiteralValue(e.Row, "ltlNick", "<font color='" + strNickColor + "'>" + strNick + "</font>");

            if (strOwnerColor == "red")
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;

            DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST);
            string strSite = "<select name=\"site" + lID + "\">";
            strSite += "<option value=\"---\">---</option>";
            for (int i = 0; i < DataSetUtil.RowCount(dsSite); i++)
            {
                string strSiteName = DataSetUtil.RowStringValue(dsSite, "site_name", i);
                int nSiteID = DataSetUtil.RowIntValue(dsSite, "id", i);
                strSite += string.Format("<option value=\"{0}\"{1}>{2}</option>",
                    strSiteName,
                    nSiteID == nUserSite ? " selected" : "",
                    strSiteName);
            }
            strSite += "</select>";
            setLiteralValue(e.Row, "ltlSite", strSite);

            string strUserLevel = "代理";
            if (iUserLevel < Constants.LEVEL_DIST)
                strUserLevel = "<font color='#000000'>用户</font>";

            setLiteralValue(e.Row, "ltlPermission", strUserLevel);

            string strBankName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bankname"));
            string strBankNum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "banknum"));
            string strBankOwner = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bankowner"));

            setLiteralValue(e.Row, "ltlBankInfo", string.IsNullOrEmpty(strBankNum) ? "无" : string.Format("{0}, {1} / {2}", strBankName, strBankOwner, strBankNum));
        }
    }

    protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long lID = 0;
        switch (e.CommandName)
        {
            case "MemberEdit":
                lID = Convert.ToInt64(e.CommandArgument);
                Response.Redirect("MemberEdit.aspx?mid=" + lID + "&page=" + PageNumber);
                break;
            case "MemberDelete":
                lID = Convert.ToInt64(e.CommandArgument);

                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSERINFO,
                    new string[] {
                        "@id",
                        "@leavedate"
                    },
                    new object[] {
                        lID,
                        CurrentDate
                    });

                PageDataSource = null;
                BindData();
                break;
        }
    }

    protected void chkMoneyHist_ChecckedChanged(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strSelIDs = Request.Form["chkNo"];
        if (string.IsNullOrEmpty(strSelIDs))
        {
            ShowMessageBox(Resources.Msg.MSG_NOSELECTITEM);
            return;
        }

        string[] arrSelID = strSelIDs.Split(',');

        try
        {
            for (int i = 0; i < arrSelID.Length; i++)
            {
                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSERINFO,
                    new string[] {
                        "@id",
                        "@ulevel",
                        "@site",
                        "@blockdate"
                    },
                    new object[] {
                        Convert.ToInt64(arrSelID[i]),
                        Convert.ToInt16(Request.Form["perm" + arrSelID[i]]),
                        Convert.ToString(Request.Form["site" + arrSelID[i]]).Trim(),
                        (Request.Form["chkBlock" + arrSelID[i]] == "block") ? CurrentDate : null
                    });
            }
        }
        catch
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR);
            return;
        }

        PageDataSource = null;
        BindData();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string strSelIDs = Request.Form["chkNo"];
        if (string.IsNullOrEmpty(strSelIDs))
        {
            ShowMessageBox(Resources.Msg.MSG_NOSELECTITEM);
            return;
        }

        string[] arrSelID = strSelIDs.Split(',');

        try
        {
            for (int i = 0; i < arrSelID.Length; i++)
            {
                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSERINFO,
                    new string[] {
                        "@id",
                        "@leavedate"
                    },
                    new object[] {
                        Convert.ToInt64(arrSelID[i]),
                        CurrentDate
                    });
            }
        }
        catch
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR);
            return;
        }

        PageDataSource = null;
        BindData();
    }
    protected void btnExcelDown_Click(object sender, EventArgs e)
    {
        LoadData();
        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.AddHeader("Content-Disposition", "attachment;filename=MemberList_" + CurrentDate + ".csv");

        Response.BinaryWrite(new byte[] { 0xFF, 0xFE });

        string[] arrHeader = { 
                                 Resources.Str.STR_NUMBER, 
                                 Resources.Str.STR_LOGINID, 
                                 Resources.Str.STR_LOGINPWD,
                                 Resources.Str.STR_NICKNAME,
                                 Resources.Str.STR_OWNERNAME,
                                 Resources.Str.STR_PERMISSION,
                                 "기여도",
                                 Resources.Str.STR_SITE,
                                 Resources.Str.STR_TELNO,
                                 Resources.Str.STR_CASH,
                                 Resources.Str.STR_REGDATE
                             };
        Response.Write(string.Join("\t", arrHeader) + "\n");

        string[] arrRow = null;
        DataTable dtTemp = PageDataSource.Tables[0].Select("", string.Format("{0} {1}", SortColumn, SortDirection == System.Web.UI.WebControls.SortDirection.Ascending ? "" : "DESC")).CopyToDataTable<DataRow>();
        PageDataSource.Tables.Clear();
        PageDataSource.Tables.Add(dtTemp);
        if (!DataSetUtil.IsNullOrEmpty(PageDataSource))
        {
            for (int i = 0; i < PageDataSource.Tables[0].Rows.Count; i++)
            {
                arrRow = new string[] {
                    DataSetUtil.RowLongValue(PageDataSource, "id", i).ToString(),
                    DataSetUtil.RowStringValue(PageDataSource, "loginid", i),
                    CryptSHA256.Decrypt(DataSetUtil.RowStringValue(PageDataSource, "loginpwd", i)),
                    DataSetUtil.RowStringValue(PageDataSource, "nick", i),
                    DataSetUtil.RowStringValue(PageDataSource, "ownername", i),
                    DataSetUtil.RowIntValue(PageDataSource, "ulevel", i).ToString(),
                    DataSetUtil.RowIntValue(PageDataSource, "benefit", i).ToString(),
                    DataSetUtil.RowStringValue(PageDataSource, "recommend", i).ToString(),
                    DataSetUtil.RowStringValue(PageDataSource, "site", i).ToString(),
                    DataSetUtil.RowStringValue(PageDataSource, "hp", i),
                    DataSetUtil.RowLongValue(PageDataSource, "cash", i).ToString(),
                    DateTime.Parse(DataSetUtil.RowStringValue(PageDataSource, "regdate", i)).ToString("yyyy/MM/dd HH:mm:ss")
                };

                Response.Write(string.Join("\t", arrRow) + "\n");
            }
        }

        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }

    protected void btnRegDist_Click(object sender, EventArgs e)
    {
        string strSelIDs = Request.Form["chkNo"];
        if (string.IsNullOrEmpty(strSelIDs))
        {
            ShowMessageBox(Resources.Msg.MSG_NOSELECTITEM);
            return;
        }

        string[] arrSelID = strSelIDs.Split(',');

        try
        {
            for (int i = 0; i < arrSelID.Length; i++)
            {
                DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                    new string[]{
                        "@id"
                    },
                    new object[]{
                        Convert.ToInt64(arrSelID[i])
                    });
                int nLevle = DataSetUtil.RowIntValue(dsUser, "ulevel", 0);
                if (nLevle >= Constants.LEVEL_DIST)
                    continue;

                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSERINFO,
                    new string[] {
                        "@id",
                        "@ulevel"
                    },
                    new object[] {
                        Convert.ToInt64(arrSelID[i]),
                        Constants.LEVEL_DIST
                    });
            }
        }
        catch
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR);
            return;
        }

        PageDataSource = null;
        BindData();
    }
}
