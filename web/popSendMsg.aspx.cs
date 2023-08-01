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


public partial class popSendMsg : Ronaldo.uibase.PageBase
{
    protected override void LoadData()
    {
        base.LoadData();

        if (!string.IsNullOrEmpty(Request.Params["sendid"]))
            tbxRecvID.Text = Request.Params["sendid"];
    }

    protected override void InitControls()
    {
        base.InitControls();

        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem(Resources.Str.STR_MSG_SYSTEM, Constants.MSGTYPE_SYSTEM.ToString()));
        ddlType.Items.Add(new ListItem(Resources.Str.STR_MSG_CHARGE, Constants.MSGTYPE_CHARGE.ToString()));
        ddlType.Items.Add(new ListItem(Resources.Str.STR_MSG_DISCHARGE, Constants.MSGTYPE_DISCHARGE.ToString()));

        ddlSite.Items.Clear();
        ddlSite.Items.Add(new ListItem(Resources.Str.STR_WHOLE, "0"));
        DataSet dsSite = DBConn.RunStoreProcedure(Constants.SP_GETSITELIST);
        for (int i = 0; i < DataSetUtil.RowCount(dsSite); i++)
        {
            ddlSite.Items.Add(new ListItem(DataSetUtil.RowStringValue(dsSite, "site_name", i), DataSetUtil.RowStringValue(dsSite, "id", i)));
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        //writeLog(string.Format("쪽지보내기 요청 아이디:{0}", AuthUser.LoginID));
        if (string.IsNullOrEmpty(tbxTitle.Text))
        {
            ShowMessageBox(Resources.Err.ERR_TITLE_INPUT);
            return;
        }
        if (string.IsNullOrEmpty(tbxContent.Text))
        {
            ShowMessageBox(Resources.Err.ERR_CONTENT_INPUT);
            return;
        }

        ArrayList arrRecvID = new ArrayList();
        DataSet dsUser = null;
        if (tbxRecvID.Text == Resources.Str.STR_RECVALL && tbxRecvID.Enabled == false)
        {
            dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER);

            for (int i = 0; i < DataSetUtil.RowCount(dsUser); i++)
            {
                if (!string.IsNullOrEmpty(DataSetUtil.RowDateTimeValue(dsUser, "leavedate", i)) ||
                    !string.IsNullOrEmpty(DataSetUtil.RowDateTimeValue(dsUser, "interceptdate", i)))
                    continue;

                arrRecvID.Add(DataSetUtil.RowLongValue(dsUser, "id", i));
            }
        }
        else if (tbxRecvID.Text == Resources.Str.STR_SITE && tbxRecvID.Enabled == false)
        {
            dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                new string[] { "@site" }, new object[] { ddlSite.SelectedValue });

            for (int i = 0; i < DataSetUtil.RowCount(dsUser); i++)
            {
                if (!string.IsNullOrEmpty(DataSetUtil.RowDateTimeValue(dsUser, "leavedate", i)) ||
                    !string.IsNullOrEmpty(DataSetUtil.RowDateTimeValue(dsUser, "interceptdate", i)))
                    continue;

                arrRecvID.Add(DataSetUtil.RowLongValue(dsUser, "id", i));
            }
        }
        else
        {
            string[] arrID = tbxRecvID.Text.Split(',');
            for (int i = 0; i < arrID.Length; i++)
            {
                if (string.IsNullOrEmpty(arrID[i]))
                    continue;

                dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                    new string[] { "@loginid" }, new object[] { arrID[i] });

                if (DataSetUtil.IsNullOrEmpty(dsUser) ||
                    !string.IsNullOrEmpty(DataSetUtil.RowDateTimeValue(dsUser, "leavedate", 0)) ||
                    !string.IsNullOrEmpty(DataSetUtil.RowDateTimeValue(dsUser, "interceptdate", 0)))
                    continue;

                arrRecvID.Add(DataSetUtil.RowLongValue(dsUser, "id", 0));
            }
        }

        if (arrRecvID.Count < 1)
        {
            ShowMessageBox(Resources.Err.ERR_LOGINID_INVALID);
            return;
        }

        long lMsgCount = 0;
        for (int i = 0; i < arrRecvID.Count; i++)
        {
            long lID = Convert.ToInt64(arrRecvID[i]);
            if (lID == AuthUser.ID)
                continue;

            dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER, new string[] { "@id" }, new object[] { lID });
            if (!DataSetUtil.IsNullOrEmpty(dsUser))
            {
                if (!DataSetUtil.IsNullOrEmpty(DBConn.RunStoreProcedure(Constants.SP_CREATENOTICE,
                    new string[] {
                        "@title",
                        "@htmlcontent",
                        "@user_id",
                        "@user_loginid",
                        "@user_nick",
                        "@kind",
                        "@w_ip"
                    },
                    new object[] {
                        tbxTitle.Text.Trim(),
                        text2Html(tbxContent.Text),
                        lID,
                        DataSetUtil.RowStringValue(dsUser, "loginid", 0),
                        DataSetUtil.RowStringValue(dsUser, "nick", 0),
                        Constants.NOTICEKIND_MESSAGE,//ddlType.SelectedValue,
                        Request.ServerVariables["REMOTE_ADDR"]
                    })))
                    lMsgCount++;
            }
        }

        AlertAndClose(string.Format(Resources.Msg.MSG_SENDMSG_NOTICE, lMsgCount));
    }

    protected void chkRecvSite_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRecvSite.Checked)
        {
            tbxRecvID.Text = Resources.Str.STR_SITE;
            tbxRecvID.Enabled = false;
            chkRecvAll.Checked = false;
            ddlSite.Enabled = true;
        }
        else
        {
            tbxRecvID.Text = "";
            tbxRecvID.Enabled = true;
            ddlSite.Enabled = false;
        }
    }

    protected void chkRecvAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRecvAll.Checked)
        {
            tbxRecvID.Text = Resources.Str.STR_RECVALL;
            tbxRecvID.Enabled = false;
            ddlSite.SelectedIndex = 0;
            ddlSite.Enabled = false;
            chkRecvSite.Checked = false;
        }
        else
        {
            tbxRecvID.Text = "";
            tbxRecvID.Enabled = true;
        }
    }
}
