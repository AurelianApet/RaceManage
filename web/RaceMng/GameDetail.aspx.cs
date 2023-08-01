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

public partial class GameMng_GameDetail : Ronaldo.uibase.PageBase
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

        long lID = 0;
        if (Request.Params["id"] == null ||
            !long.TryParse(Request.Params["id"], out lID))
        {
            ShowMessageBox(Resources.Err.ERR_INVALID_ACCESS, "GameHist.aspx");
            return;
        }

        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETGAMES,
            new string[] { "@lottery", "@id" }, new object[] { Constants.GAMETYPE_RACE, lID });
        if (DataSetUtil.IsNullOrEmpty(dsGame))
        {
            ShowMessageBox(Resources.Err.ERR_INVALID_ACCESS, "GameHist.aspx");
            return;
        }

        ltlTitle.Text = string.Format("{0} - {1}" + Resources.Str.STR_ROUND,
            DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0, "MM-dd"),
            DataSetUtil.RowIntValue(dsGame, "round", 0));

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST,
            new string[] {
                "@lottery",
                "@game_id"
            },
            new object[] {
                Constants.GAMETYPE_RACE,
                lID
            });
    }

    protected override void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        base.gvContent_RowDataBound(sender, e);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long lUserID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "userid"));
            string strBetIP = DataBinder.Eval(e.Row.DataItem, "bet_ip").ToString();
            DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                                    new string[]{
                                        "@id"
                                    },
                                    new object[]{
                                        lUserID
                                    });
            if (!DataSetUtil.IsNullOrEmpty(dsUser))
            {
                string strLoginID = DataSetUtil.RowStringValue(dsUser, "loginid", 0);
                int nLevel = DataSetUtil.RowIntValue(dsUser, "ulevel", 0);
                string strNick = DataSetUtil.RowStringValue(dsUser, "nick", 0);
                string strUserInfo = string.Format("{0} {1}[{2}]", strLoginID, strNick, strBetIP);
                setLiteralValue(e.Row, "ltlUser", strUserInfo);
            }

            int nBetPos = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "betpos").ToString());
            string strBetVal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "betval").ToString());
            string strBetMode = getBetMode(nBetPos);

            
            setLiteralValue(e.Row, "ltlBetMode", strBetMode);
            setLiteralValue(e.Row, "ltrBetPos", strBetVal);

        }
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("GameHist.aspx?page=" + PageNumber);
    }
}
