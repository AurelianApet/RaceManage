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


public partial class MoneyMng_CashMng : Ronaldo.uibase.PageBase
{
    protected override GridView getGridControl()
    {
        return gvContent;
    }
    protected override void Page_Load(object sender, EventArgs e)
    {
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

        ddlSearchKey.Items.Clear();
        ddlSearchKey.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "loginid"));
        ddlSearchKey.Items.Add(new ListItem(Resources.Str.STR_NICKNAME, "nick"));
        ddlSearchKey.Items.Add(new ListItem(Resources.Str.STR_CONTENT, "description"));

        if (Request.Params["loginid"] != null)
        {
            ddlSearchKey.SelectedValue = "loginid";
            tbxSearchValue.Text = Request.Params["loginid"];

            tbxCreditLoginID.Text = Request.Params["loginid"];
        }
        else if (Request.Params["description"] != null)
        {
            ddlSearchKey.SelectedValue = "description";
            tbxSearchValue.Text = Request.Params["description"];
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
        
        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
            dicParams.Add("@" + ddlSearchKey.SelectedValue, tbxSearchValue.Text);

        if (!string.IsNullOrEmpty(tbxStartDate.Text))
            dicParams.Add("@sdate", tbxStartDate.Text);

        if (!string.IsNullOrEmpty(tbxEndDate.Text))
            dicParams.Add("@edate", tbxEndDate.Text);

        if (ddlSite.SelectedIndex > 0)
            dicParams.Add("@site", ddlSite.SelectedValue);

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINFO, dicParams);
    }

    protected override void BindData()
    {
        base.BindData();

        long lSumCredit = 0;
        long lSumDebit = 0;
        if (!DataSetUtil.IsNullOrEmpty(PageDataSource))
        {
            for (int i = 0; i < PageDataSource.Tables[0].Rows.Count; i++)
            {
                lSumCredit += DataSetUtil.RowLongValue(PageDataSource, "credit", i);
                lSumDebit += DataSetUtil.RowLongValue(PageDataSource, "debit", i);
            }
        }

        ltlSumCredit.Text = string.Format("{0:N0}", lSumCredit);
        ltlSumDebit.Text = string.Format("{0:N0}", lSumDebit);
        ltlSumCurrent.Text = string.Format("{0:N0}", lSumCredit - lSumDebit);
    }

    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strDescription = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "description"));
            string strRel = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rel"));

            string strContent = "";

            int nLength = strDescription.IndexOf("]") - 1;

            strDescription = strDescription.Substring(nLength < 0 ? 0 : 1, nLength < 0 ? strDescription.Length : nLength);
            if (strDescription.IndexOf("배팅배당") >= 0)
                strContent = strContent + strRel + " <a href='?description=" + strDescription + "' style='color:red;font-weight:bold;'>" + strDescription + "</a>";
            else
                strContent = strContent + strRel + " <a href='?description=" + strDescription + "'>" + strDescription + "</a>";

            setLiteralValue(e.Row, "ltlContent", strContent);
        }
    }

    protected void SearchConditions_Changed(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }

    protected void btnCreditDo_Click(object sender, EventArgs e)
    {
        string strLoginID = tbxCreditLoginID.Text;
        string strDesc = tbxCreditDesc.Text;
        long lMoney = 0;

        if (string.IsNullOrEmpty(strLoginID))
        {
            ShowMessageBox(Resources.Err.ERR_LOGINID_INPUT);
            return;
        }
        if (string.IsNullOrEmpty(strDesc))
        {
            ShowMessageBox(Resources.Err.ERR_CONTENT_INPUT);
            return;
        }
        if (!long.TryParse(tbxCreditMoney.Text, out lMoney))
        {
            ShowMessageBox(Resources.Err.ERR_MONEY_INPUT);
            return;
        }

        DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
            new string[] { "@loginid" }, new object[] { strLoginID });
        if (DataSetUtil.IsNullOrEmpty(dsUser))
        {
            ShowMessageBox(Resources.Err.ERR_LOGINID_INVALID);
            return;
        }

        if (lMoney > 0)
        {
            if (!InsertCash(
                    DataSetUtil.RowIntValue(dsUser, "id", 0),
                    lMoney,
                    0,
                    tbxCreditDesc.Text))
            {
                ShowMessageBox(Resources.Err.ERR_DBERROR);
                return;
            }
        }
        else
        {
            if (!InsertCash(
                    DataSetUtil.RowIntValue(dsUser, "id", 0),
                    0,
                    Math.Abs(lMoney),
                    tbxCreditDesc.Text))
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["chkNo"]))
            return;

        string[] strIDs = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < strIDs.Length; i++)
        {
            DBConn.RunStoreProcedure(Constants.SP_DELETEMONEYINFO,
                new string[] { "@id" }, new object[] { Convert.ToInt32(strIDs[i]) });
        }

        PageDataSource = null;
        BindData();
    }
}
