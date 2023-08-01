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

public partial class loginuser1 : Ronaldo.uibase.PageBase
{
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

        dicParams.Add("@time", SiteConfig.LoginMinutes);
        if (!string.IsNullOrEmpty(tbxSearchValue.Text))
            dicParams.Add("@" + ddlSearchKind.SelectedValue, tbxSearchValue.Text);

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETLOGINS, dicParams);
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
            strUrl = strUrl.Substring(nPosDomain, (nPosDomain < 0) ? strUrl.Length : strUrl.Length - nPosDomain);

            setLiteralValue(e.Row, "ltlConnPath", strUrl);

            long lUserID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "id").ToString());
            DataSet dsMoney = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTINFO,
                new string[]{
                "@user_id",
                "@sdate",
                "@edate"
            },
            new object[]{
                lUserID,
                DateTime.Now.ToString("yyyy-MM-dd 00:00:00"),
                DateTime.Now.ToString("yyyy-MM-dd 23:59:59")
            });
            if (!DataSetUtil.IsNullOrEmpty(dsMoney))
            {
                setLiteralValue(e.Row, "ltlChargeMoney", string.Format("{0:N0}", DataSetUtil.RowLongValue(dsMoney, "chargemoney", 0)));
                setLiteralValue(e.Row, "ltlDisChargeMoney", string.Format("{0:N0}", DataSetUtil.RowLongValue(dsMoney, "dischargemoney", 0)));
                setLiteralValue(e.Row, "ltlTotalChargeMoney", string.Format("{0:N0}", DataSetUtil.RowLongValue(dsMoney, "totalchargemoney", 0)));
                setLiteralValue(e.Row, "ltlTotalDisChargeMoney", string.Format("{0:N0}", DataSetUtil.RowLongValue(dsMoney, "totaldischargemoney", 0)));
                setLiteralValue(e.Row, "ltlBenifitMoney", string.Format("{0:N0}", DataSetUtil.RowLongValue(dsMoney, "totalchargemoney", 0) - DataSetUtil.RowLongValue(dsMoney, "totaldischargemoney", 0)));
            }
            else
            {
                setLiteralValue(e.Row, "ltlChargeMoney", "0");
            }


        }
    }
    protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long lID = 0;
        string strIP = "";
        switch (e.CommandName)
        {
            case "IpIntercept":
                strIP = Convert.ToString(e.CommandArgument);
                if (SiteConfig.InterceptIP.Contains(strIP))
                    break;
                strIP = SiteConfig.InterceptIP + strIP + "\r\n";
                DBConn.RunSelectQuery("update configs set cf_intercept_ip = @cf_intercept_ip",
                        new string[]{
                            "@cf_intercept_ip"
                        },
                        new object[]{
                            strIP
                        });
                break;
            case "Logout":
                lID = Convert.ToInt64(e.CommandArgument);
                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSER,
                        new string[]{
                            "@id",
                            "@ismustlogout"
                        },
                        new object[]{
                            lID,
                            1
                        });
                //Response.Redirect("GameReg.aspx?id=" + lID + "&type=" + Constants.GAMEUPDATE_ALL + "&page=" + PageNumber + "&retpage=standby&rettype=" + ddlKind.SelectedValue + "&sec=" + ddlSection.SelectedValue + "&lea=" + ddlLeague.SelectedValue + "&skey=" + ddlSearchKey.SelectedValue + "&sval=" + tbxSearchValue.Text);
                break;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageDataSource = null;
        BindData();
    }
}
