using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BLL;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxGoods 的摘要说明
    /// </summary>
    public class AjaxGoods : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.Goods.GoodsInfo goods = new BLL.Goods.GoodsInfo();
            try
            {
                switch (action)
                {
                    case "GetGoodInfo":
                        string GoodsName = req.Params["GoodsName"];
                        string GoodsModel = req.Params["GoodsModel"];
                        string size = req.Params["PageSize"];
                        string index = req.Params["PageIndex"];
                        res.Write(Common.CommonClass.ToJosn(goods.GetGoodsList(GoodsName,GoodsModel, size, index)));
                        break;
                    case "GetUnit":
                        string SelUnitName = req.Params["UnitName"];
                        string UIndex = req.Params["UnitIndex"];
                        string USize = req.Params["UnitSize"];
                        res.Write(Common.CommonClass.ToJosn( goods.UnitPage(UIndex,USize, SelUnitName)));
                        break;
                    case "DelUnitList":
                        string Uid = req.Params["Uid"];
                        res.Write(goods.DelUnit(Uid));
                        break;    
                    case "GetUnitName":
                        res.Write(goods.GetUnitName());
                        break;
                    case "AddUnitName":
                        string UnitName = req.Params["UnitName"];
                        res.Write(goods.AddUnit(UnitName));
                        break;
                    case "UpUnit":
                        string Unid = req.Params["Uid"];
                        res.Write(goods.UpdateUnit(Unid));
                        break;
                    case "SaveUnit":
                        string Saveid = req.Params["Uid"];
                        string Savename = req.Params["UnitName"];
                        res.Write(goods.SaveUnit(Saveid,Savename));
                        break;        
                        //添加商品              
                    case "AddGoods":       
                        string MNumber = req.Params["custom.number"];
                        string MName = req.Params["custom.name"];
                        string MUnit = req.Params["custom.unit"];
                        string Mspecification = req.Params["custom.model"];
                        string Msaleprice = req.Params["custom.price"];
                        string Mnote = req.Params["custom.note"];
                        res.Write(goods.SaveMerchandise(MNumber, MName, MUnit, Mspecification, Msaleprice, Mnote));
                        break;
                    case "GetModel":
                        string SelModel = req.Params["ModelName"];
                        string MIndex = req.Params["ModelIndex"];
                        string MSize = req.Params["ModelSize"];
                        res.Write(Common.CommonClass.ToJosn(goods.ModelPage(MIndex, MSize,SelModel)));
                        break;

                    case "DelModel":
                        string Mid = req.Params["Mid"];
                        res.Write(goods.DelModel(Mid));
                        break;
                    case "UpdateModel":
                        string Modelid = req.Params["Modelid"];
                        res.Write(goods.UpdateModel(Modelid));
                        break;
                    case "SaveModel":
                        string SaveMid = req.Params["Modelid"];
                        string SaveMdate = req.Params["ModelText"];
                        res.Write(goods.SaveModel(SaveMid, SaveMdate));
                        break;
                    case "AddModel":
                        string AddModel = req.Params["ModelText"];
                        res.Write(goods.AddModel(AddModel));
                        break;
                    case "GetModelName":
                        res.Write(goods.SelModel());
                        break;
                    case "DelMerchandise":
                        string Merid = req.Params["Merid"];
                        res.Write(goods.DelMerchandise(Merid));
                        break;
                        //保存并入库
                    case "SaveStockIn":                     
                        string InNumber = req.Params["custom.number"];
                        string InName = req.Params["custom.name"];
                        string InUnit = req.Params["custom.unit"];
                        string Inspecification = req.Params["custom.model"];
                        string Insaleprice = req.Params["custom.price"];
                        string Innote = req.Params["custom.note"];
                       // string Insum = req.Params["sum"];
                        // string Mid = req.Params["Mid"];
                        res.Write(goods.SaveStockIn(InNumber, InName, InUnit, Inspecification, Insaleprice, Innote));
                        break;
                    case "SelGoodInfo":
                        string gid = req.Params["Gid"];
                        res.Write(goods.SelGoodsInfo(gid));
                        break;
                    case "SaveGoodInfo":
                        string Sid = req.Params["Gid"];
                        string SNumber = req.Params["custom.number"];
                        string SName = req.Params["custom.name"];
                        string SUnit = req.Params["custom.unit"];
                        string SModel = req.Params["custom.model"];
                        //string SSum = req.Params["GSum"];
                        string SSalePrice = req.Params["custom.price"];
                        string SNote = req.Params["custom.note"];

                        res.Write(goods.SaveGoodInfo(Sid, SNumber, SName, SUnit, SModel, SSalePrice, SNote));
                        break;

                }

            }
            catch (Exception ex)
            {
                res.Write("{ \"statusCode\":\"300\",\"message\":\"操作失败," + ex.Message + "\"}");
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