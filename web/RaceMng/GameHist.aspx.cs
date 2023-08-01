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

public partial class GameMng_GameHist : Ronaldo.uibase.PageBase
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
            StartDate = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00"));
            EndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETGAMELIST,
            new string[] {
                "@lottery",
                "@sdate",
                "@edate"
            },
            new object[] {
                Constants.GAMETYPE_RACE,
                StartDate,
                EndDate
            });
    }
    protected override void BindData()
    {
        base.BindData();

        tbxStartDate.Text = StartDate.ToString("yyyy-MM-dd");
        tbxEndDate.Text = EndDate.ToString("yyyy-MM-dd");
        DataSet dsConfig = DBConn.RunSelectQuery(Constants.SP_GETCONFIG, new string[] { "@type" }, new object[] { Constants.GAMETYPE_RACE });

        Dictionary<string, string> dicConfigs = new Dictionary<string, string>();
        for (int i = 0; i < DataSetUtil.RowCount(dsConfig); i++)
            dicConfigs.Add(DataSetUtil.RowStringValue(dsConfig, "conf_name", i), DataSetUtil.RowStringValue(dsConfig, "conf_value", i));

        int nGameResultStatus = Convert.ToInt32(dicConfigs["race_result_mode"]);
        string strGameStatus = "";
        if (nGameResultStatus == 0)
            strGameStatus = Resources.Str.STR_GAMERESULT_PASSIVE;
        else if (nGameResultStatus == 1)
            strGameStatus = string.Format("<font color='green'>{0}</font>", Resources.Str.STR_GAMERESULT_AUTO);
        else if(nGameResultStatus == 2)
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
                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "rank1")) != 0)
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
        long lID = 0;
        int iTotalCount = 0;

        if (e.CommandName == "CmdValid")
        {
            lID = Convert.ToInt64(e.CommandArgument);

            // 게임내역표에서 현재 무효처리하려는 게임의 상태를 얻는다.
            DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETGAMES,
                new string[] {
                    "@id"
                },
                new object[] {
                    lID
                });
            if (DataSetUtil.IsNullOrEmpty(dsGame))
                return;

            int iComplete = DataSetUtil.RowIntValue(dsGame, "complete", 0);
            if (iComplete == Constants.GAME_VALID)
                return;

            // 베팅내역표를 보고 유저머니를 변경시킨다.
            DataSet dsBet = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST,
                new string[] {
                    "@game_id"
                },
                new object[] {
                    lID
                });

            for (int i = 0; i < DataSetUtil.RowCount(dsBet); i++)
            {
                long iBetID = DataSetUtil.RowLongValue(dsBet, "id", i);
                double fBetMoney = DataSetUtil.RowDoubleValue(dsBet, "betmoney", i);
                double fRatio = DataSetUtil.RowDoubleValue(dsBet, "ratio", i);
                long lUserID = DataSetUtil.RowLongValue(dsBet, "userid", i);
                int iBetResult = DataSetUtil.RowIntValue(dsBet, "result", i);
                double fResultMoney = DataSetUtil.RowDoubleValue(dsBet, "winmoney", 0);

                // 베팅머니 돌려준다.
                InsertCash(lUserID, fBetMoney, 0, Resources.Desc.DESC_VALIDBETMONEY, iBetID.ToString());

                // 만일 당첨되었으면 결과머니 반환
                if (iBetResult == 2)
                {
                    InsertCash(lUserID, 0, fResultMoney, Resources.Desc.DESC_VALIDWINMONEY, iBetID.ToString());
                }

                //베팅내역표를 갱신한다.
                DBConn.RunStoreProcedure(Constants.SP_UPDATEBETTINGHIST,
                    new string[] { "@id", "@result" }, new object[] { iBetID, Constants.BETSTAT_VALID });

                iTotalCount++;
            }

            // 게임내역표의 결과를 무효로 놓는다.
            DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
                new string[] {
                    "@id",
                    "@complete"
                },
                new object[] {
                    lID,
                    Constants.GAME_VALID
                });

            ShowMessageBox(string.Format(Resources.Msg.MSG_PROCESS_COMPLETED, iTotalCount));
            PageDataSource = null;
            BindData();
        }
        else if (e.CommandName == "CmdCalc")
        {
            lID = Convert.ToInt64(e.CommandArgument);

            // 베팅정보를 얻는다.
            DataSet dsBet = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST,
                new string[] {
                    "@game_id"
                },
                new object[] {
                    lID
                });

            for (int i = 0; i < DataSetUtil.RowCount(dsBet); i++)
            {
                long iBetID = DataSetUtil.RowLongValue(dsBet, "id", i);
                double fBetMoney = DataSetUtil.RowDoubleValue(dsBet, "betmoney", i);
                double fRatio = DataSetUtil.RowDoubleValue(dsBet, "ratio", i);
                long lUserID = DataSetUtil.RowLongValue(dsBet, "userid", i);
                int iBetResult = DataSetUtil.RowIntValue(dsBet, "result", i);

                double fResultMoney = DataSetUtil.RowDoubleValue(dsBet, "winmoney", i);

                // 만일 당첨되었으면 당첨금 지급
                if (iBetResult == Constants.BETSTAT_WIN)
                {
                    InsertCash(lUserID, fResultMoney, 0, Resources.Desc.DESC_WIN, iBetID.ToString());
                }
                iTotalCount++;
            }

            DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
                new string[] {
                    "@id",
                    "@complete"
                },
                new object[] {
                    lID,
                    Constants.GAME_CALCED
                });

            ShowMessageBox(string.Format(Resources.Msg.MSG_PROCESS_COMPLETED, iTotalCount));
            PageDataSource = null;
            BindData();
        }
        else if (e.CommandName == "CmdUpdate")
        {
            lID = Convert.ToInt64(e.CommandArgument);

            int iVal1 = Convert.ToInt32(Request.Form["selVal1" + lID]);
            int iVal2 = Convert.ToInt32(Request.Form["selVal2" + lID]);
            int iVal3 = Convert.ToInt32(Request.Form["selVal3" + lID]);
            int iVal4 = Convert.ToInt32(Request.Form["selVal4" + lID]);
            int iVal5 = Convert.ToInt32(Request.Form["selVal5" + lID]);
            int iVal6 = Convert.ToInt32(Request.Form["selVal6" + lID]);
            int iVal7 = Convert.ToInt32(Request.Form["selVal7" + lID]);
            int iVal8 = Convert.ToInt32(Request.Form["selVal8" + lID]);
            int iVal9 = Convert.ToInt32(Request.Form["selVal9" + lID]);
            int iVal10 = Convert.ToInt32(Request.Form["selVal10" + lID]);

            DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
                new string[] {
                    "@id",
                    "@rank1",
                    "@rank2",
                    "@rank3",
                    "@rank4",
                    "@rank5",
                    "@rank6",
                    "@rank7",
                    "@rank8",
                    "@rank9",
                    "@rank10"
                },
                new object[] {
                    lID,
                    iVal1,
                    iVal2,
                    iVal3,
                    iVal4,
                    iVal5,
                    iVal6,
                    iVal7,
                    iVal8,
                    iVal9,
                    iVal10
                });

            DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETGAMES,
                new string[] { "@id" }, new object[] { lID });
            if (DataSetUtil.IsNullOrEmpty(dsGame))
                return;

            int iComplete = DataSetUtil.RowIntValue(dsGame, "complete", 0);
            if (iComplete == Constants.GAME_CALCED)
            {
                DataSet dsBet = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST,
                    new string[] {
                        "@game_id"
                    },
                    new object[] {
                        lID
                    });

                for (int i = 0; i < DataSetUtil.RowCount(dsBet); i++)
                {
                    long lBetID = DataSetUtil.RowLongValue(dsBet, "id", i);
                    long lUserID = DataSetUtil.RowLongValue(dsBet, "userid", i);
                    double fBetMoney = DataSetUtil.RowDoubleValue(dsBet, "betmoney", i);
                    double fRatio = DataSetUtil.RowDoubleValue(dsBet, "ratio", i);
                    int iOldResult = DataSetUtil.RowIntValue(dsBet, "result", i);
                    int nBetmode = DataSetUtil.RowIntValue(dsBet, "betpos", i);
                    int nBetVal = DataSetUtil.RowIntValue(dsBet, "betval", i);
                    double fResultMoney = DataSetUtil.RowDoubleValue(dsBet, "winmoney", i);
                    
                    bool bWin = false;
                    //배팅결과 다시 계산한다.
                    //

                    // 만일 이미 당첨처리되고 새 결과로 당첨이 안된 내역인 경우 당첨금 반환
                    if (iOldResult == Constants.BETSTAT_WIN && !bWin)
                    {
                        InsertCash(lUserID, 0, fResultMoney, Resources.Desc.DESC_WINRECALC, lBetID.ToString());
                        
                        iTotalCount++;
                    }
                    // 만일 이전에 당첨이 안되고 새 결과로 당첨이 된 내역인 경우 당첨금 지급
                    else if ((iOldResult == Constants.BETTSTAT_LOSE || iOldResult == Constants.BETSTATE_READY) && bWin)
                    {
                        InsertCash(lUserID, fResultMoney, 0, Resources.Desc.DESC_WIN, lBetID.ToString());
                        
                        iTotalCount++;
                    }

                    // 배팅내역에서 배팅결과 갱신
                    DBConn.RunStoreProcedure(Constants.SP_UPDATEBETTINGHIST,
                        new string[] {
                            "@id",
                            "@result"
                        },
                        new object[] {
                            lBetID,
                            bWin ? Constants.BETSTAT_WIN : Constants.BETTSTAT_LOSE
                        });
                }
            }
            else
            {
                //해당 게임에 배팅한 배팅정보들을 얻는다.
                DataSet dsBet = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST,
                    new string[] {
                        "@game_id"
                    },
                    new object[] {
                        lID
                    });

                if (!DataSetUtil.IsNullOrEmpty(dsBet))
                {
                    for (int i = 0; i < DataSetUtil.RowCount(dsBet); i++)
                    {
                        long lBetID = Convert.ToInt64(DataSetUtil.RowLongValue(dsBet, "id", i).ToString());
                        double fBetMoney = DataSetUtil.RowDoubleValue(dsBet, "betmoney", i);
                        double fRatio = DataSetUtil.RowDoubleValue(dsBet, "ratio", i);
                        int nBetmode = DataSetUtil.RowIntValue(dsBet, "betpos", i);
                        string strBetVal = DataSetUtil.RowStringValue(dsBet, "betval", i);
                        int nResult = Constants.BETSTATE_READY;
                        double fWinMoney = 0.0f;
                        if (getBetResult(iVal1, iVal2, iVal3, iVal4, iVal5, iVal6, iVal7, iVal8, iVal9, iVal10, nBetmode, strBetVal, fBetMoney, fRatio, out nResult, out fWinMoney))
                        {
                            DBConn.RunStoreProcedure(Constants.SP_UPDATEBETTINGHIST,
                                new string[] {
                                    "@id",
                                    "@result",
                                    "@winmoney"
                                },
                                new object[] {
                                    lBetID,
                                    nResult,
                                    fWinMoney
                                });
                        }
                        
                    }
                }
            }


            ShowMessageBox(string.Format(Resources.Msg.MSG_PROCESS_COMPLETED, iTotalCount));
            PageDataSource = null;
            BindData();
        }
    }

    protected void btnSelA_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_RACE });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DataSet dsTmpo = DBConn.RunSelectQuery("select * from tmporesults_race where round = @round", new string[] { "@round" }, new object[] { lGameRound });

        string strResult = DataSetUtil.RowStringValue(dsTmpo, "high_result", 0);

        int[] Rank = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 10; i++)
        {
            Rank[i] = Convert.ToInt32(strResult.Split('-')[i]);
        }

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@rank1",
                "@rank2",
                "@rank3",
                "@rank4",
                "@rank5",
                "@rank6",
                "@rank7",
                "@rank8",
                "@rank9",
                "@rank10",
                "@id",
                "@lottery"
            },
            new object[] { 
                Rank[0],
                Rank[1],
                Rank[2],
                Rank[3],
                Rank[4],
                Rank[5],
                Rank[6],
                Rank[7],
                Rank[8],
                Rank[9],
                lGameID,
                Constants.GAMETYPE_RACE
            });

        PageDataSource = null;
        BindData();
    }

    protected void btnSelB_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_RACE });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DataSet dsTmpo = DBConn.RunSelectQuery("select * from tmporesults_race where round = @round", new string[] { "@round" }, new object[] { lGameRound });

        string strResult = DataSetUtil.RowStringValue(dsTmpo, "medium_result", 0);

        int[] Rank = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 10; i++)
        {
            Rank[i] = Convert.ToInt32(strResult.Split('-')[i]);
        }

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@rank1",
                "@rank2",
                "@rank3",
                "@rank4",
                "@rank5",
                "@rank6",
                "@rank7",
                "@rank8",
                "@rank9",
                "@rank10",
                "@id",
                "@lottery"
            },
            new object[] { 
                Rank[0],
                Rank[1],
                Rank[2],
                Rank[3],
                Rank[4],
                Rank[5],
                Rank[6],
                Rank[7],
                Rank[8],
                Rank[9],
                lGameID,
                Constants.GAMETYPE_RACE
            });

        PageDataSource = null;
        BindData();
    }

    protected void btnSelC_Click(object sender, EventArgs e)
    {
        DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_RACE });

        long lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
        long lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
        DateTime dtStartTime = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0));

        DataSet dsTmpo = DBConn.RunSelectQuery("select * from tmporesults_race where round = @round", new string[] { "@round" }, new object[] { lGameRound });

        string strResult = DataSetUtil.RowStringValue(dsTmpo, "low_result", 0);

        int[] Rank = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 10; i++)
        {
            Rank[i] = Convert.ToInt32(strResult.Split('-')[i]);
        }

        DBConn.RunStoreProcedure(Constants.SP_UPDATEGAMES,
            new string[] { 
                "@rank1",
                "@rank2",
                "@rank3",
                "@rank4",
                "@rank5",
                "@rank6",
                "@rank7",
                "@rank8",
                "@rank9",
                "@rank10",
                "@id",
                "@lottery"
            },
            new object[] { 
                Rank[0],
                Rank[1],
                Rank[2],
                Rank[3],
                Rank[4],
                Rank[5],
                Rank[6],
                Rank[7],
                Rank[8],
                Rank[9],
                lGameID,
                Constants.GAMETYPE_RACE
            });

        PageDataSource = null;
        BindData();
    }
}
