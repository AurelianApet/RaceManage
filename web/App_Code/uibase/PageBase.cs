using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

using Ronaldo.common;
using Ronaldo.model;

using DataAccess;

namespace Ronaldo.uibase
{
    /// <summary>
    /// Summary description for PageBase
    /// </summary>
    public class PageBase : CompressedViewStatePageBase
    {
        #region 속성 및 상수정의부

        // 경기시간으로부터 몇초전에 배팅마감
        public int TIME_GAMEEND
        {
            get { return 0; }
        }

        //디비
        protected MSSqlAccess _dbconn = null;
        public MSSqlAccess DBConn
        {
            get
            {
                return _dbconn;
            }
        }

        //환경설정
        private Config _config = null;
        public Config SiteConfig
        {
            get { return _config; }
        }

        protected bool _isInited = false;
        public bool IsInited
        {
            get { return _isInited; }
        }

        //현재시간
        protected string strCurDate = null;
        public string CurrentDate
        {
            get { return strCurDate; }
        }

        //접속아이피
        protected string _userip = "";
        public string UserIP
        {
            get { return _userip; }
        }

        //인증된 유저
        public AuthUser AuthUser
        {
            get
            {
                try
                {
                    // 세션에 유저정보가 등록되어있는 경우
                    if (Session[Constants.SESSION_KEY_USERINFO] != null &&
                        Session[Constants.SESSION_KEY_USERINFO] as AuthUser != null)
                    {
                        return Session[Constants.SESSION_KEY_USERINFO] as AuthUser;
                    }
                    else
                    {
                        // 세션에 없다면 쿠키 검사
                        // 쿠키에는 아이디;로그인아이디;암호화된패스워드;닉네임;레벨;추천인아이디;로그인시간 형태로 보관됨
                        if (getCookie(Constants.COOKIE_KEY_USERINFO) != null)
                        {
                            string strCookie = getCookie(Constants.COOKIE_KEY_USERINFO);

                            strCookie = CryptSHA256.Decrypt(strCookie);
                            string[] arrTemp = strCookie.Split(';');

                            // 보관된 자료개수가 정확한 경우
                            if (arrTemp.Length == 7)
                            {
                                // 로그인한 시간을 검사
                                // 만일 하루이전에 로그인한것이면 다시 로그인정보를 세션에 기록함
                                DateTime dtLoginDate = DateTime.Parse(arrTemp[6]);
                                if ((DateTime.Now - dtLoginDate).Hours < 24)
                                {
                                    System.Data.DataSet dsUser = DBConn.RunStoreProcedure(
                                            Constants.SP_GETUSER,
                                                new string[] {
                                                "@loginid"
                                            },
                                            new object[] {
                                                arrTemp[1]
                                            });

                                    if (DataSetUtil.IsNullOrEmpty(dsUser))
                                        return null;

                                    // 쿠키에 저장된 암호와 디비의 암호가 맞지 않는 경우
                                    if (DataSetUtil.RowStringValue(dsUser, "loginpwd", 0) != arrTemp[2])
                                        return null;

                                    AuthUser _authUser = new AuthUser(
                                        DataSetUtil.RowLongValue(dsUser, "id", 0),
                                        DataSetUtil.RowStringValue(dsUser, "loginid", 0),
                                        DataSetUtil.RowStringValue(dsUser, "loginpwd", 0),
                                        DataSetUtil.RowStringValue(dsUser, "nick", 0),
                                        DataSetUtil.RowStringValue(dsUser, "hp", 0),
                                        DataSetUtil.RowIntValue(dsUser, "site", 0));

                                    Session[Constants.SESSION_KEY_USERINFO] = _authUser;
                                    return _authUser;
                                }
                            }
                        }
                    }
                }
                catch { }

                return null;
            }
        }

        //뷰스테이트관련정보
        public int PageNumber
        {
            get
            {
                if (ViewState[Constants.VS_PAGENUMBER] != null)
                    return Convert.ToInt32(ViewState[Constants.VS_PAGENUMBER]);
                else
                    return 0;
            }
            set
            {
                ViewState[Constants.VS_PAGENUMBER] = value;
            }
        }

        private DataSet _dsPageData = null;
        public System.Data.DataSet PageDataSource
        {
            get
            {
                return _dsPageData;
            }
            set
            {
                _dsPageData = value;
            }
        }

        public string SortColumn
        {
            get
            {
                return ViewState[Constants.VS_SORTCOLUMN] as string;
            }
            set
            {
                ViewState[Constants.VS_SORTCOLUMN] = value;
            }
        }
        public SortDirection SortDirection
        {
            get
            {
                return (SortDirection)ViewState[Constants.VS_SORTDIRECTION];
            }
            set
            {
                ViewState[Constants.VS_SORTDIRECTION] = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                DateTime dtRet = DateTime.Now;

                if (dtNow.Hour < 15)
                    dtRet = DateTime.Parse(dtNow.AddDays(-1).ToString("yyyy-MM-dd 15:00:00"));
                else
                    dtRet = DateTime.Parse(dtNow.ToString("yyyy-MM-dd 15:00:00"));

                if (ViewState[Constants.VS_STARTDATE] != null)
                {
                    try
                    {
                        dtRet = Convert.ToDateTime(ViewState[Constants.VS_STARTDATE]);
                    }
                    catch { }
                }
                else
                {
                    ViewState[Constants.VS_STARTDATE] = dtRet;
                }
                return dtRet;
            }
            set
            {
                ViewState[Constants.VS_STARTDATE] = value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                DateTime dtRet = DateTime.Now;

                if (dtNow.Hour < 15)
                    dtRet = DateTime.Parse(dtNow.ToString("yyyy-MM-dd 14:59:00"));
                else
                    dtRet = DateTime.Parse(dtNow.AddDays(1).ToString("yyyy-MM-dd 14:59:00"));

                if (ViewState[Constants.VS_ENDDATE] != null)
                {
                    try
                    {
                        dtRet = Convert.ToDateTime(ViewState[Constants.VS_ENDDATE]);
                    }
                    catch { }
                }
                else
                {
                    ViewState[Constants.VS_ENDDATE] = dtRet;
                }
                return dtRet;
            }
            set
            {
                ViewState[Constants.VS_ENDDATE] = value;
            }
        }
        public DateTime SearchDate
        {
            get
            {
                DateTime dtRet = DateTime.Now;

                if (Session[Constants.VS_SEARCHDATE] != null)
                {
                    try
                    {
                        dtRet = Convert.ToDateTime(Session[Constants.VS_SEARCHDATE]);
                    }
                    catch { }
                }
                else
                {
                    Session[Constants.VS_SEARCHDATE] = dtRet;
                }
                return dtRet;
            }
            set
            {
                Session[Constants.VS_SEARCHDATE] = value;
            }
        }
        #endregion

