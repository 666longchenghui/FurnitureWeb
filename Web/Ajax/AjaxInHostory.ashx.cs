using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Model;

namespace Web.Ajax
{
    /// <summary>
    /// InHostory 的摘要说明
    /// </summary>
    public class InHostory : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        { 
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.InHostory.InHostory Hostory = new BLL.InHostory.InHostory();
            switch (action)
            {
                case "AddInHostory":
                    // new JavaScriptSerializer().Serialize(productlist)
                    // string In_name = new JavaScriptSerializer().Serialize(req.Params["Arraylist"]);//反序列化
                    //字符串转对象
                    string In_name = req.Params["Arraylist"];
                    string Suppliers = req.Params["Suppliersid"];
                    res.Write(Hostory.SaveHoustory(In_name, Suppliers));
                    break;
                case "GetEditGoods":
                    string EIndex = req.Params["HIndex"];
                    string ESize = req.Params["HSize"];
                    string ELike = req.Params["Likegoods"];
                    res.Write(Common.CommonClass.ToJosn(Hostory.EditGoodsPage(EIndex, ESize, ELike)));
                    break;
                case "GetSuppliers":
                    string name = req.Params["likename"];
                    string index = req.Params["SupplierPageIndex"];
                    string size = req.Params["SupplierPageSize"];
                    res.Write(Common.CommonClass.ToJosn(Hostory.GetSuppliers(index, size, name)));
                    break;
                case "GetGoodsId":
                    string Getgoodsid = req.Params["goodsid"];
                    res.Write(Common.CommonClass.ToJosn(Hostory.GetByGoodsid(Getgoodsid)));
                    break;
                case "InHostory":
                    string In_index = req.Params["index"];
                    string In_size = req.Params["size"];
                    string In_createdate = req.Params["CreateDate"];
                    string In_enddate = req.Params["EndDate"];
                    string In_number = req.Params["OrderNumber"];
                    string In_supplier = req.Params["Distributor"];
                    res.Write(Common.CommonClass.ToJosn(Hostory.GetInHostory(In_index, In_size, In_createdate, In_enddate, In_number, In_supplier)));
                    break;
                case "DelInHoustory":
                    string InHoustory = req.Params["Pid"];
                    res.Write(Hostory.Delete(InHoustory));
                    break;
                case "GetInHostoryDetail":
                        res.Write(Common.CommonClass.ToJosn(Hostory.GetInHostoryDetail(req.Params["InHostoryId"])));
                    break;
                case "StoreManager":
                    string Sindex = req.Params["index"];
                    string SSize = req.Params["size"];
                    string StoreNo = req.Params["no"];
                    string StoreName = req.Params["name"];
                    string StoreModel = req.Params["model"];
                    
                    res.Write(Common.CommonClass.ToJosn(Hostory.GetStoreManager(Sindex, SSize,StoreNo,StoreName,StoreModel)));
                    break;
                case "InverToryDetail":
                    string Detailmid = req.Params["DetailNumber"];
                    string Detailindex = req.Params["Detailindex"];
                    string Detailsize = req.Params["Detailsize"];
                    res.Write(Common.CommonClass.ToJosn(Hostory.GetInverToryDetail(Detailmid,Detailindex,Detailsize)));
                    break;
                //根据流水号查询历史订单
                case "SelRunningNumber":
                    string OrderNo = req.Params["OrderNo"];
                    res.Write(Hostory.GetRunNumber(OrderNo));
                    break;
                default:
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