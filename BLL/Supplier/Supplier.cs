using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BLL.supplier
{
   public class Supplier
    {
        Common.CommonClass Com = new Common.CommonClass();
        /// <summary>
        /// 供应商资料
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,object> GetSupplierPage(string index,string size,string name,string contacts,string contactnumber, string createdate,string enddate)
        {
            string AddToSql = "";
            if (name.Trim()!="")
            {
                 AddToSql += " and SCompanyName like '%" + name + "%' ";
            }
            if (contacts.Trim()!="")
            {

                AddToSql += " and SContacts like '%" + contacts + "%' ";
            }
            if (contactnumber.Trim()!="")
            {
                AddToSql += " and SPhone like '%" + contactnumber + "%' ";
            }
            if (createdate.Trim()!= "" && enddate.Trim()!= "")
            {
                AddToSql += " and '" + createdate + "'< SCreateTime and SCreateTime <'" + enddate + "' ";
            }
            //  string PageSQL = "select top "+size+ " Superid,SCompanyName,SContacts,SAddress,SPhone,SEmail,SCreateTime,SUpdateTime,SFax from Supplier where SDelete=0 " + AddToSql+" and (SId not in (select top (" + size+"*("+index+"-1)) SId from Supplier order by Sid))order by Sid";
            string SupperPage=@"select* from (select Superid, SCompanyName, SContacts, SAddress, SPhone, SEmail, SCreateTime, SUpdateTime, SFax, ROW_NUMBER() OVER(ORDER BY Superid desc) as rank from Supplier where SDelete = 0  "+ AddToSql + " ) as t where t.rank between ((("+index+" - 1) * "+size+") + 1) and("+index+" * "+size+") ORDER BY Superid desc";
        
            //string SuperPageSQL = "select top "+size+ " Superid,SCompanyName,SContacts,SAddress,SPhone,SEmail,SCreateTime,SUpdateTime,SFax from Supplier where SDelete=0 " + AddToSql + " and (Superid not in(select top (" + size+"*("+index+"-1)) Superid from Supplier order by Superid)) order by Superid ";
            string PageTotal = "select count(*) from Supplier where SDelete=0 "+ AddToSql + "";
            string Data=Common.CommonClass.DataTableToJson(Com.Selcets(SupperPage));    
            string Count = Common.CommonClass.DataTableToJson(Com.Selcets(PageTotal));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Data", Data);
            dic.Add("Count", Count);
            return dic;
        }
        /// <summary>
        /// 删除供应商（伪删除）等于1删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelSupplier(string id) {
            string DelSql = "update Supplier set SDelete=1 WHERE Superid=" + id+"";
            int result= Common.CommonClass.ExecutionSQL(DelSql);          
            if (result>0)
            {
             return Utils.GetResult(200, "删除成功","true");              
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }     
        }
        /// <summary>
        /// 修改供应商数据,先查询显示数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpSupplier(string id) {
            string SelSql = "select Superid,SCompanyName,SContacts,SAddress,SPhone,SEmail,SCreateTime,SUpdateTime,SFax,SRemark from Supplier where SDelete=0 and Superid=" + id+"";
            return Common.CommonClass.DataTableToJson(Com.Selcets(SelSql));
        }
        /// <summary>
        /// 修改方法供应供应商资料
        /// </summary>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="address"></param>
        /// <param name="email"></param>
        /// <param name="fax"></param>
        /// <param name="phone"></param>
        /// <param name="note"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SaveSupplierList(string name, string company, string address, string email, string fax, string phone, string note,string id,string update) {
            SqlParameter[] par;
            par = new SqlParameter[] {
                new SqlParameter ("SContacts",name),new SqlParameter("SCompanyName",company),
                new SqlParameter ("SAddress",address),new SqlParameter ("SPhone",phone),
                new SqlParameter("SEmail",email),new SqlParameter ("SFax",fax),
                new SqlParameter ("SDelete","0"),new SqlParameter("SRemark",note),
                new SqlParameter("SUpdateTime",DateTime.Now) 
            };
            string UpSql = @"UPDATE Supplier set SContacts=@SContacts,SCompanyName=@SCompanyName,SAddress=@SAddress, SPhone=@SPhone,SEmail=@SEmail,SFax=@SFax,SDelete=@SDelete,SRemark=@SRemark ,SUpdateTime=@SUpdateTime where Superid="+id+"";
            //and convert(varchar(20), SUpdateTime, 120)= '" + Convert.ToDateTime( update).ToString("yyyy-MM-dd HH:mm:ss") + "'
            int count = Com.ExecutionSqlPar(UpSql,par);
            if (count>0)
            {
                return Utils.GetResult(200, "保存成功","true");
            }
            else
            {
               return Utils.GetResult(300, "保存失败");
            }     
        }
        /// <summary>
        /// 添加验证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="address"></param>
        /// <param name="email"></param>
        /// <param name="fax"></param>
        /// <param name="phone"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public string InsertSupplierList(string name, string company, string address, string email, string fax, string phone, string note ) {
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter ("CusCompany",name),new SqlParameter("CusContacts",company),
                new SqlParameter ("CusAddress",address),new SqlParameter ("CusPhone",phone),
                new SqlParameter("CusEmail",email),new SqlParameter ("CusFax",fax),
                new SqlParameter ("CusDelete","0"),new SqlParameter("CusNote",note),
                new SqlParameter("CusCreate",DateTime.Now)
            };
            string InserSql = @"insert into Supplier(SContacts,SCompanyName,SAddress,SPhone,SEmail,SFax,SDelete,SRemark,SCreateTime)VALUES
                                                (@CusContacts,@CusCompany,@CusAddress, @CusPhone,@CusEmail,@CusFax,@CusDelete,@CusNote,@CusCreate)";
            int Supercount = Com.ExecutionSqlPar(InserSql, par);
            if (Supercount > 0)
            {
                return Utils.GetResult(200, "添加成功","true");
            }
            else
            {
                return Utils.GetResult(300, "添加失败");
            }     
        }
        public string SelDepmentId(string id)
        {
            string SqlSel = @" select * from dbo.DepartMent d inner join Purchase p on d.DepartId=p.Depotid where PurchaseID="+id+"";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(SqlSel));
            return result;
        }
    }
}
