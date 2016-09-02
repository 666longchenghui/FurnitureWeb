using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;

namespace Web.Ajax
{
    /// <summary>
    /// Uploadfiy 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

          
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}