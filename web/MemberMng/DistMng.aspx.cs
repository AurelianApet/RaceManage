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

public partial class MemberMng_DistMng : Ronaldo.uibase.PageBase
{
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

        base.Page_Load(sender, e);

        if (!IsPostBack)
            Master.outputRes2JS(
                new string[] { 
                    "MSG_NOSELECTITEM",
                    "MSG_CONFIRMDELETE"
                },
                new string[]
                {       
                    Resources.Msg.MSG_NOSELECTITEM,
                    Resources.Msg.MSG_CONFIRMDELETE
                });
    }

    protected override void InitControls()
    {
        base.InitControls();

        ddlSearchKind.Items.Clear();
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "loginid"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_NICKNAME, "nick"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_REGDATE, "regdate"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_SITE, "site"));
    }

    protected override void LoadData()
    {
        base.LoadData();

        Dictionary<string, object> dicParams = new Dictionary<string, object>();

        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
        {
            if (ddlSearchKind.SelectedValue == "regdate")
            {
                DateTime dtTemp = DateTime.Now;
                if (DateTime.TryParse(tbxSearchValue.Text, out dtTemp))
                {
                    if (tbxSearchValue.Text.Split('-').Length == 2)
                    {
                        dicParams.Add("@sdate", dtTemp.ToString("yyyy-MM-dd 00:00:00"));
                        dicParams.Add("@edate", dtTemp.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                    }
                    else
                    {
                        dicParams.Add("@sdate", dtTemp.ToString("yyyy-MM-dd 00:00:00"));
                        dicParams.Add("@edate", dtTemp.ToString("yyyy-MM-dd 23:59:59"));
                    }
                }
            }
            else
            {
                dicParams.Add("@" + ddlSearchKind.SelectedValue, tbxSearchValue.Text);
            }
        }

        dicParams.Add("@ulevel", Constants.LEVEL_DIST);
        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETUSERLIST, dicParams);
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

        ltlUserCount.Text = string.Format(Resources.Str.STR_DISTCOUNT, lTotalCount, lBlockCount, lLeaveCount);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }

    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int nSiteID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "site"));
            DataSet dsUsers = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                new string[]{
                    "@site"
                },
                new object[]{
                    nSiteID
                });
            int nUsers = (DataSetUtil.IsNullOrEmpty(dsUsers)) ? 0 : DataSetUtil.RowCount(dsUsers);
            setLiteralValue(e.Row, "ltlUserNum", string.Format("{0}名", nUsers));
        }
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
                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSER,
                    new string[] {
                        "@id",
                        "@ulevel"
                    },
                    new object[] {
                        Convert.ToInt64(arrSelID[i]),
                        Constants.LEVEL_USER
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

    protected void btnDistReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberMng.aspx");
    }
}
