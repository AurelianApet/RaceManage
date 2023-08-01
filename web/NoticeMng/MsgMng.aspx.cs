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

public partial class NoticeMng_MsgMng : Ronaldo.uibase.PageBase
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
                    "MSG_CONFIRMDELETE",
                    "MSG_NOSELECTITEM"
                },
                new string[] {
                    Resources.Msg.MSG_CONFIRMDELETE,
                    Resources.Msg.MSG_NOSELECTITEM
                });
    }

    protected override void LoadData()
    {
        base.LoadData();

        PageDataSource = DBConn.RunStoreProcedure(
            Constants.SP_GETNOTICELIST,
            new string[] {
                "@kind",
                ddlSearchKind.SelectedValue
            },
            new object[] {
                Constants.NOTICEKIND_MESSAGE,
                (string.IsNullOrEmpty(tbxSearchValue.Text) ? null : tbxSearchValue.Text)
            });
    }

    protected override void InitControls()
    {
        base.InitControls();

        ddlSearchKind.Items.Clear();
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_LOGINID, "@loginid"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_NAME, "@nick"));
        ddlSearchKind.Items.Add(new ListItem(Resources.Str.STR_CONTENT, "@htmlcontent"));

        if (Request.Params["loginid"] != null)
        {
            ddlSearchKind.SelectedValue = "loginid";
            tbxSearchValue.Text = Request.Params["loginid"];
        }
    }

    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "deldate") != DBNull.Value)
                e.Row.Style[HtmlTextWriterStyle.BackgroundColor] = "#D6F7FF";

            e.Row.Cells[3].Attributes["onclick"] = "onViewMsg(" + Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "id")) + ")";
            e.Row.Cells[3].Style[HtmlTextWriterStyle.Cursor] = "pointer";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["chkNo"]))
            return;

        string[] strIDs = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < strIDs.Length; i++)
        {
            DBConn.RunStoreProcedure(Constants.SP_DELETENOTICE,
                new string[] { "@id", "@deldate", "@deltype" }, new object[] { Convert.ToInt32(strIDs[i]), CurrentDate, 1 });
        }

        PageDataSource = null;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }
}
