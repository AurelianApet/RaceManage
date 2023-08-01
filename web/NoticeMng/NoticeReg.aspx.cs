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

public partial class NoticeMng_NoticeReg : Ronaldo.uibase.PageBase
{
    protected int _noticeID = 0;

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
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
        if (string.IsNullOrEmpty(tbxWriter.Text))
        {
            ShowMessageBox(Resources.Err.ERR_WRITER_INPUT);
            return;
        }

        _noticeID = Convert.ToInt32(hdNoticeID.Value);

        // 수정
        if (_noticeID > 0)
        {
            if (DataSetUtil.IsNullOrEmpty(DBConn.RunStoreProcedure(
                Constants.SP_UPDATENOTICE,
                new string[] {
                    "@id",
                    "@title",
                    "@htmlcontent",
                    "@writer",
                    "@w_ip"
                },
                new object[] {
                    _noticeID,
                    tbxTitle.Text,
                    tbxContent.Text,
                    tbxWriter.Text,
                    Request.ServerVariables["REMOTE_ADDR"]
                })))
            {
                ShowMessageBox(Resources.Msg.MSG_UPDATENOTICE_FAILED);
                return;
            }

            ShowMessageBox(Resources.Msg.MSG_UPDATENOTICE_SUCCESS);
        }
        // 등록
        else
        {
            if (DataSetUtil.IsNullOrEmpty(DBConn.RunStoreProcedure(
                Constants.SP_CREATENOTICE,
                new string[] {
                    "@title",
                    "@htmlcontent",
                    "@user_id",
                    "@user_loginid",
                    "@user_nick",
                    "@writer",
                    "@kind",
                    "@w_ip"
                },
                new object[] {
                    tbxTitle.Text,
                    tbxContent.Text,
                    AuthUser.ID,
                    AuthUser.LoginID,
                    AuthUser.NickName,
                    tbxWriter.Text,
                    Constants.NOTICEKIND_NOTICE,
                    UserIP
                })))
            {
                ShowMessageBox(Resources.Msg.MSG_REGNOTICE_FAILED);
                return;
            }

            ShowMessageBox(Resources.Msg.MSG_REGNOTICE_SUCCESS);
        }

        btnList_Click(null, null);
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("NoticeMng.aspx?page=" + PageNumber);
    }

    protected override void LoadData()
    {
        base.LoadData();

        if (Request.Params["id"] != null)
        {
            _noticeID = Convert.ToInt32(Request.Params["id"]);

            PageDataSource = DBConn.RunStoreProcedure(
                Constants.SP_GETNOTICEINFO,
                new string[] { "@id" },
                new object[] { _noticeID });

            if (DataSetUtil.IsNullOrEmpty(PageDataSource))
            {
                Alert(Resources.Msg.MSG_NOEXISTDATA, "NoticeMng.aspx?page=" + PageNumber);
                return;
            }
        }

        if (!string.IsNullOrEmpty(Request.Params["ret"]))
            hdRetType.Value = Request.Params["ret"];
    }

    protected override void BindData()
    {
        base.BindData();

        if (!DataSetUtil.IsNullOrEmpty(PageDataSource))
        {
            tbxTitle.Text = DataSetUtil.RowStringValue(PageDataSource, "title", 0);
            tbxContent.Text = DataSetUtil.RowStringValue(PageDataSource, "htmlcontent", 0);
            btnOK.Text = Resources.Str.STR_UPDATE;
            
            tbxWriter.Text = DataSetUtil.RowStringValue(PageDataSource, "writer", 0);
            if (string.IsNullOrEmpty(tbxWriter.Text))
                tbxWriter.Text = AuthUser.NickName;
        }
        else
        {
            tbxTitle.Text = "";
            tbxContent.Text = "";
            btnOK.Text = Resources.Str.STR_CONFIRM;

            tbxWriter.Text = AuthUser.NickName;
        }

        hdNoticeID.Value = _noticeID.ToString();
    }

    protected override void InitControls()
    {
        base.InitControls();
    }
}
