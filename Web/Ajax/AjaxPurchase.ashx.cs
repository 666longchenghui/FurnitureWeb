using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxPurchase 的摘要说明
    /// </summary>
    public class AjaxPurchase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.Purchase.Purchase pruchase = new BLL.Purchase.Purchase();
            try
            {
                   switch (action)
                {
                    case "GetPurchase":
                        string LikeNumber = req.Params["Number"];
                        string LikeName = req.Params["SupplierName"];
                        string Pageindex = req.Params["PurchaseIndex"];
                        string PageSize = req.Params["PurchaseSize"];
                        res.Write(Common.CommonClass.ToJosn( pruchase.PurchasePage(Pageindex, PageSize,LikeName,LikeNumber)));
                        break;
                    case "GetDepot":
                        string EdPageindex = req.Params["DepotIndex"];
                        string EdPageSize = req.Params["DepotSize"];
                        string EdPageName = req.Params["EdPageName"];
                        res.Write(Common.CommonClass.ToJosn(pruchase.DpotPage(EdPageindex, EdPageSize, EdPageName)));
                        break;
                        //模态窗加载供应商数据
                    case "GetPurSupplier":
                        string SupplierName = req.Params["SupplierName"];
                        string PurSupplierPageIndex = req.Params["SupplierPageIndex"];
                        string PurSupplierPageSize = req.Params["SupplierPageSize"];
                        res.Write(Common.CommonClass.ToJosn(pruchase.PuSupplier(PurSupplierPageIndex, PurSupplierPageSize, SupplierName)));
                        break;
                    case "InsertPurchase":         
                        string Depart = req.Params["Depart"];     
                        string Number = req.Params["Number"];
                        string Date = req.Params["Date"];
                        string Depot = req.Params["Depot"];
                        string Man = req.Params["Man"];
                        string Supplier = req.Params["Supplier"];
                        string Note = req.Params["Note"];
                        string Type = req.Params["type"];
                        string Name = req.Params["Name"];
                        string UnitName = req.Params["UnitName"];
                        string Sum = req.Params["Sum"];
                        string Price = req.Params["Money"];         
                        res.Write(pruchase.PurchaseAdd(Depart,Number,Date,Depot,Man,Supplier,Note, Type, Name,UnitName,Sum,Price));
                        break;
                    case "DelPurchaseList":
                        string Pid =req.Params["pid"];
                        res.Write(pruchase.DelPurchase(Pid));
                        break;
                        //修改
                    case "UpSupplier":
                        string UpId = req.Params["id"];
                        res.Write(pruchase.UpPurchase(UpId));
                        break;
                        //审核
                    case "Auditing":
                        string CKid = req.Params["Checkid"];
                        string Purchaseid = req.Params["Purchaseid"];
                        res.Write(Common.CommonClass.ToJosn( pruchase.AuditingNumber(CKid,Purchaseid)));
                        break;
                    case "UpdateSave":

                        string Sid = req.Params["id"];
                        string DepotMent = req.Params["Depart"];
                        string PType = req.Params["PurchaseType"];
                        string PNumber = req.Params["PurchaseNumber"];
                        string PMan = req.Params["PurchaseMan"];
                        string PDepotId = req.Params["Depot"];
                        string SupplierId = req.Params["supplier"];
                        string PNote = req.Params["Note"];
                        string Update = req.Params["UpDataTime"];
                        res.Write(pruchase.UpdateSave(Sid, DepotMent, PType, PNumber, PMan, PDepotId, SupplierId, PNote,Update));
                        break;
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            //context.Response.Write("Hello World");
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