using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataAccess;
using Ronaldo.common;

public partial class login : Ronaldo.uibase.PageBase
{
    protected override void Page_Init(object sender, EventArgs e)
    {
    }

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (AuthUser != null)
            Response.Redirect(Defines.URL_DEFAULT);
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(tbxLoginID.Text) || tbxLoginID.Text == Resources.Str.STR_LOGINID)
        {
            ShowMessageBox(Resources.Err.ERR_LOGINID_INPUT);
            return;
        }
        if (string.IsNullOrEmpty(tbxLoginPwd.Text) || tbxLoginPwd.Text == Resources.Str.STR_PWD)
        {
            ShowMessageBox(Resources.Err.ERR_PASS_INPUT);
            return;
        }
        if (!UserLogin(tbxLoginID.Text, tbxLoginPwd.Text))
        {
            ShowMessageBox(Resources.Err.ERR_LOGIN_FAILED);
            return;
        }

        Response.Redirect(Defines.URL_DEFAULT);
    }
}