        public PageBase()
        {
        }

        #region 유저관리부분
        protected bool UserLogin(string strLoginID, string strLoginPwd)
        {
            try
            {
                string strUserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                string strUserIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(strUserIP))
                    strUserIP = Request.ServerVariables["REMOTE_ADDR"];
                string strUrl = Request.UrlReferrer.AbsolutePath;
                string strSite = Request.Url.Host;
                strSite.Replace("www.", "");

                DataSet dsSite = DBConn.RunStoreProcedure(
                    Constants.SP_GETSITE,
                    new string[]{
                        "@siteurl"
                    },
                    new object[]{
                        strSite
                });

                if (!DataSetUtil.IsNullOrEmpty(dsSite))
                    strSite = DataSetUtil.RowStringValue(dsSite, "site_name", 0);

                System.Data.DataSet dsUser = DBConn.RunStoreProcedure(
                    Constants.SP_GETUSER,
                    new string[] {
                    "@loginid"
                },
                    new object[] {
                    strLoginID
                });

                if (DataSetUtil.IsNullOrEmpty(dsUser))
                {
                    return false;
                }
                string sLoginPwd = DataSetUtil.RowValue(dsUser, "loginpwd", 0).ToString();
                int nULevel = int.Parse(DataSetUtil.RowValue(dsUser, "ulevel", 0).ToString());
                int nLoginSite = int.Parse(DataSetUtil.RowValue(dsUser, "site", 0).ToString());

                if (strLoginPwd != CryptSHA256.Decrypt(sLoginPwd) || nULevel < Constants.LEVEL_ADMIN) //SiteConfig.ManageIP.IndexOf(strUserIP) < 0 || 
                {
                    DBConn.RunStoreProcedure(Constants.SP_CREATELOGIN,
                    new string[] {
                        "@user_id",
                        "@user_ip",
                        "@site",
                        "@url",
                        "@agent",
                        "@success",
                        "@is_adminpage"
                    },
                    new object[] {
                        DataSetUtil.RowLongValue(dsUser, "id", 0),
                        strUserIP,
                        nLoginSite,
                        strUrl,
                        strUserAgent,
                        0,
                        1
                    });
                    return false;
                }

                long lID = long.Parse(DataSetUtil.RowValue(dsUser, "id", 0).ToString());
                string sLoginID = DataSetUtil.RowValue(dsUser, "loginid", 0).ToString();
                string sUserName = DataSetUtil.RowStringValue(dsUser, "nick", 0).ToString();
                string sHP = DataSetUtil.RowStringValue(dsUser, "hp", 0).ToString();

                AuthUser _authUser = new AuthUser(lID, sLoginID, sLoginPwd, sUserName, sHP, nLoginSite);
                Session[Constants.SESSION_KEY_USERINFO] = _authUser;

                if (Defines.COOKIE_INUSED)
                {
                    string strCookieData = string.Format("{0};{1};{2};{3};{4};{5};{6}",
                        _authUser.ID,
                        _authUser.LoginID,
                        _authUser.LoginPwd,
                        _authUser.NickName,
                        _authUser.HP,
                        _authUser.Site,
                        CurrentDate);

                    setCookie(Constants.COOKIE_KEY_USERINFO, CryptSHA256.Encrypt(strCookieData));
                }

                DBConn.RunStoreProcedure(Constants.SP_CREATELOGIN,
                    new string[] {
                        "@user_id",
                        "@user_ip",
                        "@site",
                        "@url",
                        "@agent",
                        "@success",
                        "@is_adminpage"
                    },
                    new object[] {
                        _authUser.ID,
                        strUserIP,
                        nLoginSite,
                        strUrl,
                        strUserAgent,
                        1,
                        1
                    });

                DBConn.RunStoreProcedure(Constants.SP_UPDATEUSERINFO,
                    new string[] {
                        "@id",
                        "@user_ip"
                    },
                    new object[] {
                        _authUser.ID,
                        strUserIP
                    });
            }
            catch(Exception ex)
            {
                string strErr = ex.Message;
                return false;
            }

            return true;
        }
        public void UserLogout()
        {
            if (DBConn != null && DBConn.IsConnected && AuthUser != null)
            {
                DBConn.RunStoreProcedure(Constants.SP_UPDATELOGIN,
                    new string[] {
                        "@user_id",
                        "@logout"
                    },
                    new object[] {
                        AuthUser.ID,
                        1
                    });
            }

            Session[Constants.SESSION_KEY_USERINFO] = null;
            Session.Remove(Constants.SESSION_KEY_USERINFO);

            if (Request.Cookies[Constants.COOKIE_KEY_SITE] != null)
            {
                HttpCookie hcCookie = Request.Cookies[Constants.COOKIE_KEY_SITE];
                hcCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(hcCookie);
            }
        }

