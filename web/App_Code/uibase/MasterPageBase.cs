using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess;

namespace Ronaldo.uibase
{
    /// <summary>
    /// Summary description for MasterPageBase
    /// </summary>
    public class MasterPageBase : System.Web.UI.MasterPage
    {
        public MSSqlAccess DBConn
        {
            get
            {
                return ((PageBase)Page).DBConn;
            }
        }

        public PageBase CurrentPage
        {
            get { return Page as PageBase; }
        }
        public MasterPageBase()
        {
        }

        public virtual void UpdateUserInfo()
        {
        }

        public virtual void outputRes2JS(string[] strNames, string[] strValues)
        {

        }
    }
}

