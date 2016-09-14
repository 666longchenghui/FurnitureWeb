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
                    case "GetOutPage":
                    string out_index = req.Params["outindex"];
                    string out_size = req.Params["outsize"];
                    string out_no = req.Params["outputno"];
                    string out_create = req.Params["outputcreate"];
                    string out_end = req.Params["outputend"];
                    string out_account = req.Params["outAccount"];
                    res.Write(Common.CommonClass.ToJosn(put.GetOutPage(out_index, out_size, out_no, out_create, out_end, out_account)));
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