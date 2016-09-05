using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Model;

namespace BLL.InHostory
{
    public class InHostory
    {
        Common.CommonClass com = new Common.CommonClass();
        /// <summary>
        /// 加载商品信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> EditGoodsPage(string index, string size,string product)
        {
            string AddSQl = "";
            if (product.Trim()!="")
            {
                AddSQl = " and Mname like '%" + product + "%'";
            }
            string PageSQL = @"SELECT TOP " + size + " m.Mid,Mname,UnitName,Mnumber,m.Unitid,DataText,Msellingprice,ISNULL(InvertorySum,0) as InvertorySum,Mnote,MCreateDate FROM Merchandise m inner join Unit u on m.Unitid=u.Unitid inner join Dictionary d on m.DictionaryID=d.DictionaryID  left join Inventory i on m.Mid=i.Mid  WHERE MDelete=0 "+AddSQl+" and(m.Mid NOT IN(SELECT TOP (" + size + "*(" + index + "-1)) Mid FROM Merchandise ORDER BY Mid))ORDER BY m.Mid";
            string CountSQL = @"select count(*) from Merchandise where MDelete=0 "+AddSQl+"";
            string StrPage = Common.CommonClass.DataTableToJson(com.Selcets(PageSQL));
            string StrCount = Common.CommonClass.DataTableToJson(com.Selcets(CountSQL));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("StrPage", StrPage);
            dic.Add("StrCount", StrCount);
            return dic;
        }
        /// <summary>
        /// 加载lookup供应商信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetSuppliers(string index, string size, string name)
        {
            string AddSQL = "";
            if (name.Trim() != "")
            {
                AddSQL = " and SCompanyName like '%" + name + "%' ";
            }
            string SuppliersPage = "select * from (select Superid,SCompanyName,SContacts ,Row_NUMber() OVER(ORDER BY Superid desc)AS RANK From Supplier WHere SDelete=0 " + AddSQL + ") as t where t.rank between(((" + index + "-1)*" + size + ")+1) and(" + index + "*" + size + " )order by Superid desc";
            string SupplierCount = "select count(*) from  Supplier WHere SDelete=0 " + AddSQL + "";
            string Page = Common.CommonClass.DataTableToJson(com.Selcets(SuppliersPage));
            string Count = Common.CommonClass.DataTableToJson(com.Selcets(SupplierCount));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Page", Page);
            dic.Add("Count", Count);
            return dic;
        }
        /// <summary>
        /// 根据返回的ID查询商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetByGoodsid(string id)
        {

            string SelIdSQL = @"select ROW_NUMBER() OVER(ORDER BY m.mid) AS ROWINDEX,Mnumber, Mname,Msellingprice,m.Mid, u.Unitid,DataText,UnitName,ISNULL(InvertorySum, 0) as InvertorySum,d.DictionaryID from Merchandise m left join Unit u on m.Unitid = u.Unitid left join Dictionary d on m.DictionaryID = d.DictionaryID
                             left join Inventory i on m.Mid = i.Mid where MDelete = 0 and m.Mid in(" + id + ") order by m.mid";
            string Resurt = Common.CommonClass.DataTableToJson(com.Selcets(SelIdSQL));
            return Resurt;
        }
        /// <summary>
        /// 历史进货单
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string,object> GetInHostory(string index,string size,string create,string end ,string number, string supplier) {
            string AppendSql="";
            string AppendCount = "";
            if (create.Trim()!="" && end.Trim()!="")
            {
                AppendSql += " and '" + create + "'< OrderCreatetime and OrderCreatetime<'" + end + "' ";
                AppendCount += " where '" + create + "'< OrderCreatetime and OrderCreatetime<'" + end + "'";
            }
            if (number.Trim()!="")
            {
                AppendSql += " and Number like '%"+number+"%'";
                AppendCount += " where  Number like '%" + number + "%'";
            }
            if (supplier.Trim()!="")
            {
                AppendSql += " and SCompanyName like '%" + supplier + "%'";
                AppendCount += " where SCompanyName like '%" + supplier + "%'";
            }
            string SelInHostory = @"select distinct top "+size+ " *  FROM  ( SELECT ROW_NUMBER() OVER (ORDER BY o.Pid desc) AS RowNumber, o.Pid, Number,OrderCreatetime,s.SCompanyName,Suppliersid FROM Orderhistory o left join PurchaseProperty p on o.Pid=p.Pid inner join Supplier s on o.Suppliersid=s.Superid  where Deleted=0 " + AppendSql+" ) A WHERE RowNumber > " + size+"*("+index+"-1)";
            string count = @"select count(*)  from (select distinct o.Pid, Number,OrderCreatetime,SCompanyName,Suppliersid from Orderhistory o left join PurchaseProperty p on o.Pid=p.Pid 
          inner join Supplier s on o.Suppliersid=s.Superid  where Deleted=0) as a " + AppendCount + "";

