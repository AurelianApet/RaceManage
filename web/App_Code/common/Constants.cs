using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Summary description for Constants
/// </summary>
/// 
namespace Ronaldo.common
{
    public class Constants
    {
        #region 쿠키 관련 상수
        public const string COOKIE_KEY_SITE     = "RACEMANAGE::COOKIE::KEY";
        public const string COOKIE_KEY_USERINFO = "RACEMANAGE::COOKIE::KEY::USERINFO";
        #endregion

        #region 세션 관련 상수
        public const string SESSION_KEY_USERINFO    = "RACEMANAGE::SESSION::KEY::USERINFO";
        public const string SESSION_KEY_AUTOLOGOUT  = "RACEMANAGE::SESSION::KEY::AUTOLOGOUT";
        #endregion

        #region 디비관련상수
        //Member디비관련 상수

        public const string DB_CONN_HOST    = "192.168.192.1"; //192.168.30.1
        public const string DB_CONN_PORT    = "1433"; //56777
        public const string DB_CONN_NAME    = "RaceGame";
        public const string DB_CONN_USER    = "cargame";//"sa_ace";
        public const string DB_CONN_PASS    = "qweASDzxc!@#456";//"sa_ace123";
        public const string DB_CONN_BACKUP  = "D:\\DBBackup\\";
        /*/
        public const string DB_CONN_HOST    = "127.0.0.1";
        public const string DB_CONN_PORT    = "1433";
        public const string DB_CONN_NAME    = "RaceGame";
        public const string DB_CONN_USER    = "sa";
        public const string DB_CONN_PASS    = "sa";
        public const string DB_CONN_BACKUP  = "D:\\DBBackup\\";
        /**/
        #endregion

        #region 레벨상수
        public const int LEVEL_ADMIN = 99;
        public const int LEVEL_DIST = 80;
        public const int LEVEL_USER = 1;
        #endregion

        #region 충환전 상수
        public const int MONEYINOUT_MODE_CHARGE = 0;
        public const int MONEYINOUT_MODE_DISCHARGE = 1;

        public const int MONEYINOUT_STATUS_REQUEST = 0;
        public const int MONEYINOUT_STATUS_APPLY = 1;
        public const int MONEYINOUT_STATUS_CANCEL = 2;
        public const int MONEYINOUT_STATUS_STANDBY = 3;
        #endregion

        #region 뷰스테이트 관련 상수
        public const string VS_PAGENUMBER = "PageNumber";
        public const string VS_DATASOURCE = "DataSource";

        public const string VS_STARTDATE = "StartDate";
        public const string VS_ENDDATE = "EndDate";

        public const string VS_SORTCOLUMN = "SortColumn";
        public const string VS_SORTDIRECTION = "SortDirection";

        public const string VS_SEARCHDATE = "SearchDate";
        #endregion

        #region Stored Procedure 이름 정의
        public static string SP_GETDBINFO           = "sp_getDBInfo";
        public static string SP_GETUNREADREQUEST    = "sp_getUnreadRequest";
        public static string SP_GETCONFIG       = "sp_getConfig";
        public static string SP_UPDATECONFIG    = "sp_updateConfig";
        public static string SP_UPDATECONFIGRATIO = "sp_updateConfigRatio";

        public static string SP_GETUSER         = "sp_getUser";
        public static string SP_GETUSERLIST     = "sp_getUserList";
        public static string SP_UPDATEUSER      = "sp_updateUser";
        public static string SP_UPDATEUSERINFO  = "sp_updateUserInfo";

        public static string SP_CREATEMONEYINFO = "sp_createMoneyInfo";
        public static string SP_DELETEMONEYINFO = "sp_deleteMoneyInfo";
        public static string SP_GETMONEYINFO    = "sp_getMoneyInfo";
        public static string SP_GETMONEYINOUTS  = "sp_getMoneyInOuts";
        public static string SP_GETMONEYINOUTINFO = "sp_getMoneyInOutInfo";
        public static string SP_UPDATEMONEYINOUTS = "sp_updateMoneyInouts";
        public static string SP_GETSALEDATA     = "sp_getSaleData";
        public static string SP_GETSALEDATA1    = "sp_getSaleData1";

