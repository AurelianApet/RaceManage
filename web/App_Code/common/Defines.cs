using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for Defines
/// </summary>

namespace Ronaldo.common
{
    public class Defines
    {
        public Defines()
        {
        }

        #region 페이지 관련 정보
        public static string URL_LOGIN
        {
            get { return ConfigurationManager.AppSettings["URL_LOGIN"]; }
        }
        public static string URL_LOGOUT
        {
            get { return ConfigurationManager.AppSettings["URL_LOGOUT"]; }
        }
        public static string URL_DEFAULT
        {
            get { return ConfigurationManager.AppSettings["URL_DEFAULT"]; }
        }
        public static string URL_PREFIX_IMG
        {
            get { return ConfigurationManager.AppSettings["URL_PREFIX_IMG"]; }
        }
        #endregion

        #region 쿠키 관련 정보
        public static bool COOKIE_INUSED
        {
            get { return (ConfigurationManager.AppSettings["COOKIE_INUSED"].ToLower() == "true"); }
        }
        public static int COOKIE_TIMEOUT
        {
            get
            {
                int iTimeOut = 0;
                return (int.TryParse(ConfigurationManager.AppSettings["COOKIE_TIMEOUT"], out iTimeOut) && iTimeOut > 0 ? iTimeOut : 24);
            }
        }
        #endregion

        #region 세션 관련 정보
        public static int SESSION_TIMEOUT
        {
            get
            {
                int iTimeOut = 0;
                return (int.TryParse(ConfigurationManager.AppSettings["SESSION_TIMEOUT"], out iTimeOut) && iTimeOut > 0 ? iTimeOut : 30);
            }
        }
        #endregion
    }
}
