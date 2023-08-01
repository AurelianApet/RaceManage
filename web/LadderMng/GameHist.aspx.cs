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

public partial class LadderMng_GameHist : Ronaldo.uibase.PageBase
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
                    "MSG_CONFIRM_VALID",
                    "MSG_CONFIRM_CALC",
                    "MSG_UPDATE_RESULT"
                },
                new string[]
                {         
                    Resources.Msg.MSG_CONFIRM_VALID,
                    Resources.Msg.MSG_CONFIRM_CALC,
                    Resources.Msg.MSG_UPDATE_RESULT
                });
    }

    protected override void LoadData()
    {
        base.LoadData();

        if (!IsPostBack)
        {
            StartDate = DateTime.Parse(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00"));
            EndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETGAMELIST,
            new string[] {
                "@lottery",
                "@sdate",
                "@edate"
            },
            new object[] {
                Constants.GAMETYPE_LADDER,
                StartDate,
                EndDate
            });
    }

    protected override void BindData()
    {
        base.BindData();

        tbxStartDate.Text = StartDate.ToString("yyyy-MM-dd");
        tbxEndDate.Text = EndDate.ToString("yyyy-MM-dd");
        DataSet dsConfig = DBConn.RunSelectQuery(Constants.SP_GETCONFIG, new string[] { "@type" }, new object[] { Constants.GAMETYPE_LADDER });

        Dictionary<string, string> dicConfigs = new Dictionary<string, string>();
        for (int i = 0; i < DataSetUtil.RowCount(dsConfig); i++)
            dicConfigs.Add(DataSetUtil.RowStringValue(dsConfig, "conf_name", i), DataSetUtil.RowStringValue(dsConfig, "conf_value", i));

        int nGameResultStatus = Convert.ToInt32(dicConfigs["ladder_result_mode"]);
        string strGameStatus = "";
        if (nGameResultStatus == 0)
            strGameStatus = Resources.Str.STR_GAMERESULT_PASSIVE;
        else if (nGameResultStatus == 1)
            strGameStatus = string.Format("<font color='green'>{0}</font>", Resources.Str.STR_GAMERESULT_AUTO);
        else if (nGameResultStatus == 2)
            strGameStatus = string.Format("<font color='blue'>{0}</font>", Resources.Str.STR_GAMERESULT_OBTAIN);
        ltlGameStatus.Text = strGameStatus;
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
            LinkButton lnkValid = (LinkButton)e.Row.FindControl("lnkValid");
            LinkButton lnkCalc = (LinkButton)e.Row.FindControl("lnkCalc");

            int iComplete = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "complete"));

            string strStatus = Resources.Str.STR_GAMMING;

            if (iComplete == Constants.GAME_NOCALC)
            {
                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "startpos")) != 0)
                {
                    strStatus = "<font color='orange'>" + Resources.Str.STR_CALCWAIT + "</font>";
                    lnkCalc.Visible = true;
                }
                else
                    lnkCalc.Visible = false;
            }
            if (iComplete == Constants.GAME_CALCED)
            {
                strStatus = "<font color='blue'>" + Resources.Str.STR_CALCEND + "</font>";
                lnkCalc.Visible = false;
            }
            else if (iComplete == Constants.GAME_VALID)
            {
                strStatus = "<font color='red'>" + Resources.Str.STR_VALID + "</font>";
                lnkValid.Visible = false;
                lnkCalc.Visible = false;
            }

            setLiteralValue(e.Row, "ltlStatus", strStatus);
        }
    }

    protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void btnSelTOdd_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_LADDER });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@startpos",
                "@laddercount",
                "@oddeven",
                "@id",
                "@lottery"
            },
            new object[] { 
                2,
                1,
                1,
                lGameID,
                Constants.GAMETYPE_LADDER
            });

        PageDataSource = null;
        BindData();
    }

    protected void btnSelTEven_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_LADDER });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@startpos",
                "@laddercount",
                "@oddeven",
                "@id",
                "@lottery"
            },
            new object[] { 
                1,
                1,
                2,
                lGameID,
                Constants.GAMETYPE_LADDER
            });

        PageDataSource = null;
        BindData();
    }

    protected void btnSelFOdd_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_LADDER });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@startpos",
                "@laddercount",
                "@oddeven",
                "@id",
                "@lottery"
            },
            new object[] { 
                1,
                2,
                1,
                lGameID,
                Constants.GAMETYPE_LADDER
            });

        PageDataSource = null;
        BindData();
    }

    protected void btnSelFEven_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_LADDER });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@startpos",
                "@laddercount",
                "@oddeven",
                "@id",
                "@lottery"
            },
            new object[] { 
                2,
                2,
                2,
                lGameID,
                Constants.GAMETYPE_LADDER
            });

        PageDataSource = null;
        BindData();
    }
}
