using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;
using System.IO;  
using System.Web;
using System.Web.UI;

using Ronaldo.common;

namespace Ronaldo.uibase
{
    /// <summary>
    /// Summary description for CompressedViewStatePageBase
    /// </summary>
    public class CompressedViewStatePageBase : System.Web.UI.Page
    {
        protected override object LoadPageStateFromPersistenceMedium()
        {
            string viewState = Request.Form["__VSTATE"];
            byte[] bytes = Convert.FromBase64String(viewState);
            bytes = Compressor.Decompress(bytes);
            LosFormatter formatter = new LosFormatter();
            return formatter.Deserialize(Convert.ToBase64String(bytes));
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            LosFormatter formatter = new LosFormatter();
            StringWriter writer = new StringWriter();
            formatter.Serialize(writer, viewState);
            string viewStateString = writer.ToString();
            byte[] bytes = Convert.FromBase64String(viewStateString);
            bytes = Compressor.Compress(bytes);
            ClientScript.RegisterHiddenField("__VSTATE", Convert.ToBase64String(bytes));
        }
    }

}
