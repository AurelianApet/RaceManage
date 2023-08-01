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

public partial class GameMng_BetHist : Ronaldo.uibase.PageBase
{
    protected override GridView getGridControl()
    {
        return gvContent;
    }
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }
    protected override void LoadData()
    {
        base.LoadData();

        if (!IsPostBack)
        {
            StartDate = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00"));
            EndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        Dictionary<string, object> dicParams = new Dictionary<string, object>();
        dicParams.Add("@lottery", Constants.GAMETYPE_RACE);
        dicParams.Add("@sdate", StartDate);
        dicParams.Add("@edate", EndDate);
        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
        {
            dicParams.Add("@" + ddlSearchKind.SelectedValue, tbxSearchValue.Text);
        }
        if (ddlSite.SelectedIndex > 0)
            dicParams.Add("@site", ddlSite.SelectedValue);

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST, dicParams);
    }
    protected override void BindData()
    {
        base.BindData();

        tbxStartDate.Text = StartDate.ToString("yyyy-MM-dd");
        tbxEndDate.Text = EndDate.ToString("yyyy-MM-dd");
    }
    protected override void InitControls()
    {
        base.InitControls();

        ddlSearchKind.Items.Clear();
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "loginid"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_NICKNAME, "nick"));

        if (!string.IsNullOrEmpty(Request.Params["loginid"]))
        {
            ddlSearchKind.SelectedValue = Resources.Str.STR_LOGINID;
            tbxSearchValue.Text = Request.Params["loginid"];
        }

        ddlSite.Items.Clear();
        ddlSite.Items.Add(new ListItem(Resources.Str.STR_WHOLE, "0"));
        DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST);
        for (int i = 0; i < DataSetUtil.RowCount(dsSite); i++)
        {
            ddlSite.Items.Add(new ListItem(DataSetUtil.RowStringValue(dsSite, "site_name", i), DataSetUtil.RowStringValue(dsSite, "id", i)));
        }
        if (!string.IsNullOrEmpty(Request.Params["site"]))
            ddlSite.SelectedValue = Request.Params["site"];
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        StartDate = Convert.ToDateTime(tbxStartDate.Text + " 00:00:00");
        EndDate = Convert.ToDateTime(tbxEndDate.Text + " 23:59:59");
        PageDataSource = null;
        BindData();
    }
    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strLoginID = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "loginid"));
            string strNick = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nick"));
            string strBetIP = DataBinder.Eval(e.Row.DataItem, "bet_ip").ToString();

            string strUserInfo = string.Format("<a href='?loginid={2}'>{0}</a> ({1})[{3}]", strLoginID, strNick, strLoginID, strBetIP);
            setLiteralValue(e.Row, "ltlUser", strUserInfo);

            int nBetPos = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "betpos").ToString());
            string strBetVal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "betval"));
            string strBetMode = getBetMode(nBetPos);
            
            setLiteralValue(e.Row, "ltrBetMode", strBetMode);
            setLiteralValue(e.Row, "ltrBetPos", strBetVal);

            int nResult = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "result").ToString());
            if (nResult == 0)
            {
                setLiteralValue(e.Row, "ltrResult", Resources.Str.STR_PLAYING);
            }
            else if (nResult == 1)
            {
                setLiteralValue(e.Row, "ltrResult", "<font color='red'>" + Resources.Str.STR_LOSE + "</font>");
            }
            else if (nResult == 3)
            {
                setLiteralValue(e.Row, "ltrResult", "<font color='orange'>" + Resources.Str.STR_VALID + "</font>");
            }
            else
            {
                setLiteralValue(e.Row, "ltrResult", "<font color='blue'>" + Resources.Str.STR_WIN + "</font>");
            }
        }
    }
}
