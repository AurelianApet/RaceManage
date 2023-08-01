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

public partial class getLadderInfo : Ronaldo.uibase.AjaxPageBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        Response.Clear();
        Response.ContentType = "text/json";
        Response.ContentEncoding = System.Text.Encoding.UTF8;

        long lGameID = 0;
        long lGameRound = 0;
        double fBetMoney = 0;
        int nLeftTime = 0;
        int nRefresh = 0;

        double fTOdd = 0.0f, fTEven = 0.0f, fFOdd = 0.0f, fFEven = 0.0f;
        try
        {

            DataSet dsGame = DBConn.RunStoreProcedure(Constants.SP_GETCURRENTGAME, new string[] { "@lottery" }, new object[] { Constants.GAMETYPE_LADDER });

            lGameID = DataSetUtil.RowLongValue(dsGame, "id", 0);
            lGameRound = DataSetUtil.RowLongValue(dsGame, "round", 0);
            DateTime dtSdate = Convert.ToDateTime(DataSetUtil.RowDateTimeValue(dsGame, "sdate", 0)); 
            TimeSpan tsDiff = dtSdate.AddMinutes(5).AddSeconds(-10) - DateTime.Now;
            nLeftTime = (int)tsDiff.TotalSeconds;
            fBetMoney = DataSetUtil.RowDoubleValue(dsGame, "betmoney", 0);

            DataSet dsTmpo = DBConn.RunSelectQuery("select * from tmporesults_ladder where sdate = @sdate and round = @round", new string[] { "@sdate", "@round" }, new object[] { dtSdate, lGameRound });
            fTOdd = DataSetUtil.RowDoubleValue(dsTmpo, "t_odd", 0);
            fTEven = DataSetUtil.RowDoubleValue(dsTmpo, "t_even", 0);
            fFOdd = DataSetUtil.RowDoubleValue(dsTmpo, "f_odd", 0);
            fFEven = DataSetUtil.RowDoubleValue(dsTmpo, "f_even", 0);

            DataSet dsStatus = DBConn.RunSelectQuery("select * from status");
            nRefresh = DataSetUtil.RowIntValue(dsStatus, "refresh_ladder", 0);
            if (nRefresh == 1)
            {
                DBConn.RunSelectQuery("update status set refresh_ladder = 0");
            }
        }
        catch (Exception ex)
        {
            writeLog(ex.Message);
            Response.Write("{\"round\":\"0\", \"lefttime\":\"1\", \"towin\":\"0\", \"tewin\":\"0\", \"fowin\":\"0\", \"fewin\":\"0\", \"betmoney\":\"0\", \"refresh\":\"0\"}");
            Response.End();
        }
        Response.Write("{\"round\":\"" + lGameRound +
                    "\", \"lefttime\":\"" + nLeftTime +
                    "\", \"towin\":\"" + string.Format((fBetMoney > fTOdd ? "+" : "") + "{0:F2}", fBetMoney - fTOdd) +
                    "\", \"tewin\":\"" + string.Format((fBetMoney > fTEven ? "+" : "") + "{0:F2}", fBetMoney - fTEven) +
                    "\", \"fowin\":\"" + string.Format((fBetMoney > fFOdd ? "+" : "") + "{0:F2}", fBetMoney - fFOdd) +
                    "\", \"fewin\":\"" + string.Format((fBetMoney > fFEven ? "+" : "") + "{0:F2}", fBetMoney - fFEven) +
                    "\", \"betmoney\":\"" + string.Format("{0:F2}", fBetMoney) +
                    "\", \"refresh\":\"" + nRefresh + "\"}");

        Response.End();

    }
}
