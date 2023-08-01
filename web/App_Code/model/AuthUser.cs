using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ronaldo.common;

/// <summary>
/// Summary description for AuthUser
/// </summary>

namespace Ronaldo.model
{
    public class AuthUser
    {
        protected long _id = 0;
        public long ID
        {
            get { return _id; }
        }

        protected string _loginid = null;
        public string LoginID
        {
            get { return _loginid; }
        }

        protected string _loginpwd = null;
        public string LoginPwd
        {
            get { return _loginpwd; }
        }

        protected string _nickname = null;
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        protected string _hp = null;
        public string HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        protected int _site = 0;
        public int Site
        {
            get { return _site; }
            set { _site = value; }
        }


        public AuthUser()
        {
            _id = 0;
            _loginid = null;
            _loginpwd = null;
            _nickname = null;
            _hp = null;
            _site = 0;
        }

        public AuthUser(
            long lID,
            string strLoginID,
            string strLoginPwd,
            string strNickName,
            string strHp,
            int nSite)
        {
            _id = lID;
            _loginid = strLoginID;
            _loginpwd = strLoginPwd;
            _nickname = strNickName;
            _hp = strHp;
            _site = nSite;
        }
    }
}
