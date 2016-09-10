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
                    case "GetAccountInfo":
                        string Account_name = req.Params["name"];
                        string Account_begin = req.Params["begin"];
                        string Account_end = req.Params["end"];
                        string Account_index = req.Params["index"];
                        string Account_size = req.Params["size"];
                        res.Write(Common.CommonClass.ToJosn(account.Get_AccountInfo(Account_name, Account_begin, Account_end, Account_index, Account_size)));
                        break;
                    case "DeleteAccount":
                        string AccountId = req.Params["Accountid"];
                        res.Write(account.Delete_AccountInfo(AccountId));
                        break;
                    case "SelAccountid":
                        string SelAccountId = req.Params["Accountid"];
                        res.Write(Common.CommonClass.ToJosn( account.SelAccountinfo(SelAccountId)));
                        break;
                    case "SaveAccount":
                        string Save_AccountId = req.Params["AccountID"];
                        string Save_AccountName = req.Params["Account.Name"];
                        string Save_AccountComtel = req.Params["Account.Comtel"];
                        string Save_AccountMoney = req.Params["Account.Money"];
                        string Save_AccountPhone = req.Params["Account.Phone"];
                        string Save_AccountEmail = req.Params["Account.Email"];
                        string Save_AccountNote = req.Params["Account.Note"];
                        res.Write(account.Save_AccountInfo(Save_AccountId,Save_AccountName, Save_AccountComtel, Save_AccountMoney, Save_AccountPhone, Save_AccountEmail, Save_AccountNote));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
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