using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BLL;
namespace Web.Ajax
{
    /// <summary>
    /// AjaxDepartMent 的摘要说明
    /// </summary>
    public class AjaxDepartMent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.DepartMent.DepartMent ment = new BLL.DepartMent.DepartMent();
            switch (action)
            {
                case "GetDepartMent":
                    res.Write(ment.GetDepartMentInfo());
                    break;
                case "GetPageDepart":
                    string SelDepartName = req.Params["DepartName"];
                    string PageIndex = req.Params["DepartIndex"];
                    string PageSize = req.Params["DepartSize"];
                    res.Write(Common.CommonClass.ToJosn( ment.PageDepartMent(PageIndex, PageSize, SelDepartName)));
                    break;
                case "SelDepart":
                    string id = req.Params["id"];
                    res.Write(ment.GetDepartMentList(id));
                    break;
                case "SaveDepartMent":
                    string SaveId = req.Params["DepartId"];
                    string SaveDepartName = req.Params["depart.name"];                                  
                    res.Write(ment.SaveDepartMentList( SaveId, SaveDepartName));
                    break;
                case "DelUser":
                    string uid = req.Params["Uid"];
                    res.Write(ment.DelUser(uid));
                    break;
                case "AddDepart":
                    string DeparmentName = req.Params["depart.name"];   
                    res.Write(ment.AddDepart(DeparmentName));
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