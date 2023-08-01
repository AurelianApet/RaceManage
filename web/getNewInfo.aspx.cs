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

public partial class getNewInfo : Ronaldo.uibase.AjaxPageBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        Response.Clear();
        Response.ContentType = "text/json";
        Response.ContentEncoding = System.Text.Encoding.UTF8;

        int iChargeRequest = 0;
        int iChargeStandby = 0;
        int iChargeComplete = 0;
        int iDischargeRequest = 0;
        int iDischargeStandby = 0;
        int iDischargeComplete = 0;
        int iMemTotal = 0;
        int iMemNew = 0;
        int iMemNew_0 = 0;

        DataSet dsReq = DBConn.RunStoreProcedure(Constants.SP_GETUNREADREQUEST);
        iChargeRequest = DataSetUtil.RowIntValue(dsReq, "charge", 0);
        iChargeStandby = DataSetUtil.RowIntValue(dsReq, "chargestandby", 0);
        iChargeComplete = DataSetUtil.RowIntValue(dsReq, "chargecomplete", 0);

        iDischargeRequest = DataSetUtil.RowIntValue(dsReq, "discharge", 0);
        iDischargeStandby = DataSetUtil.RowIntValue(dsReq, "dischargestandby", 0);
        iDischargeComplete = DataSetUtil.RowIntValue(dsReq, "dischargecomplete", 0);
        
        iMemTotal = DataSetUtil.RowIntValue(dsReq, "memtotal", 0);
        iMemNew = DataSetUtil.RowIntValue(dsReq, "memnew", 0);
        iMemNew_0 = DataSetUtil.RowIntValue(dsReq, "memnew_0", 0);

        Response.Write("{\"chreq\":\"" + iChargeRequest +
                    "\", \"chstand\":\"" + iChargeStandby +
                    "\", \"chcom\":\"" + iChargeComplete +
                    "\", \"dischreq\":\"" + iDischargeRequest +
                    "\", \"dischstand\":\"" + iDischargeStandby +
                    "\", \"dischcom\":\"" + iDischargeComplete +
                    "\", \"memtotal\":\"" + iMemTotal +
                    "\", \"memnew\":\"" + iMemNew +
                    "\", \"memnew_0\":\"" + iMemNew_0 + "\"}");

        Response.End();
    }
}
