using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Ronaldo.common;
using DataAccess;

namespace Ronaldo.uibase
{
    /// <summary>
    /// Summary description for AjaxPageBase
    /// </summary>
    public class AjaxPageBase : Ronaldo.uibase.PageBase
    {
        public AjaxPageBase()
        {
        }

        protected override void Page_PreInit(object sender, EventArgs e)
        {
            // 디비 초기 연결 작업 시작...
            _dbconn = new MSSqlAccess();
            _dbconn.DBServer = Constants.DB_CONN_HOST;
            _dbconn.DBPort = Constants.DB_CONN_PORT;
            _dbconn.DBName = Constants.DB_CONN_NAME;
            _dbconn.DBID = Constants.DB_CONN_USER;
            _dbconn.DBPwd = Constants.DB_CONN_PASS;
            _dbconn.Connect();
            // 디비 연결 끝

            // 페이지 설정 시작
            strCurDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Session.Timeout = Defines.SESSION_TIMEOUT;
            try
            {
                readSiteConfig();
            }
            catch
            {
                _isInited = false;
                return;
            }
            _isInited = true;
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            checkAuth();
        }

        protected override void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsInited)
                Response.End();
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void Page_PreRender(object sender, EventArgs e)
        {
        }

        protected override void Page_Unload(object sender, EventArgs e)
        {
            base.Page_Unload(sender, e);
        }

    }
}