        public bool InsertCash(long lUserID, double fCredit, double fDebit, string strDesc)
        {
            return InsertCash(lUserID, fCredit, fDebit, strDesc, "");
        }
        protected bool InsertCash(long lUserID, double fCredit, double fDebit, string strDesc, string strRel)
        {
            if (lUserID < 1)
                return false;

            if (fCredit <= 0 && fDebit <= 0)
                return false;

            if (fCredit > 0 && fDebit > 0)
                return false;

            string strLoginID = "";
            string strNick = "";
            int nSite = 0;

            DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                new string[] { "@id" }, new object[] { lUserID });
            if (!DataSetUtil.IsNullOrEmpty(dsUser))
            {
                strLoginID = DataSetUtil.RowStringValue(dsUser, "loginid", 0);
                strNick = DataSetUtil.RowStringValue(dsUser, "nick", 0);
                nSite = DataSetUtil.RowIntValue(dsUser, "site", 0);
            }

            try
            {
                DBConn.RunStoreProcedure(Constants.SP_CREATEMONEYINFO,
                    new string[] {
                        "@user_id",
                        "@loginid",
                        "@nick",
                        "@site",
                        "@credit",
                        "@debit",
                        "@description",
                        "@rel"
                    },
                        new object[] {
                        lUserID,
                        strLoginID,
                        strNick,
                        nSite,
                        fCredit,
                        fDebit,
                        strDesc,
                        strRel
                    });
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 기타유틸함수
        public string MD5(string strSrc)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(strSrc, "MD5");
        }
        public void ShowMessageBox(string strMsg)
        {
            if (string.IsNullOrEmpty(strMsg))
                return;

            ClientScript.RegisterStartupScript(GetType(),
                "MessageBox",
                "alert('" + strMsg.Replace("'", "\'") + "');",
                true);
        }
        public void ShowMessageBox(string strMsg, string strUrl)
        {
            if (string.IsNullOrEmpty(strMsg))
                return;

            ClientScript.RegisterStartupScript(GetType(),
                "MessageBox",
                "alert('" + strMsg.Replace("'", "\'") + "');" +
                "location.href='" + strUrl + "';",
                true);
        }
        public void ShowConfirm(string strMsg, string strUrl)
        {
            if (string.IsNullOrEmpty(strMsg))
                return;

            ClientScript.RegisterStartupScript(GetType(),
                "ShowConfirm",
                "if(confirm('" + strMsg.Replace("'", "\'") + "') == true) {" +
                "location.href='" + strUrl + "';" +
                "}",
                true);
        }
        public void Alert(string strMsg, string strUrl)
        {
            if (strMsg == null)
                return;

            Response.Write("<script lanuage=\"javascript\" type=\"text/javascript\">\n" +
                            "alert('" + strMsg.Replace("'", "\'") + "');\n" +
                            "location.href='" + strUrl + "';\n" +
                            "</script>");
            Response.End();
        }
        public void AlertAndClose(string strMsg)
        {
            if (string.IsNullOrEmpty(strMsg))
                return;

            Response.Write("<script lanuage=\"javascript\" type=\"text/javascript\">\n" +
                            "alert('" + strMsg.Replace("'", "\'") + "');\n" +
                            "window.opener.location.href=window.opener.location.href;\n" +
                            "window.close();\n" +
                            "</script>");
            Response.End();
        }
        public void ShowError(string strMsg)
        {
            if (strMsg != null)
                Response.Write("<h4><font color=\"red\">" + strMsg + "</font></h4>");

            Response.End();
        }
        public bool checkAuth()
        {
            if (AuthUser == null)
            {
                Alert(Resources.Err.ERR_REQUIRED_AUTH, Defines.URL_LOGIN);
                return false;
            }

            return true;
        }

        public bool checkBlockOrLeave(out string strMsg)
        {
            strMsg = "";

            // 차단 또는 삭제되었는가 검사
            if (AuthUser != null)
            {
                System.Data.DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                    new string[] { "@id" }, new object[] { AuthUser.ID });

                if (DataSetUtil.IsNullOrEmpty(dsUser))
                {
                    strMsg = Resources.Err.ERR_LOGINID_INVALID;
                    return true;
                }

                string strBlockDate = DataSetUtil.RowDateTimeValue(dsUser, "interceptdate", 0);
                if (!string.IsNullOrEmpty(strBlockDate))
                {
                    strMsg = string.Format(Resources.Err.ERR_USER_INTERCEPT, strBlockDate);
                    return true;
                }
                string strLeaveDate = DataSetUtil.RowDateTimeValue(dsUser, "leavedate", 0);
                if (!string.IsNullOrEmpty(strLeaveDate))
                {
                    strMsg = string.Format(Resources.Err.ERR_USER_LEAVE, strLeaveDate);
                    return true;
                }
            }

