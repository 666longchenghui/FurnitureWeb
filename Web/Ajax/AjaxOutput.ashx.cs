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
            BLL.OutPut.Output put = new BLL.OutPut.Output();
         
            try
            {
                switch (action) {
                    //look加载客户信息
                    case "GetSelectAccount":
                        string acc_name = req.Params["accountname"];
                        string acc_index = req.Params["accountindex"];
                        string acc_size = req.Params["accountsize"];
                        res.Write(Common.CommonClass.ToJosn(put.SelectAccount( acc_name,acc_index,acc_size)));
                        break;
                    case "OutputInfo":
                        string outputinfo = req.Params["ArrayList"];
                        string AccountId = req.Params["accountid"];                     
                        res.Write(put.SaveOutPut(outputinfo, AccountId));
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