        public static string SP_CREATESITE      = "sp_createSite";
        public static string SP_DELETESITE      = "sp_deleteSite";
        public static string SP_GETSITE         = "sp_getSite";
        public static string SP_GETSITELIST     = "sp_getSiteList";
        public static string SP_UPDATESITE      = "sp_updateSite";

        public static string SP_CREATELOGIN     = "sp_createLogin";
        public static string SP_DELETELOGIN     = "sp_deleteLogin";
        public static string SP_GETLOGINS       = "sp_getLogins";
        public static string SP_GETLOGINHISTS   = "sp_getLoginHists";
        public static string SP_UPDATELOGIN     = "sp_updateLogin";

        public static string SP_CREATENOTICE    = "sp_createNotice";
        public static string SP_DELETENOTICE    = "sp_deleteNotice";
        public static string SP_GETNOTICELIST   = "sp_getNoticeList";
        public static string SP_UPDATENOTICE    = "sp_updateNotice";
        public static string SP_GETNOTICEINFO   = "sp_getNoticeInfo";

        public static string SP_GETCURRENTGAME  = "sp_getCurrentGame";
        public static string SP_GETGAMEHIST     = "sp_getGameHist";
        public static string SP_GETGAMELIST     = "sp_getGameList";
        public static string SP_GETGAMES        = "sp_getGames";
        public static string SP_UPDATEGAMES     = "sp_updateGames";

        public static string SP_GETBETTINGHIST  = "sp_getBettingHist";
        public static string SP_UPDATEBETTINGHIST = "sp_updateBettingHist";
        #endregion

        #region 지급상태 상수
        public const int CALC_NOPAY = 0;        // 미지급
        public const int CALC_PAYED = 1;        // 지급
        #endregion

        #region 정렬상수
        public const int SORTTYPE_DESC = 0;
        public const int SORTTYPE_ASC = 1;
        #endregion

        #region 게시글 종류
        public const int NOTICEKIND_NOTICE = 0;        // 공지사항
        public const int NOTICEKIND_MESSAGE = 4;        // 쪽지
        #endregion

        #region 쪽지류형상수
        public const int MSGTYPE_NON = 0;           //미정
        public const int MSGTYPE_SYSTEM = 1;        //체계
        public const int MSGTYPE_CHARGE = 2;        //충전
        public const int MSGTYPE_DISCHARGE = 3;     //환전
        #endregion

        #region 게임류형상수
        public const int GAMETYPE_NORMAL = 0;   //일반적으로 게임 전체를 나타낸다.
        public const int GAMETYPE_RACE = 8;     //경주경기
        public const int GAMETYPE_LADDER = 25;   //사다리경기
        #endregion

        #region 게임 정산 상수
        public const int GAME_NOCALC = 0;       // 미정상
        public const int GAME_CALCED = 1;       // 정산완료
        public const int GAME_VALID = 2;        // 무효
        #endregion

        #region 배팅 결과 상수
        public const int BETSTATE_READY = 0;    // 미정상태
        public const int BETTSTAT_LOSE = 1;     // 미적중
        public const int BETSTAT_WIN = 2;       // 적중
        public const int BETSTAT_VALID = 3;     // 무효
        #endregion

        #region 회원류형 상수
        public const int USERSTATUS_ALL = 0;        //전체
        public const int USERSTATUS_COMMON =1;     //일반
        public const int USERSTATUS_NEW = 2;        //정상
        public const int USERSTATUS_INTERCEPT = 3;  //차단
        public const int USERSTATUS_LEAVE = 4;      //탈퇴
        #endregion

        #region 배팅삭제형태
        public const int BETDELTYPE_ADMIN = 1;
        public const int BETDELTYPE_USER = 0;
        #endregion
    }
}
