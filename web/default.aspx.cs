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

public partial class _default : Ronaldo.uibase.PageBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

    protected override void BindData()
    {
        base.BindData();

        DataSet dsCont = DBConn.RunStoreProcedure(Constants.SP_GETDBINFO,
            new string[] {
                "@time",
                "@sdate",
                "@edate"
            },
            new object[] {
                SiteConfig.LoginMinutes,
                DateTime.Now.ToString("yyyy-MM-dd 00:00:00"),
                DateTime.Now.ToString("yyyy-MM-dd 23:59:59")
            });

        if (!DataSetUtil.IsNullOrEmpty(dsCont))
        {
            ltlLoginCount.Text = DataSetUtil.RowLongValue(dsCont, "logincount", 0).ToString();
            ltlLoginCash.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "logincash", 0));
            ltlJoinCount.Text = DataSetUtil.RowLongValue(dsCont, "joincount", 0).ToString();
            ltlLeaveCount.Text = DataSetUtil.RowLongValue(dsCont, "leavecount", 0).ToString();
            ltlTotalJoinCount.Text = DataSetUtil.RowLongValue(dsCont, "totaljoincount", 0).ToString();
            ltlTotalLeaveCount.Text = DataSetUtil.RowLongValue(dsCont, "totalleavecount", 0).ToString();
            ltlMemberCount.Text = DataSetUtil.RowLongValue(dsCont, "membercount", 0).ToString();
            ltlMemberCash.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "membercash", 0));
        }

        dsCont = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTINFO,
            new string[]{
                "@sdate",
                "@edate"
            },
            new object[]{
                DateTime.Now.ToString("yyyy-MM-dd 00:00:00"),
                DateTime.Now.ToString("yyyy-MM-dd 23:59:59")
            });
        if (!DataSetUtil.IsNullOrEmpty(dsCont))
        {
            ltlChargeCount.Text = DataSetUtil.RowLongValue(dsCont, "chargecount", 0).ToString();
            ltlChargeMoney.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "chargemoney", 0));
            ltlDisChargeCount.Text = DataSetUtil.RowLongValue(dsCont, "dischargecount", 0).ToString();
            ltlDisChargeMoney.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "dischargemoney", 0));
            ltlBenifitMoney.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "chargemoney", 0) - DataSetUtil.RowDoubleValue(dsCont, "dischargemoney", 0));
            ltlTotalChargeCount.Text = DataSetUtil.RowLongValue(dsCont, "totalchargecount", 0).ToString();
            ltlTotalChargeMoney.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "totalchargemoney", 0));
            ltlTotalDisChargeCount.Text = DataSetUtil.RowLongValue(dsCont, "totaldischargecount", 0).ToString();
            ltlTotalDisChargeMoney.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "totaldischargemoney", 0));
            ltlTotalBenifitMoney.Text = string.Format("{0:F2}", DataSetUtil.RowDoubleValue(dsCont, "totalchargemoney", 0) - DataSetUtil.RowDoubleValue(dsCont, "totaldischargemoney", 0));
        }

        dsCont = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
            new string[] {
                "@kind",
                "@status",
                "@rowcount"
            },
            new object[] {
                Constants.MONEYINOUT_MODE_CHARGE,
                Constants.MONEYINOUT_STATUS_REQUEST,
                2
            });
        rpCharge.DataSource = dsCont;
        rpCharge.DataBind();

        dsCont = DBConn.RunStoreProcedure(Constants.SP_GETMONEYINOUTS,
            new string[] {
                "@kind",
                "@status",
                "@rowcount"
            },
            new object[] {
                Constants.MONEYINOUT_MODE_DISCHARGE,
                Constants.MONEYINOUT_STATUS_REQUEST,
                2
            });
        rpDischarge.DataSource = dsCont;
        rpDischarge.DataBind();
        
        dsCont = DBConn.RunStoreProcedure(Constants.SP_GETGAMEHIST,
            new string[] {
                "@lottery",
                "@rowcount"
            },
            new object[] {
                Constants.GAMETYPE_RACE,
                5
            });
        rpBetStatus.DataSource = dsCont;
        rpBetStatus.DataBind();

        dsCont = DBConn.RunStoreProcedure(Constants.SP_GETGAMEHIST,
            new string[] {
                "@result",
                "@lottery",
                "@rowcount"
            },
            new object[] {
                1,
                Constants.GAMETYPE_LADDER,
                5
            });
        rpLadderBetStatus.DataSource = dsCont;
        rpLadderBetStatus.DataBind();
    }

    protected override void visibleEmptyRow(object sender, RepeaterItemEventArgs e)
    {
        base.visibleEmptyRow(sender, e);
    }
}
