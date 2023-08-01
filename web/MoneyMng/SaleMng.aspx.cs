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

public partial class MoneyMng_SaleMng : Ronaldo.uibase.PageBase
{
    double fCharge = 0;
    int nCharge_count = 0;
    int nCharge_one_count = 0;
    double fDischarge = 0;
    int nDischarge_count = 0;
    double fSale = 0;
    double fBet = 0;
    int nBetCount = 0;
    double fWin = 0;
    int nWinCount = 0;
    double fBetSale = 0;

    DataSet dsTotalBet = null;

    protected override GridView getGridControl()
    {
        return gvContent;
    }

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

    protected override void InitControls()
    {
        base.InitControls();

        ddlSearchKind.Items.Clear();
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "loginid"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_NICKNAME, "nick"));

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

        if (!IsPostBack)
        {
            StartDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
            EndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }
        
        Dictionary<string, object> dicParams = new Dictionary<string, object>();
        dicParams.Add("@startdate", StartDate);
        dicParams.Add("@enddate", EndDate);
        if (ddlSite.SelectedIndex > 0)
            dicParams.Add("@site", ddlSite.SelectedValue);

        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
        {
            dicParams.Add("@" + ddlSearchKind.SelectedValue, tbxSearchValue.Text);
        }

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETSALEDATA, dicParams);
        dsTotalBet = DBConn.RunStoreProcedure(Constants.SP_GETSALEDATA1, dicParams);
    }

    protected override void BindData()
    {
        base.BindData();

        tbxStartDate.Text = StartDate.ToString("yyyy-MM-dd");
        tbxEndDate.Text = EndDate.ToString("yyyy-MM-dd");
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
            fCharge += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "charge"));
            nCharge_count += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "charge_count"));
            nCharge_one_count += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "charge_one_count"));
            fDischarge += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "discharge"));
            nDischarge_count += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "discharge_count"));
            fSale += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "salemoney"));
            fBet += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "betmoney"));
            nBetCount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "betcount"));
            fWin += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "winmoney"));
            nWinCount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "wincount"));
            fBetSale += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "betsalemoney"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            nCharge_one_count = DataSetUtil.RowIntValue(dsTotalBet, "charge_count", 0);
            foreach (TableCell cell in e.Row.Cells)
            {
                cell.CssClass = "clstablefooter withborder";
                cell.HorizontalAlign = HorizontalAlign.Right;
            }
            e.Row.Cells[0].Text = Resources.Str.STR_SUM;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Text = string.Format("{0:F2}({1:N0}/<font color='red'>{2:N0}</font>)", fCharge, nCharge_count, nCharge_one_count);
            e.Row.Cells[1].ForeColor = System.Drawing.Color.Blue;
            e.Row.Cells[2].Text = string.Format("{0:F2}({1:N0})", fDischarge, nDischarge_count);
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            e.Row.Cells[3].Text = string.Format("{0:F2}({1:N0})", fBet, nBetCount);
            e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
            e.Row.Cells[4].Text = string.Format("{0:F2}({1:N0})", fWin, nWinCount);
            e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            e.Row.Cells[5].Text = string.Format("{0:F2}", fBetSale);
            
        }
    }
}
