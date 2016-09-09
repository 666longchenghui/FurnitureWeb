using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxAccount 的摘要说明
    /// </summary>
    public class AjaxAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        { 
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.Account.Account account = new BLL.Account.Account();
            try
            {
                switch (action)
                {
                    case "AddAccount":
                        string AccountNo = req.Params["Account.NO"];
                        string AccountName = req.Params["Account.Name"];
                        string AccountComtel = req.Params["Account.Comtel"];
                        string AccountMoney = req.Params["Account.Money"];
                        string AccountPhone = req.Params["Account.Phone"];
                        string AccountEmail = req.Params["Account.Email"];
                        string AccountNote = req.Params["Account.Note"];
                     res.Write(account.Insert_AccountInfo(AccountNo, AccountName, AccountComtel, AccountMoney, AccountPhone, AccountEmail, AccountNote));
                        break;
                    default:
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