using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxBills 的摘要说明
    /// </summary>
    public class AjaxBills : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.Bills.Bills bill = new BLL.Bills.Bills();
            try
            {
                switch (action) {
                    case "GetBills":
                        string index = req.Params["BillsIndex"];
                        string size = req.Params["BillsSize"];

                      //  res.Write();
                        break;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
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