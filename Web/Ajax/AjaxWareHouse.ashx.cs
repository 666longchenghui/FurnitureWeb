using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;


namespace Web.Ajax
{
    /// <summary>
    /// AjaxWareHouse 的摘要说明
    /// </summary>
    public class AjaxWareHouse : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.WareHouse.WareHouse WH = new BLL.WareHouse.WareHouse();
            switch (action)
            {
                case "GetWHPage":
                    string WareName = req.Params["WareHouseName"];
                    string Whindex = req.Params["WHIndex"];
                    string Whsize = req.Params["WhSize"];
                    res.Write(Common.CommonClass.ToJosn( WH.WareHousePage(Whindex,Whsize, WareName)));                
                    break;
                case "GetDepot":
                    res.Write(WH.GetDepot());
                    break;
                case "GetDepotName":
                    res.Write(WH.GetDepotName());
                    break;
                case "EditWareHouese":
                    string id = req.Params["id"];
                    res.Write(WH.EditWareHouse(id));
                    break;
                case "SaveWareHouse":           
                    string Sid = req.Params["id"];
                    string SName = req.Params["WareHouse.name"];
                    string SContacts = req.Params["WareHouse.manager"];
                
                    string SPhone = req.Params["WareHouse.phone"];
                    string SAddress = req.Params["WareHouse.address"];
                    string SNote = req.Params["WareHouse.note"];
                    res.Write(WH.SaveWareHouse(Sid, SName, SContacts, SPhone, SAddress, SNote));
                    break;
                case "AddWarehouse":
                    string AName = req.Params["WareHouse.name"];
                    string AContacts = req.Params["WareHouse.manager"];
                    string AWPhone = req.Params["WareHouse.phone"];
                    string AAdress = req.Params["WareHouse.address"];
                    string ANote = req.Params["WareHouse.note"];
                    res.Write(WH.AddWareHouseName(AName, AContacts, AWPhone, AAdress, ANote));
                    break;
                case "DelWareHouse":
                    string Pid = req.Params["pid"];
                    res.Write(WH.DelPurchase(Pid));
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