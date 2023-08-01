using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccess;

namespace Ronaldo.uibase
{

    /// <summary>
    /// Summary description for UserControlBase
    /// </summary>
    public class UserControlBase : System.Web.UI.UserControl
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

        public MasterPageBase CurrentMaster
        {
            get
            {
                PageBase pg = Page as PageBase;
                if (pg == null)
                    return null;

                return pg.Master as MasterPageBase;
            }
        }
        public UserControlBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
