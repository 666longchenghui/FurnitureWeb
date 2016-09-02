using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxSupplierList 的摘要说明
    /// </summary>
    public class AjaxSupplierList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            BLL.supplier.Supplier supplier = new BLL.supplier.Supplier();
            try
            {
                switch (action)
                {
                    case "GetSupplier":
                        string Pageindex = req.Params["SupplierPageIndex"];
                        string PageSize = req.Params["SupplierPageSize"];
                        string SupplierName = req.Params["SupplierName"];
                        string Contacts = req.Params["Contacts"];
                        string ContactNumber = req.Params["ContactNumber"];
                        string CreateDate = req.Params["CreateDate"];
                        string EndDate = req.Params["EndDate"];
                        res.Write(Common.CommonClass.ToJosn(supplier.GetSupplierPage(Pageindex, PageSize, SupplierName, Contacts, ContactNumber, CreateDate,EndDate)));
                        break;
                    case "DelSupplier":
                        string Sid = req.Params["Sid"];             
                        res.Write(supplier.DelSupplier(Sid));
                        break;
                    case "UpdateSupplier":
                        string id = req.Params["id"];
                        res.Write(supplier.UpSupplier(id));
                        break;
                    case "SaveSupplier":
                        string Editid = req.Params["id"];
                        string Scompany = req.Params["supplier.name"];
                        string Sname = req.Params["supplier.contact"];
                        string Saddress = req.Params["supplier.address"];
                        string Semail= req.Params["supplier.email"];
                        string Sfax = req.Params["supplier.fax"];
                        string Sphone = req.Params["supplier.phone"]; 
                        string Snote = req.Params["supplier.note"];
                        string SUpdate = req.Params["supplier.date"];
                        res.Write(supplier.SaveSupplierList(Sname, Scompany, Saddress, Semail, Sfax, Sphone, Snote, Editid,SUpdate));
                        break;
                    case "InserSupplier":          
                        string CusCompany = req.Params["supplier.contact"];
                        string CusContacts = req.Params["supplier.name"]; 
                        string CusAddress = req.Params["supplier.address"];
                        string CusEmail = req.Params["supplier.email"];
                        string CusFax = req.Params["supplier.fax"];
                        string CusPhone = req.Params["supplier.phone"];
                        string CusNote = req.Params["supplier.note"];
                        res.Write(supplier.InsertSupplierList(CusCompany,CusContacts, CusAddress, CusEmail, CusFax, CusPhone, CusNote));
                        break;
                    case "SelDepart":
                        string Depmentid = req.Params["id"];
                        res.Write(supplier.SelDepmentId(Depmentid));
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
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