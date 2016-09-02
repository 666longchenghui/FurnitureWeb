using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxStock 的摘要说明
    /// </summary>
    public class AjaxStock : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.Stock.Stock st = new BLL.Stock.Stock();
            switch (action)
            {
                case "GetStockPage":
                    string Pageindex = req.Params["StockIndex"];
                    string PageSize = req.Params["StockSize"];
                    res.Write(Common.CommonClass.ToJosn( st.PageStock(Pageindex, PageSize)));
                    break;
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