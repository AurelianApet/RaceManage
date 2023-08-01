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

public partial class MemberMng_ConnectHist : Ronaldo.uibase.PageBase
{
    protected override GridView getGridControl()
    {
        return gvContent;
    }
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!IsPostBack)
        {
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
    }
    protected override void InitControls()
    {
        base.InitControls();

        ddlSearchKind.Items.Clear();
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "loginid"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_NICKNAME, "nick"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_SITE, "site"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_IP, "ip"));

        if (Request.Params["loginid"] != null)
        {
            ddlSearchKind.SelectedValue = "loginid";
            tbxSearchValue.Text = Request.Params["loginid"];
        }
    }

    protected override void LoadData()
    {
        base.LoadData();
        Dictionary<string, object> dicParams = new Dictionary<string, object>();
        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
        {
            dicParams.Add("@" + ddlSearchKind.SelectedValue, tbxSearchValue.Text);
        }
        if (!string.IsNullOrEmpty(tbxStartDate.Text))
            dicParams.Add("@sdate", StartDate.ToString("yyyy-MM-dd 00:00:00"));
        if (!string.IsNullOrEmpty(tbxEndDate.Text))
            dicParams.Add("@edate", EndDate.ToString("yyyy-MM-dd 23:59:00"));
        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETLOGINHISTS, dicParams);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbxStartDate.Text))
            StartDate = Convert.ToDateTime(tbxStartDate.Text + " 00:00:00");
        if (!string.IsNullOrEmpty(tbxEndDate.Text))
            EndDate = Convert.ToDateTime(tbxEndDate.Text + " 23:59:00");
        PageDataSource = null;
        BindData();
    }
    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strUrl = DataBinder.Eval(e.Row.DataItem, "url").ToString();
            strUrl = strUrl.Replace("http://", "");
            int nPosDomain = strUrl.IndexOf("/");
            setLiteralValue(e.Row, "ltlConnDomain", (nPosDomain < 0) ? "" : strUrl.Substring(0, nPosDomain));
        }
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

            DBConn.RunStoreProcedure(Constants.SP_DELETELOGIN,
                new string[] { "@id" }, new object[] { iID });

            iDeleteCount++;
        }

        ShowMessageBox(string.Format(Resources.Msg.MSG_NOTICE_PROCESS, iTotalCount, iDeleteCount));
        PageDataSource = null;
        BindData();
    }
}
