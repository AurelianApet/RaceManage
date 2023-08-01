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

public partial class NoticeMng_NoticeMng : Ronaldo.uibase.PageBase
{
    public long lTotalCount = 0;
    protected override GridView getGridControl()
    {
        return gvContent;
    }
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
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
    protected override void InitControls()
    {
        base.InitControls();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["chkNo"]))
            return;

        string[] strIDs = Request.Form["chkNo"].Split(',');
        for (int i = 0; i < strIDs.Length; i++)
        {
            DBConn.RunStoreProcedure(Constants.SP_DELETENOTICE,
                new string[] { "@id" }, new object[] { Convert.ToInt32(strIDs[i]) });
        }

        PageDataSource = null;
        BindData();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("NoticeReg.aspx?page=" + PageNumber);
    }
    protected override void LoadData()
    {
        base.LoadData();

        System.Collections.Generic.Dictionary<string, object> dicParmas = new System.Collections.Generic.Dictionary<string, object>();

        dicParmas.Add("@kind", Constants.NOTICEKIND_NOTICE);
        
        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETNOTICELIST, dicParmas);
        lTotalCount = DataSetUtil.RowCount(PageDataSource);
    }

    protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }

    protected override void BindData()
    {
        base.BindData();

        if (DataSetUtil.IsNullOrEmpty(PageDataSource))
            btnDelete.Enabled = false;
    }
}