            string Page=Common.CommonClass.DataTableToJson( com.Selcets(SelInHostory));
            string CountSum= Common.CommonClass.DataTableToJson(com.Selcets(count));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Page", Page);
            dic.Add("CountSum",CountSum);
            return dic;  
        }
        /// <summary>
        /// 保存商品详细信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Suppliers"></param>
        /// <returns></returns>
        public string  SaveHoustory( string Name, string Suppliers)
        {
            int count = 0;
            decimal money = 0;
            var serialize = new JavaScriptSerializer();
            SqlParameter[] par;
            List<JsonMerchandise> model = serialize.Deserialize<List<JsonMerchandise>>(Name);
            string RandSum = RandNumber();
            par = new SqlParameter[] {
               new SqlParameter ("SuppliersId",Suppliers),new SqlParameter("Deleted","0"),
               new SqlParameter("OrderCreatetime",DateTime.Now), new SqlParameter("Money",money),
               new SqlParameter("number",RandSum),
           };
           string InsertOrderhistory = @"INSERT INTO [Orderhistory]([Count],[TotalMoney],[OrderCreatetime],[Number],[Suppliersid],[Deleted])
                                 VALUES(null,@Money,@OrderCreatetime,@number,@SuppliersId,@Deleted);SELECT @@IDENTITY";//取得最后一次自增ID
            int Orderhistoryid = int.Parse( com.ExecuteScalar(InsertOrderhistory, par).ToString());
            for (int i = 0; i < model.Count; i++)
            {
                par = new SqlParameter[] {
                 new SqlParameter("Mid",model[i].history_id),new SqlParameter("note",model[i].history_note),
                 new SqlParameter("taotalproce",model[i].history_money),new SqlParameter("Suppliers",Suppliers),
                 new SqlParameter("sum",model[i].history_sum),new SqlParameter("Number",RandSum),
                 new SqlParameter("PropertyDelete","0"),new SqlParameter("CreateTime",DateTime.Now),
                 new SqlParameter("Orderhistoryid",Orderhistoryid)
                };
                //历史详情表
                string InsertPurchaseProperty = @"INSERT INTO [PurchaseProperty]([MId],[PropertySum],[PropertyToal],[PropertyDelete],[CreateTime],[PropertyNote]  ,[Pid] ) 
                             VALUES(@Mid,@sum,@taotalproce,@PropertyDelete,@CreateTime,@note,@Orderhistoryid)";
                count = com.ExecutionSqlPar(InsertPurchaseProperty, par);
                //将进货的商品添加至库存
                string insertDetail = @"INSERT INTO [DB_MgooERP].[dbo].[InventoryDetail]
                                               ([Supplierid]
                                               ,[Mid]
                                               ,[InventoryId],Number,Sum,CreateTime)VALUES(@Suppliers,@Mid,null,@Number,@sum,@CreateTime)";
                count = com.ExecutionSqlPar(insertDetail, par);
             //   string UpdateSum = @"Update InventoryDetail set Sum=Sum+@sum where Mid=@Mid";
               string UpdateStore = @"UPDATE [dbo].[Inventory] set [InvertorySum] = InvertorySum+@sum WHERE Mid=@Mid";
                count = com.ExecutionSqlPar(UpdateStore, par);
            }          
            if (count > 0)
            {
                return Utils.GetResult(200, "保存成功", "true");
            }
            else
            {
                return Utils.GetResult(400, "保存失败" );
            }
        }
         public string RandNumber()
        {
            long ticks = DateTime.Now.Ticks;//新建一个时间戳
            Random rad = new Random();
            string number ="HD"+ ticks + rad.Next(10000) +"";
            return number;
        }
        /// <summary>
        /// 详细订单
        /// </summary>
        /// <param name="InHoustoryid"></param>
        /// <returns></returns>
        public string GetInHostoryDetail(string InHoustoryid) {
            string SelectPid = @"select ROW_NUMBER() OVER(ORDER BY o.Pid)as NO,o.Pid, Mname,DataText,Mnumber,UnitName,PropertySum ,SCompanyName,PropertyToal,PropertyNote from Orderhistory o left join PurchaseProperty p on o.Pid = p.Pid left join dbo.Merchandise m on p.mid = m.MId
                                                             left join Dictionary d on m.DictionaryID = d.DictionaryID
                                                               left join Unit u on u.Unitid = m.Unitid
                                                 left join dbo.Supplier s on o.Suppliersid=s.Superid where o.Pid=" + InHoustoryid + "";
           return Common.CommonClass.DataTableToJson(com.Selcets(SelectPid));
        }
        /// <summary>
        /// 删除详细订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete(string id) {
            string Pid= @"UPDATE [Orderhistory] SET [Deleted] = 1 WHERE Pid="+id+"";
            int count=  Common.CommonClass.ExecutionSQL(Pid);
            if (count>0)
            {
                return Utils.GetResult(200, "删除成功");
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }       
        }
   /// <summary>
   /// 库存 
   /// </summary>
   /// <param name="index">当前页</param>
   /// <param name="size">每页条数</param>
   /// <returns></returns>
        public Dictionary<string,object> GetStoreManager(string index ,string size,string no,string name,string model) {
            string AppendSQL = "";
            if (no.Trim()!="")
            {
                AppendSQL += " and Mnumber like '%"+no+"%'";
            }
            if (name.Trim()!="")
            {
                AppendSQL += " and Mname like '%" + name + "%'";
            }
            if (model.Trim()!="")
            {
                AppendSQL += " and UnitName like '%" + model + "%'";
            }
            string SelStrore = @"select * from (select InventoryId,Msellingprice,Mnumber,Mname,UnitName,DataText,b.Mid ,ISNULL(InvertorySum,0)as InvertorySum ,ROW_NUMBER() OVER(ORDER BY InventoryId desc) as rank from Inventory  a 
                                 inner join Merchandise as b on a.Mid=b.Mid inner join dbo.Unit as u on b.Unitid=u.Unitid inner join 
                    dbo.Dictionary as d on b.DictionaryID=d.DictionaryID where MDelete=0 "+AppendSQL+") as t where t.rank  between (((" + index+" - 1) * "+size+")+1) and("+index+" * "+size+") ORDER BY InventoryId desc";
            string Count= @" select count(*)from dbo.Inventory as a right join Merchandise as b on a.Mid=b.Mid inner join dbo.Unit as u on b.Unitid=u.Unitid inner join 
                    dbo.Dictionary as d on b.DictionaryID=d.DictionaryID where MDelete=0 "+AppendSQL+"";
            string Page= Common.CommonClass.DataTableToJson(com.Selcets(SelStrore));
            string Total = Common.CommonClass.DataTableToJson(com.Selcets(Count));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Page", Page);
            dic.Add("Total", Total);
            return dic;

        }
        /// <summary>
        /// 库存明细
        /// </summary>
        /// <returns></returns>
        public string GetInverToryDetail(string mid,string index,string size)
        {
            string OrderDeltail = "select * from (select Detailid,a.CreateTime,Mname,Number,SCompanyName,Sum, ROW_NUMBER()OVER(ORDER BY Detailid )AS Rank from InventoryDetail as a inner join dbo.Inventory as b on a.Mid=b.Mid inner join Supplier as c on a.Supplierid=c.Superid inner join  Merchandise  as d on a.mid=d.mid where a.mid=" + mid+" )as t where t.rank between((("+index+"-1)*"+size+")+1)and ("+index+"*"+size+")order by Detailid";
           // string SelDetail = @"select a.CreateTime,Mname,Number,ScompanyName,Sum from dbo.InventoryDetail as a inner join dbo.Inventory as b on a.Mid=b.Mid inner join Supplier as c on a.Supplierid=c.Superid inner join  Merchandise  as d on a.mid=d.mid  where a.mid=" + mid+"";
            return Common.CommonClass.DataTableToJson(com.Selcets(OrderDeltail));
        }
        /// <summary>
        /// 根据流水号查询出相关历史订单
        /// </summary>
        /// <param name="runnumber"></param>
        /// <returns></returns>
        public string GetRunNumber(string runnumber) {
            string selectnumber = @"select ROW_NUMBER() OVER(ORDER BY o.Pid)as Numbered,o.Pid, Mname,DataText,Mnumber,UnitName,PropertySum ,SCompanyName,PropertyToal,PropertyNote from Orderhistory o left join PurchaseProperty p on o.Pid = p.Pid left join dbo.Merchandise m on p.mid = m.MId
                                                             left join Dictionary d on m.DictionaryID = d.DictionaryID
                                                               left join Unit u on u.Unitid = m.Unitid
                                                 left join dbo.Supplier s on o.Suppliersid = s.Superid where Number = '" + runnumber + "'";
            return Common.CommonClass.DataTableToJson( com.Selcets(selectnumber));
        }
    }
}
