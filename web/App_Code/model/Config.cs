using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Config
/// </summary>

namespace Ronaldo.model
{
    public class Config
    {
        #region 설정값속성
        // 사이트 제목
        private string _title = null;
        public string Title
        {
            get { return _title; }
        }

        // 회원가입 여부
        private int _member_join = 0;
        public bool MemberJoin
        {
            get
            {
                return (_member_join == 0) ? false : true;
            }
        }

        // 한 페이지에 표시할 행수
        private int _page_rows = 0;
        public int PageRows
        {
            get { return (_page_rows > 0) ? _page_rows : 15; }
        }

        // 접속자로 판단하는 시간, 단위는 분
        // 이 시간이 지나면 접속자목록에서 삭제
        private int _login_minutes = 0;
        public int LoginMinutes
        {
            get { return _login_minutes; }
        }

        // 접속차단아이피목록, \n으로 구분
        private string _intercept_ip = null;
        public string InterceptIP
        {
            get { return _intercept_ip; }
        }

        // 관리자 아이피 목록, \n으로 구분
        private string _manage_ip = null;
        public string ManageIP
        {
            get { return _manage_ip; }
        }

        // 아이디, 닉네임으로 이용할수 없는 단어목록: ,로 구분
        private string _prohibit_id = null;
        public string ProhibitID
        {
            get { return _prohibit_id; }
        }

        #endregion

        public Config()
        {
        }

        public void initConfig(
            string strTitle,
            int iMemberJoin,
            int iPageRows,
            int iLoginMinutes,
            string strInterceptIP,
            string strManageIP,
            string strProhibitID
            )
        {
            _title = strTitle;
            _member_join = iMemberJoin;
            _page_rows = iPageRows;
            _login_minutes = iLoginMinutes;
            _intercept_ip = strInterceptIP;
            _manage_ip = strManageIP;
            _prohibit_id = strProhibitID;
        }
    }
}

