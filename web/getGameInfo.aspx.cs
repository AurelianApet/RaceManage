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

public partial class getGameInfo : Ronaldo.uibase.AjaxPageBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        Response.Clear();
        Response.ContentType = "text/json";
        Response.ContentEncoding = System.Text.Encoding.UTF8;

        double fAWinMoney = 0;
        double fBWinMoney = 0;
        double fCWinMoney = 0;

        long lGameID = 0;
        long lGameRound = 0;
        double fBetMoney = 0;
        int nLeftTime = 0;
        int nRefresh = 0;
        try
        {

            DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_RACE });

            lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
            lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
            TimeSpan tsDiff = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0)) - DateTime.Now;
            nLeftTime = (int)tsDiff.TotalSeconds;
            fBetMoney = DataSetUtil.RowDoubleValue(dsGame, "betmoney", 0);

            DataSet dsTmpo = DBConn.RunSelectQuery("select * from tmporesults_race where round = @round", new string[] { "@round" }, new object[] { lGameRound });
            string strSelA = DataSetUtil.RowStringValue(dsTmpo, "high_result", 0);
            string strSelB = DataSetUtil.RowStringValue(dsTmpo, "medium_result", 0);
            string strSelC = DataSetUtil.RowStringValue(dsTmpo, "low_result", 0);

            int[] RankA = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] RankB = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] RankC = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < 10; i++)
            {
                RankA[i] = Convert.ToInt32(strSelA.Split('-')[i]);
                RankB[i] = Convert.ToInt32(strSelB.Split('-')[i]);
                RankC[i] = Convert.ToInt32(strSelC.Split('-')[i]);
            }

            

            DataSet dsBet = DBConn.RunStoreProcedure(Constants.SP_GETBETTINGHIST, new string[] { "@lottery", "@game_id"}, new object[] { Constants.GAMETYPE_RACE, lGameID});

            for (int i = 0; i < DataSetUtil.RowCount(dsBet); i++)
            {
                double fBet = DataSetUtil.RowDoubleValue(dsBet, "betmoney", i);
                double fRatio = DataSetUtil.RowDoubleValue(dsBet, "ratio", i);
                int nBetmode = DataSetUtil.RowIntValue(dsBet, "betpos", i);
                string strBetVal = DataSetUtil.RowStringValue(dsBet, "betval", i);
                int nResult = 0;
                double fWin = 0.0f;
                if (getBetResult(RankA[0], RankA[1], RankA[2], RankA[3], RankA[4], RankA[5], RankA[6], RankA[7], RankA[8], RankA[9], nBetmode, strBetVal, fBet, fRatio, out nResult, out fWin))
                    fAWinMoney += fWin;
                if (getBetResult(RankB[0], RankB[1], RankB[2], RankB[3], RankB[4], RankB[5], RankB[6], RankB[7], RankB[8], RankB[9], nBetmode, strBetVal, fBet, fRatio, out nResult, out fWin))
                    fBWinMoney += fWin;
                if (getBetResult(RankC[0], RankC[1], RankC[2], RankC[3], RankC[4], RankC[5], RankC[6], RankC[7], RankC[8], RankC[9], nBetmode, strBetVal, fBet, fRatio, out nResult, out fWin))
                    fCWinMoney += fWin;
            }

            DataSet dsStatus = DBConn.RunSelectQuery("select * from status");
            nRefresh = DataSetUtil.RowIntValue(dsStatus, "refresh", 0);
            if (nRefresh == 1)
            {
                writeLog("정산으로 인한 리프래시 진행");
                DBConn.RunSelectQuery("update status set refresh = 0");
            }
        }
        catch (Exception ex)
        {
            writeLog(ex.Message);
            Response.Write("{\"round\":\"0\", \"lefttime\":\"1\", \"awin\":\"0\", \"bwin\":\"0\", \"cwin\":\"0\", \"betmoney\":\"0\", \"refresh\":\"0\"}");
            Response.End();
        }
        Response.Write("{\"round\":\"" + lGameRound +
                    "\", \"lefttime\":\"" + nLeftTime +
                    "\", \"awin\":\"" + string.Format( (fBetMoney > fAWinMoney ? "+" : "") + "{0:F2}", fBetMoney - fAWinMoney) +
                    "\", \"bwin\":\"" + string.Format( (fBetMoney > fBWinMoney ? "+" : "") + "{0:F2}", fBetMoney - fBWinMoney) +
                    "\", \"cwin\":\"" + string.Format( (fBetMoney > fCWinMoney ? "+" : "") + "{0:F2}", fBetMoney - fCWinMoney) +
                    "\", \"betmoney\":\"" + string.Format("{0:F2}", fBetMoney) +
                    "\", \"refresh\":\"" + nRefresh + "\"}");

        Response.End();
    }
}