            return false;
        }
        public string cutString(string strValue, int iLength)
        {
            return cutString(strValue, iLength, "...");
        }
        public string cutString(string strValue, int iLength, string strFooter)
        {
            if (string.IsNullOrEmpty(strValue))
                return "";

            if (iLength < 1 || strValue.Length <= iLength)
                return strValue;

            return strValue.Substring(0, iLength) + strFooter;
        }
        public string cutHTML(string strValue, int iLength)
        {
            return cutHTML(strValue, iLength, "...");
        }
        public string cutHTML(string strValue, int iLength, string strFooter)
        {
            if (string.IsNullOrEmpty(strValue))
                return "";

            string strRet = "";
            bool bIsTag = false;
            for (int i = 0; i < strValue.Length; i++)
            {
                string strChar = strValue.Substring(i, 1);
                if (strChar == "<") bIsTag = true;
                if (!bIsTag) strRet += strChar;
                if (strRet.Length >= iLength)
                {
                    strRet += strFooter;
                    break;
                }
                if (strChar == ">") bIsTag = false;
            }
            return strRet;
        }
        public int null2Zero(object objValue)
        {
            try
            {
                return int.Parse(objValue.ToString());
            }
            catch
            {
            }

            return 0;
        }
        public string text2Html(string strValue)
        {
            return strValue.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }
        public string html2Text(string strValue)
        {
            return strValue.Replace("<br />", "\r\n");
        }
        protected string getCookie(string strKey)
        {
            HttpCookie hcCookie = Request.Cookies[Constants.COOKIE_KEY_SITE];
            if (hcCookie == null || hcCookie[strKey] == null)
                return null;

            return hcCookie[strKey];
        }
        protected void setCookie(string strKey, string strValue)
        {
            HttpCookie hc = null;

            if (Request.Cookies[Constants.COOKIE_KEY_SITE] != null)
                hc = Request.Cookies[Constants.COOKIE_KEY_SITE];
            else
                hc = new HttpCookie(Constants.COOKIE_KEY_SITE);

            hc.Values.Add(strKey, strValue);

            hc.Expires = DateTime.Now.AddHours(Defines.COOKIE_TIMEOUT);

            Response.Cookies.Add(hc);
        }
        protected virtual void visibleEmptyRow(object sender, RepeaterItemEventArgs e)
        {
            Repeater rpCtrl = sender as Repeater;
            if (rpCtrl == null)
                return;

            if (rpCtrl.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("rowEmpty");
                    if (tr != null)
                        tr.Visible = true;
                }
            }
        }
        public void setLiteralValue(GridViewRow gvRow, string strID, string strValue)
        {
            Literal ltlTarget = (Literal)gvRow.FindControl(strID);
            if (ltlTarget != null)
                ltlTarget.Text = strValue;
        }
        public void setItemValue(RepeaterItem rpItem, string strID, string strValue)
        {
            Literal ltlTarget = (Literal)rpItem.FindControl(strID);
            if (ltlTarget != null)
                ltlTarget.Text = strValue;
        }
        protected bool checkImageFile(HttpPostedFile srcFile)
        {
            if (srcFile == null || srcFile.ContentLength == 0)
                return false;

            string[] arrValidExt =
                new string[] { 
                    "gif",
                    "jpg",
                    "png"
                };

            string strExt = srcFile.FileName.Substring(srcFile.FileName.LastIndexOf(".") + 1);

            for (int i = 0; i < arrValidExt.Length; i++)
            {
                if (strExt.ToLower() == arrValidExt[i])
                    return true;
            }

            return false;
        }
        protected string uploadFile(HttpPostedFile srcFile, string strWebPath, string strFileName)
        {
            if (srcFile == null || srcFile.ContentLength == 0)
                return null;

            try
            {
                string strPath = Server.MapPath(strWebPath);
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                string strTargetPath = strPath + "\\" + strFileName;
                if (File.Exists(strTargetPath))
                    File.Delete(strTargetPath);

                srcFile.SaveAs(strTargetPath);
            }
            catch
            {
                return null;
            }

            return strWebPath + "/" + strFileName;
        }
        protected string uploadFile(HttpPostedFile srcFile, string strWebPath)
        {
            if (srcFile == null || srcFile.ContentLength == 0)
                return null;

            string strFileName = srcFile.FileName;
            return uploadFile(srcFile, strWebPath, strFileName.Substring(strFileName.LastIndexOf("\\") + 1));
        }
        protected string copyFile(string strFullPath, string strWebPath, string strFileName)
        {
            if (string.IsNullOrEmpty(strFullPath))
                return null;

            try
            {
                string strPath = Server.MapPath(strWebPath);
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                string strTargetPath = strPath + "\\" + strFileName;

                FileStream fs = File.Create(strTargetPath);
                FileStream fsSrc = File.Open(strFullPath, FileMode.Open, FileAccess.Read);
                byte[] buff = new byte[fsSrc.Length];
                fsSrc.Read(buff, 0, buff.Length);
                fs.Write(buff, 0, buff.Length);
                fsSrc.Close();
                fs.Close();
                //File.Copy(strFullPath, strTargetPath, true);
            }
            catch
            {
                return null;
            }

            return strWebPath + "/" + strFileName;
        }
        protected string copyFile1(string strFullPath, string strTarget)
        {
            if (string.IsNullOrEmpty(strFullPath))
                return null;

            try
            {
                string strTargetPath = strTarget;

                FileStream fs = File.Create(strTargetPath);
                FileStream fsSrc = File.Open(strFullPath, FileMode.Open, FileAccess.Read);
                byte[] buff = new byte[fsSrc.Length];
                fsSrc.Read(buff, 0, buff.Length);
                fs.Write(buff, 0, buff.Length);
                fsSrc.Close();
                fs.Close();
                //File.Copy(strFullPath, strTargetPath, true);
            }
            catch
            {
                return null;
            }

            return strTarget;
        }
        protected void deleteFile(string strWebPath)
        {
            if (string.IsNullOrEmpty(strWebPath))
                return;

            try
            {
                string strPath = Server.MapPath(strWebPath);
                if (File.Exists(strPath))
                    File.Delete(strPath);
            }
            catch { }
        }
        
        protected string getChkSum(long lGameID, int iBetType, int iTarget, string strIP, long lBetMoney, double fBetRatio)
        {
            return CryptSHA256.Encrypt(string.Format("{0};{1};{2};{3};{4};{5}",
                            lGameID,
                            iBetType,
                            iTarget,
                            strIP,
                            lBetMoney,
                            fBetRatio));
        }
        public void readSiteConfig()
        {
            // 사이트 설정정보 조회
            System.Data.DataSet dsConfig = DBConn.RunStoreProcedure(Constants.SP_GETCONFIG, new string[] { "@type" }, new object[] { Constants.GAMETYPE_NORMAL });
            if (DataSetUtil.IsNullOrEmpty(dsConfig))
                throw new Exception();

            Dictionary<string, string> dicConfigs = new Dictionary<string, string>();
            for (int i = 0; i < DataSetUtil.RowCount(dsConfig); i++)
                dicConfigs.Add(DataSetUtil.RowStringValue(dsConfig, "conf_name", i), DataSetUtil.RowStringValue(dsConfig, "conf_value", i));

            _config = new Config();
            _config.initConfig(
                dicConfigs["title"],
                Convert.ToInt32(dicConfigs["member_join"]),
                Convert.ToInt32(dicConfigs["page_rows"]),
                Convert.ToInt32(dicConfigs["login_minutes"]),
                dicConfigs["intercept_ip"],
                dicConfigs["manage_ip"],
                dicConfigs["prohibit_id"]);
        }

        public string getMoneyInOutStatus(object iStatus)
        {
            int iTemp = (iStatus == null) ? 0 : int.Parse(iStatus.ToString());
            string strRet = "UnKnown";
            switch (iTemp)
            {
                case Constants.MONEYINOUT_STATUS_REQUEST:
                    strRet = "<font color='red'>" + Resources.Str.STR_REQUEST + "</font>";
                    break;
                case Constants.MONEYINOUT_STATUS_APPLY:
                    strRet = "<font color='green'>" + Resources.Str.STR_COMPLETE + "</font>";
                    break;
                case Constants.MONEYINOUT_STATUS_STANDBY:
                    strRet = "<font color='blue'>" + Resources.Str.STR_STANDBY + "</font>";
                    break;
                case Constants.MONEYINOUT_STATUS_CANCEL:
                    strRet = Resources.Str.STR_CANCEL;
                    break;
            }

            return strRet;
        }

        public string getMoneyInOutDelStatus(object delDate, object delType)
        {
            bool bDel = (delDate == DBNull.Value) ? false : true;
            int iDelType = (delType == null) ? -1 : int.Parse(delType.ToString());
            string strRet = "UnKnown";
            if (!bDel)
            {
                strRet = "<font color='blue'>No</font>";
            }
            else
            {
                if (iDelType == Constants.BETDELTYPE_ADMIN)
                    strRet = "<font color='green'>" + Resources.Str.STR_ADMIN + "</font>";
                else
                    strRet = "<font color='red'>" + Resources.Str.STR_USER + "</font>";
            }

            return strRet;
        }
        public string getBetMode(int nBetMode)
        {
            string strBetMode = "";

            switch (nBetMode)
            {
                case 8010101:
                    strBetMode = "猜冠军";
                    break;
                case 8020101:
                    strBetMode = "猜冠亚军";
                    break;
                case 8020201:
                    strBetMode = "猜冠亚军单式";
                    break;
                case 8030101:
                    strBetMode = "猜前三名";
                    break;
                case 8030201:
                    strBetMode = "猜前三名单式";
                    break;
                case 8040101:
                    strBetMode = "猜前四名";
                    break;
                case 8040201:
                    strBetMode = "猜前四名单式";
                    break;
                case 8050101:
                    strBetMode = "猜前五名";
                    break;
                case 8050201:
                    strBetMode = "猜前五名单式";
                    break;
                case 8060101:
                    strBetMode = "猜前六名";
                    break;
                case 8070101:
                    strBetMode = "猜前七名";
                    break;
                case 8080101:
                    strBetMode = "猜前八名";
                    break;
                case 8090101:
                    strBetMode = "猜前九名";
                    break;
                case 8100101:
                    strBetMode = "猜前十名";
                    break;
                case 8140101:
                    strBetMode = "前五定位胆";
                    break;
                case 8140102:
                    strBetMode = "后五定位胆";
                    break;
                case 8120101:
                    strBetMode = "自由双面";
                    break;
                case 8130101:
                    strBetMode = "1至3";
                    break;
                case 8130102:
                    strBetMode = "4至6";
                    break;
                case 8130103:
                    strBetMode = "7至10";
                    break;
                case 8130104:
                    strBetMode = "1至5";
                    break;
                case 8130105:
                    strBetMode = "6至10";
                    break;
                case 8150101:
                    strBetMode = "猜前四名";
                    break;
                case 8160101:
                    strBetMode = "猜前五名";
                    break;
                case 25010101:
                case 25010102:
                    strBetMode = "单式 - 左右";
                    break;
                case 25010103:
                case 25010104:
                    strBetMode = "单式 - 34";
                    break;
                case 25010105:
                case 25010106:
                    strBetMode = "单式 - 单双";
                    break;
                case 25020101:
                case 25020102:
                case 25020103:
                case 25020104:
                    strBetMode = "双式";
                    break;
                default:
                    break;
            }
            if(nBetMode > 8110100 && nBetMode < 8110130)
                strBetMode = "前三和值";

            return strBetMode;
        }
        public bool getBetResult(int Rank1, int Rank2, int Rank3, int Rank4, int Rank5, int Rank6, int Rank7, int Rank8, int Rank9, int Rank10, int nBetMode, string strBetVal, double fBetMoney, double fRatio, out int nBetResult, out double fWinMoney)
        {
            nBetResult = 0;
            fWinMoney = 0.0f;

            int nBetCount = 0;
            int nWinCount = 0;
            switch (nBetMode)
            {
                case 8010101:
                    string[] strPrams = strBetVal.Split(' ');
                    for (int i = 0; i < strPrams.Length; i++)
                    {
                        nBetCount++;
                        if (Convert.ToInt32(strPrams[i]) == Rank1)
                            nWinCount++;
                    }
                    break;
                case 8020101:
                    string[] strParams2 = strBetVal.Split(',');
                    string[] strParam2_1 = strParams2[0].Split(' ');
                    string[] strParam2_2 = strParams2[1].Split(' ');
                    for (int i = 0; i < strParam2_1.Length; i++)
                    {
                        int Pos_1 = Convert.ToInt32(strParam2_1[i]);
                        for (int j = 0; j < strParam2_2.Length; j++)
                        {
                            int Pos_2 = Convert.ToInt32(strParam2_2[j]);
                            if (Pos_1 != Pos_2)
                                nBetCount++;
                            if (Pos_1 == Rank1 && Pos_2 == Rank2)
                                nWinCount++;
                        }
                    }
                    break;
                case 8030101:
                    string[] strParams3 = strBetVal.Split(',');
                    string[] strParam3_1 = strParams3[0].Split(' ');
                    string[] strParam3_2 = strParams3[1].Split(' ');
                    string[] strParam3_3 = strParams3[2].Split(' ');
                    for (int i = 0; i < strParam3_1.Length; i++)
                    {
                        int Pos_1 = Convert.ToInt32(strParam3_1[i]);
                        for (int j = 0; j < strParam3_2.Length; j++)
                        {
                            int Pos_2 = Convert.ToInt32(strParam3_2[j]);
                            if (Pos_1 == Pos_2)
                                continue;
                            for (int k = 0; k < strParam3_3.Length; k++)
                            {
                                int Pos_3 = Convert.ToInt32(strParam3_3[k]);
                                if (Pos_1 == Pos_3 || Pos_2 == Pos_3)
                                    continue;

                                nBetCount++;
                                if (Pos_1 == Rank1 && Pos_2 == Rank2 && Pos_3 == Rank3)
                                    nWinCount++;
                            }

                        }
                    }
                    break;
                case 8040101:
                    string[] strParams4 = strBetVal.Split(',');
                    string[] strParam4_1 = strParams4[0].Split(' ');
                    string[] strParam4_2 = strParams4[1].Split(' ');
                    string[] strParam4_3 = strParams4[2].Split(' ');
                    string[] strParam4_4 = strParams4[3].Split(' ');
                    for (int i = 0; i < strParam4_1.Length; i++)
                    {
                        int Pos_1 = Convert.ToInt32(strParam4_1[i]);
                        for (int j = 0; j < strParam4_2.Length; j++)
                        {
                            int Pos_2 = Convert.ToInt32(strParam4_2[j]);
                            if (Pos_1 == Pos_2)
                                continue;
                            for (int k = 0; k < strParam4_3.Length; k++)
                            {
                                int Pos_3 = Convert.ToInt32(strParam4_3[k]);
                                if (Pos_1 == Pos_3 || Pos_2 == Pos_3)
                                    continue;
                                for (int p = 0; p < strParam4_4.Length; p++)
                                {
                                    int Pos_4 = Convert.ToInt32(strParam4_4[p]);
                                    if (Pos_1 == Pos_4 || Pos_2 == Pos_4 || Pos_3 == Pos_4)
                                        continue;

                                    nBetCount++;
                                    if (Pos_1 == Rank1 && Pos_2 == Rank2 && Pos_3 == Rank3 && Pos_4 == Rank4)
                                        nWinCount++;
                                }
                            }

                        }
                    }
                    break;
                case 8050101:
                    string[] strParams5 = strBetVal.Split(',');
                    string[] strParam5_1 = strParams5[0].Split(' ');
                    string[] strParam5_2 = strParams5[1].Split(' ');
                    string[] strParam5_3 = strParams5[2].Split(' ');
                    string[] strParam5_4 = strParams5[3].Split(' ');
                    string[] strParam5_5 = strParams5[4].Split(' ');
                    for (int i = 0; i < strParam5_1.Length; i++)
                    {
                        int Pos_1 = Convert.ToInt32(strParam5_1[i]);
                        for (int j = 0; j < strParam5_2.Length; j++)
                        {
                            int Pos_2 = Convert.ToInt32(strParam5_2[j]);
                            if (Pos_1 == Pos_2)
                                continue;
                            for (int k = 0; k < strParam5_3.Length; k++)
                            {
                                int Pos_3 = Convert.ToInt32(strParam5_3[k]);
                                if (Pos_1 == Pos_3 || Pos_2 == Pos_3)
                                    continue;
                                for (int p = 0; p < strParam5_4.Length; p++)
                                {
                                    int Pos_4 = Convert.ToInt32(strParam5_4[p]);
                                    if (Pos_1 == Pos_4 || Pos_2 == Pos_4 || Pos_3 == Pos_4)
                                        continue;

                                    for (int q = 0; q < strParam5_5.Length; q++)
                                    {
                                        int Pos_5 = Convert.ToInt32(strParam5_5[p]);
                                        if (Pos_1 == Pos_5 || Pos_2 == Pos_5 || Pos_3 == Pos_5 || Pos_4 == Pos_5)
                                            continue;

                                        nBetCount++;
                                        if (Pos_1 == Rank1 && Pos_2 == Rank2 && Pos_3 == Rank3 && Pos_4 == Rank4 && Pos_5 == Rank5)
                                            nWinCount++;
                                    }
                                }
                            }

                        }
                    }
                    break;
                case 8140101:
                    string[] strParams6 = strBetVal.Split(',');
                    for (int i = 0; i < 5; i++)
                    {
                        if (string.IsNullOrEmpty(strParams6[i]))
                            continue;

                        string[] strParams6_1 = strParams6[i].Split(' ');
                        for (int j = 0; j < strParams6_1.Length; j++)
                        {
                            nBetCount++;
                            int Pos = Convert.ToInt32(strParams6_1[j]);
                            if ((i == 0 && Pos == Rank1) || (i == 1 && Pos == Rank2) || (i == 2 && Pos == Rank3) || (i == 3 && Pos == Rank4) || (i == 4 && Pos == Rank5))
                                nWinCount++;
                        }

                    }
                    break;
                case 8140102:
                    string[] strParams7 = strBetVal.Split(',');
                    for (int i = 0; i < 5; i++)
                    {
                        if (string.IsNullOrEmpty(strParams7[i]))
                            continue;

                        string[] strParams7_1 = strParams7[i].Split(' ');
                        for (int j = 0; j < strParams7_1.Length; j++)
                        {
                            nBetCount++;
                            int Pos = Convert.ToInt32(strParams7_1[j]);
                            if ((i == 0 && Pos == Rank6) || (i == 1 && Pos == Rank7) || (i == 2 && Pos == Rank8) || (i == 3 && Pos == Rank9) || (i == 4 && Pos == Rank10))
                                nWinCount++;
                        }

                    }
                    break;
                case 8120101:
                    string[] strParams12 = strBetVal.Split(',');
                    for (int i = 0; i < 3; i++)
                    {
                        if (string.IsNullOrEmpty(strParams12[i]))
                            continue;
                        string[] strParams12_1 = strParams12[i].Split(' ');
                        int Rank = (i == 0) ? Rank1 : (i == 1) ? Rank2 : Rank3;
                        for (int j = 0; j < strParams12_1.Length; j++)
                        {
                            nBetCount++;
                            string Pos = strParams12_1[j];
                            if ((Pos == "大" && Rank > 5) || (Pos == "小" && Rank < 6) || (Pos == "双" && Rank % 2 == 0) || (Pos == "单" && Rank % 2 == 1))
                                nWinCount++;
                        }
                    }
                    break;
                case 8130101:
                    string[] strParams13_1 = strBetVal.Split(' ');
                    for (int i = 0; i < strParams13_1.Length; i++)
                    {
                        nBetCount++;
                        int nVal = Convert.ToInt32(strParams13_1[i]);
                        if (nVal == Rank1 || nVal == Rank2 || nVal == Rank3)
                            nWinCount++;
                    }
                    break;
                case 8130102:
                    string[] strParams13_2 = strBetVal.Split(' ');
                    for (int i = 0; i < strParams13_2.Length; i++)
                    {
                        nBetCount++;
                        int nVal = Convert.ToInt32(strParams13_2[i]);
                        if (nVal == Rank4 || nVal == Rank5 || nVal == Rank6)
                            nWinCount++;
                    }
                    break;
                case 8130103:
                    string[] strParams13_3 = strBetVal.Split(' ');
                    for (int i = 0; i < strParams13_3.Length; i++)
                    {
                        nBetCount++;
                        int nVal = Convert.ToInt32(strParams13_3[i]);
                        if (nVal == Rank7 || nVal == Rank8 || nVal == Rank9 || nVal == Rank10)
                            nWinCount++;
                    }
                    break;
                case 8130104:
                    string[] strParams13_4 = strBetVal.Split(' ');
                    for (int i = 0; i < strParams13_4.Length; i++)
                    {
                        nBetCount++;
                        int nVal = Convert.ToInt32(strParams13_4[i]);
                        if (nVal == Rank1 || nVal == Rank2 || nVal == Rank3 || nVal == Rank4 || nVal == Rank5)
                            nWinCount++;
                    }
                    break;
                case 8130105:
                    string[] strParams13_5 = strBetVal.Split(' ');
                    for (int i = 0; i < strParams13_5.Length; i++)
                    {
                        nBetCount++;
                        int nVal = Convert.ToInt32(strParams13_5[i]);
                        if (nVal == Rank6 || nVal == Rank7 || nVal == Rank8 || nVal == Rank9 || nVal == Rank10)
                            nWinCount++;
                    }
                    break;
                default:
                    break;
            }
            if (nBetMode > 8110100 && nBetMode < 8110130)
            {
                nBetCount = 1;
                int nSum = Rank1 + Rank2 + Rank3;
                if ((nBetMode == 8110101 && nSum > 16) || (nBetMode == 8110102 && nSum < 17) || (nBetMode == 8110103 && nSum % 2 == 1) || (nBetMode == 8110102 && nSum % 2 == 0))
                    nWinCount = 1;
                else if ((nSum + 8110100) == (nBetMode + 1))
                    nWinCount = 1;
            }
            if (nBetCount <= 0)
                return false;

            if (nWinCount > 0)
                nBetResult = 2;
            else
                nBetResult = 1;
            fWinMoney = fBetMoney * fRatio * nWinCount / nBetCount;
            return true;
        }

        public string getLadderGameResult(int nGameMode, int nValue)
        {
            string strReturn = "";

            switch (nGameMode)
            {
                case 1:
                    strReturn = (nValue == 1) ? "<font color='blue'>左</font>" : (nValue == 2 ? "<font color='red'>右</font>" : "?");
                    break;
                case 2:
                    strReturn = (nValue == 1) ? "<font color='blue'>3</font>" : (nValue == 2 ? "<font color='red'>4</font>" : "?");
                    break;
                case 3:
                    strReturn = (nValue == 1) ? "<font color='blue'>单</font>" : (nValue == 2 ? "<font color='red'>双</font>" : "?");
                    break;
                default:
                    strReturn = "?";
                    break;
            }
            return strReturn;
        }
        #endregion

        #region 사건처리부
        protected virtual void Page_PreInit(object sender, EventArgs e)
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
            _userip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(_userip))
                _userip = Request.ServerVariables["REMOTE_ADDR"];

            Session.Timeout = Defines.SESSION_TIMEOUT;

            try
            {
                readSiteConfig();

                // 로그인한 유저의 개인정보 갱신
                if (AuthUser != null)
                {
                    System.Data.DataSet dsUser = DBConn.RunStoreProcedure(Constants.SP_GETUSER,
                        new string[] { "@id" }, new object[] { AuthUser.ID });

                    if (DataSetUtil.IsNullOrEmpty(dsUser))
                    {
                        Alert(Resources.Err.ERR_REQUIRED_AUTH, Defines.URL_LOGOUT);
                        return;
                    }

                    AuthUser.NickName = DataSetUtil.RowStringValue(dsUser, "nick", 0);
                }/*
                if (SiteConfig.ManageIP.IndexOf(_userip) < 0)
                {
                    string strUserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                    string strUserIP = Request.ServerVariables["REMOTE_ADDR"];
                    string strUrl = Request.UrlReferrer.AbsolutePath;
                    
                    DBConn.RunStoreProcedure(Constants.SP_CREATELOGIN,
                    new string[] {
                        "@user_id",
                        "@user_ip",
                        "@site",
                        "@url",
                        "@agent",
                        "@success",
                        "@is_adminpage"
                    },
                    new object[] {
                        AuthUser.LoginID,
                        strUserIP,
                        AuthUser.Site,
                        strUrl,
                        strUserAgent,
                        0,
                        1
                    });
                    Alert("접속할수 없는 IP주소입니다.", Defines.URL_LOGOUT);
                    return;

                }*/
                // 방문 URL 저장
                if (AuthUser != null)
                {
                    DBConn.RunStoreProcedure(Constants.SP_UPDATELOGIN,
                        new string[] {
                            "@user_id",
                            "@url",
                            "@logout"
                        },
                            new object[] {
                            AuthUser.ID,
                            Request.Url.ToString(),
                            0
                        });
                }
            }
            catch(Exception ex)
            {
                string strErr = ex.Message;
                _isInited = false;
                return;
            }
            _isInited = true;
            // 페이지 설정 끝
        }
        
        protected virtual void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsInited)
            {
                ShowError(Resources.Err.ERR_CONFIG_INVALID);
            }
        }

        protected virtual void Page_Init(object sender, EventArgs e)
        {
            PageDataSource = null;

            checkAuth();
            ////////////////////////////////////////////////////////////////////////////
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            string strMsg = "";
            if (checkBlockOrLeave(out strMsg))
            {
                UserLogout();
                Alert(strMsg, Defines.URL_LOGIN);
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Params["page"]))
                    PageNumber = Convert.ToInt32(Request.Params["page"]);

                InitControls();

                BindData();
            }
            //else
            //    LoadData();
        }

        protected virtual void Page_PreRender(object sender, EventArgs e)
        {
            MasterPageBase master = Master as MasterPageBase;
            if (master != null)
                master.UpdateUserInfo();
        }

        protected virtual void LoadData()
        {
        }
        protected virtual void BindData()
        {
            if (PageDataSource == null)
                LoadData();

            if (getGridControl() != null)
            {
                DataView dv = null;
                if (PageDataSource != null && PageDataSource.Tables.Count > 0)
                    dv = PageDataSource.Tables[0].DefaultView;

                if (DataSetUtil.IsNullOrEmpty(PageDataSource))
                    PageNumber = 0;
                else
                {
                    int iPageCount = PageDataSource.Tables[0].Rows.Count / getGridControl().PageSize;
                    if (iPageCount < PageNumber)
                    {
                        PageNumber = iPageCount;
                    }

                    if (!string.IsNullOrEmpty(SortColumn))
                    {
                        string strSort = (SortDirection == SortDirection.Ascending) ? SortColumn : SortColumn + " DESC";
                        dv.Sort = strSort;
                    }
                }

                if (dv != null)
                {
                    getGridControl().PageIndex = PageNumber;
                    getGridControl().DataSource = dv;
                    getGridControl().DataBind();
                }
            }
        }

        protected virtual void InitControls()
        {
            if (getGridControl() != null)
                getGridControl().PageSize = SiteConfig.PageRows;
        }
        protected virtual GridView getGridControl()
        {
            return null;
        }

        protected virtual void gvContent_PageIndexChange(object sender, GridViewPageEventArgs e)
        {
            PageNumber = e.NewPageIndex;
            BindData();
        }
        protected virtual void gvContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected virtual void gvContent_Sorting(object sender, GridViewSortEventArgs e)
        {
            // 요청되는 정돈항이 이전과 같으면 정돈방향을 바꾼다
            if (e.SortExpression == SortColumn)
            {
                SortDirection = (SortDirection == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                // 정돈항에 새 정돈항을 넣어주고 정돈방향은 Descending 으로 설정한다.
                SortColumn = e.SortExpression;
                SortDirection = SortDirection.Descending;
            }

            BindData();
        }
        #endregion

        protected virtual void Page_Unload(object sender, EventArgs e)
        {
            // 디비 연결 닫기
            if (_dbconn != null)
            {
                _dbconn.Disconnect();
                _dbconn = null;
            }
        }

        public void writeLog(string strMsg)
        {
            try
            {
                string strLogDir = Server.MapPath("/logs");
                if (!Directory.Exists(strLogDir))
                    Directory.CreateDirectory(strLogDir);

                StreamWriter sWriter = new StreamWriter(strLogDir + "\\error.log", true);
                sWriter.WriteLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] --> [{1}] : {2}",
                    DateTime.Now, Request.ServerVariables["REMOTE_ADDR"], strMsg));
                sWriter.Close();
            }
            catch
            {

            }
        }
    }
}

