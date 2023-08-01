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

public partial class MemberMng_SosokMng : Ronaldo.uibase.PageBase
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
    protected override void LoadData()
    {
        base.LoadData();

        Dictionary<string, object> dicParams = new Dictionary<string, object>();

        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
            dicParams.Add("@site_name", tbxSearchValue.Text);

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST, dicParams);
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

            int nSosokID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "id"));
            DataSet dsUsers = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                new string[]{
                    "@site"
                },
                new object[]{
                    nSosokID
                });
            int nUsers = (DataSetUtil.IsNullOrEmpty(dsUsers)) ? 0 : DataSetUtil.RowCount(dsUsers);
            setLiteralValue(e.Row, "ltlUserNum", string.Format("{0}名", nUsers));
        }
    }
    protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long lID = 0;
        switch (e.CommandName)
        {
            case "updateSite":
                lID = Convert.ToInt64(e.CommandArgument);
                DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST,
                                new string[] { "@site_id" }, new object[] { lID });
                if (!DataSetUtil.IsNullOrEmpty(dsSite))
                {
                    string strOldName = DataSetUtil.RowStringValue(dsSite, "site_name", 0);
                    string strSiteName = Convert.ToString(Request.Form["siteName" + lID]).Trim();
                    string strDomainName = Convert.ToString(Request.Form["siteDomain" + lID]).Trim();
                    if (!string.IsNullOrEmpty(strSiteName))
                    {
                        if (strSiteName != strOldName)
                        {
                            dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST,
                                    new string[] { "@match_name" }, new object[] { strSiteName });
                            if (!DataSetUtil.IsNullOrEmpty(dsSite))
                            {
                                ShowMessageBox(Resources.Err.ERR_SITE_EXIST);
                                break;
                            }

                            DBConn.RunStoreProcedure(Constants.SP_UPDATESITE,
                                new string[] { "@site_id", "@site_name", "@site_url" },
                                new object[] {
                                    lID,
                                    strDomainName
                                });
                        }
                        else
                        {
                            DBConn.RunStoreProcedure(Constants.SP_UPDATESITE,
                                new string[] { "@site_id", "@site_url" },
                                new object[] {
                                    lID,
                                    strDomainName
                                });
                        }
                    }
                }
                break;
            case "deleteSite":
                lID = Convert.ToInt64(e.CommandArgument);
                DBConn.RunStoreProcedure(Constants.SP_DELETESITE,
                        new string[] {
                        "@site_id"
                    },
                        new object[] {
                        lID
                    });
                break;
        }
        btnSearch_Click(null, null);
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
                DBConn.RunStoreProcedure(Constants.SP_DELETESITE,
                    new string[] {
                        "@site_id"
                    },
                    new object[] {
                        Convert.ToInt64(arrSelID[i])
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(tbxSiteName.Text))
        {
            ShowMessageBox(Resources.Err.ERR_NAME_INPUT);
            return;
        }
        if (string.IsNullOrEmpty(tbxSiteDomain.Text))
        {
            ShowMessageBox(Resources.Err.ERR_NAME_DOMAIN);
            return;
        }
        DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST,
                            new string[]
                            {
                                "@match_name",
                                "@url"
                            },
                            new object[]
                            {
                                tbxSiteName.Text.Trim(),
                                tbxSiteDomain.Text.Trim()
                            });
        if (!DataSetUtil.IsNullOrEmpty(dsSite))
        {
            ShowMessageBox(Resources.Err.ERR_SITE_EXIST);
            return;
        }
        DBConn.RunStoreProcedure(Constants.SP_CREATESITE,
                            new string[]
                            {
                                "@site_name",
                                "@site_url",
                            },
                            new object[]
                            {
                                tbxSiteName.Text.Trim(),
                                tbxSiteDomain.Text.Trim(),
                            });
        tbxSiteName.Text = "";
        btnSearch_Click(null, null);
    }
}
