using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BLL.Purchase
{
   public class Purchase
    {
        /// <summary>
        /// 采购订单表
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="size">每页显示条数</param>
        /// <returns></returns>
        /// 
        Common.CommonClass com = new Common.CommonClass();
        public Dictionary<string, object> PurchasePage(string index, string size, string name, string number)
        {
            var LikeSql = "";
            if (number.Trim() != "")
            {
                LikeSql += " and PurchaseNumber  like '%" + number + "%' ";
            }
            if (name.Trim() != "")
            {
                LikeSql += " and SCompanyName like '%" + name + "%' ";
            }
            string PageSql = @"select top " + size + " p.PurchaseName,PurchaseSum,PurchaseMoney,UnitName , PurchaseID,DepotName,n.Wid,PurchaseNumber,d.Department,PurchaseMan,SCompanyName,PurchaseType,PurchaseDate ,PurchaseNote ,CheckId from Purchase p inner join Supplier s on p.Supplierid=s.SId  inner join WareHouse w on p.Depotid=w.Depotid inner join WareName  n on w.wid=n.Wid inner join DepartMent d on p.DepartId=d.DepartId inner join Unit t on t.Unitid=p.Unitid  where p.PDeleted=0 " + LikeSql + " and(PurchaseID not in (select top (" + size + "*(" + index + "-1))PurchaseID  from Purchase order by PurchaseID)) order by PurchaseID";
            string CountSql = @"select count(*) from Purchase p 
                              inner join Supplier s on p.Supplierid=s.SId 
                              inner join WareHouse w on p.Depotid=w.Depotid
                              inner join DepartMent d on p.DepartId=d.DepartId 
                              inner join WareName  n on w.wid=n.Wid
                              inner join Unit t on t.Unitid=p.Unitid
                              where p.PDeleted=0";
            string DataSql = Common.CommonClass.DataTableToJson(com.Selcets(PageSql));
            string DataCount = Common.CommonClass.DataTableToJson(com.Selcets(CountSql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DataSql", DataSql);
            dic.Add("DataCount", DataCount);
            return dic;
        }
        /// <summary>
        /// 显示子窗口仓库列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> DpotPage(string index, string size,string name)
        {
            string LikeSql = "";
            if (name.Trim()!="")
            {
                LikeSql += " and WareHouseName like '%" + name+"%' ";
                
            }
            string PageSql = @"select top " + size + " n.Wid, Depotid,WareHousDate,DepotName,DepotStatus ,WareHouseAddress from WareHouse  w inner join WareName n on w.wid=n.Wid inner join Depot d on d.WHid=w.WHid where WDelete=0  " + LikeSql + " and (Depotid not in (select top (" + size + "*(" + index + "-1))Depotid  from WareHouse order by Depotid)) order by Depotid";
            string CountSql = @"select count(*) from WareHouse w inner join WareName n on w.wid=n.Wid inner join Depot d on d.WHid=w.WHid where WDelete=0 " + LikeSql + "";
            Common.CommonClass com = new Common.CommonClass();
            string DataSql = Common.CommonClass.DataTableToJson(com.Selcets(PageSql));
            string DataCount = Common.CommonClass.DataTableToJson(com.Selcets(CountSql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DataSql", DataSql);
            dic.Add("DataCount", DataCount);
            return dic;
        }

        /// <summary>
        /// 子窗体显示供应商 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Dictionary<string, object> PuSupplier(string index, string size, string name)
        {
            string LikeSql = "";
            if (name.Trim() != "")
            {
                LikeSql += " and SCompanyName like '%" + name + "%' ";
            }
            string PageSql = @"select * from (select Superid ,SCompanyName,SupplierNumber,ROW_NUMBER() OVER(ORDER BY Superid)AS RANK FROM Supplier WHERE SDelete= 0) as t where t.rank between (((" + index + "-1)*" + size + ")+1) and (" + index + "*" + size + ") order by Superid";
            string CountSql = @"select count(*) from Supplier where SDelete=0 " + LikeSql + "";
            Common.CommonClass com = new Common.CommonClass();
            string DataSql = Common.CommonClass.DataTableToJson(com.Selcets(PageSql));
            string DataCount = Common.CommonClass.DataTableToJson(com.Selcets(CountSql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("DataSql", DataSql);
            dic.Add("DataCount", DataCount);
            return dic;
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="depart"></param>
        /// <param name="way"></param>
        /// <param name="number"></param>
        /// <param name="depot"></param>
        /// <param name="man"></param>
        /// <param name="supplier"></param>
        /// <param name="note"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public string PurchaseAdd(string depart,string number, string date,string depot,string man,string supplier,string note,string type,string name,string unitname,string sum,string price) {
            //获得仓库的ID
            string SelId =(@"select w.Depotid from  dbo.WareHouse
                             w inner join  dbo.WareName  n on w.wid= n.wid   where n.DepotName  = '" + depot + "'");
            //获得供应商ID
            string SelSupplierId = @"select Supplierid from Purchase p inner join Supplier s on p.Supplierid=s.SId
                            where s.SCompanyName='" + supplier + "'";
            //取出仓库ID
            Dictionary<string,object> Decount = Common.CommonClass.Select(SelId);
            string decount = string.Empty;
            foreach (var key in Decount)
            {
                decount = key.Value.ToString();
            }
            //取出供应商ID
            Dictionary<string, object> Sucount = Common.CommonClass.Select(SelSupplierId);
            string sucount = string.Empty;
            foreach (var key in Sucount)
            {
                sucount = key.Value.ToString();
            }
            //赋值
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("Depart",depart),new SqlParameter("Name",name),
                new SqlParameter("Number",number),new SqlParameter ("Depotid",int.Parse(decount)),
                new SqlParameter ("SupplierId",int.Parse(sucount)),new SqlParameter ("Note",note),
                new SqlParameter ("Date",date),new SqlParameter ("Man",man),
                new SqlParameter ("Type",type),new SqlParameter ("PDeleted","0"),
                new SqlParameter ("UnitName",unitname),new SqlParameter ("Sum",sum),
                new SqlParameter ("Price",price),new SqlParameter("CheckId","0")

            };
            //判断创建的订单号是否存在
            string ExistNum = @"select PurChaseNumber from dbo.Purchase where PurChaseNumber='"+ number + "'";
          
             Dictionary<string,object> Diccount= Common.CommonClass.Select(ExistNum);
            if (Diccount != null)
            {
                return Utils.GetResult(300, "订单号已存在");
            }
            //添加至数据库
            string AddSql = @"insert into Purchase(PurchaseDate,PurchaseNumber,DepartId,PurchaseMan,PurchaseType,PurchaseNote,Supplierid,Depotid,PDeleted,PurchaseName,PurchaseSum,PurchaseMoney,Unitid,CheckId)
                          values(@Date,@Number,@Depart,@Man,@Type,@Note,@SupplierId,@Depotid,@PDeleted,@Name,@Sum,@Price,@UnitName,@CheckId)";
            int count= com.ExecutionSqlPar(AddSql, par);
            if (count > 0)
            {
                return Utils.GetResult(200, "添加成功");
            }
            else {
                return Utils.GetResult(300, "添加失败");
            }
        }
        /// <summary>
        /// 删除订单表（伪删除）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelPurchase(string id) {
          string DelSql = "UPDATE Purchase set PDeleted=1 where PurchaseID="+id+"";
          int count= Common.CommonClass.ExecutionSQL(DelSql);
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
        /// 文本框显示要修改数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public string UpPurchase(string uid) {
            string SelSql = @"select Supplierid,PurchaseName,PurchaseSum,PurchaseMoney,p.Unitid,UnitName, PurchaseID,n.Wid,SId,DepotName,PurchaseNumber,w.Depotid,d.Department,d.DepartId,PurchaseMan,SCompanyName,PurchaseType,PurchaseDate ,PurchaseNote from Purchase p
                                inner join Supplier s on p.Supplierid=s.SId 
                                inner join WareHouse w on p.Depotid=w.Depotid
                                inner join DepartMent d on p.DepartId=d.DepartId  
                                inner join  WareName n on w.wid=n.Wid
                                inner join Unit u on p.Unitid=u.Unitid
                                where PDeleted=0 and PurchaseID=" + uid+"";
            return Common.CommonClass.DataTableToJson(com.Selcets(SelSql));
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="Ckid"></param>
        /// <returns></returns>
        public string AuditingNumber(string Ckid,string purid)
        {
            string Upsql="";
            if (Ckid == "0")
            {
                Upsql += "UPDATE Purchase set CheckId=1 where PurchaseID=" + purid + "";
            }
            if (Ckid == "1")
            {
                Upsql += "UPDATE Purchase set CheckId=0 where PurchaseID=" + purid + "";
            }                     
            int count = Common.CommonClass.ExecutionSQL(Upsql);
            if (count>=0)
            {
                return Utils.GetResult(200, "审核通过");
            }
            else
            {
                return Utils.GetResult(300, "未通过审核");
            }
        }
        //DepotMent, PType, PNumber, PMan, PDepotId, SupplierId, PNote)
        public string UpdateSave(string id,string depatment,string type,string number,string man,string depotid,string supplierid,string  note, string Update) {
            //string SelDeSql = @"select h.Depotid from WareName w inner join WareHouse h on w.Wid=h.wid where DepotName='" + depotid+"'";
            //string SelSuSql = @"select Sid from dbo.Supplier where ScompanyName='"+ supplierid + "'";
            //获得仓库的ID
            string SelId = (@"select w.Depotid from  dbo.WareHouse
                             w inner join  dbo.WareName  n on w.wid= n.wid   where n.DepotName  = '" + depotid + "'");
            //获得供应商ID
            string SelSupplierId = @"select Supplierid from Purchase p inner join Supplier s on p.Supplierid=s.SId
                            where s.SCompanyName='" + supplierid + "'";
            //取出仓库ID
            Dictionary<string, object> Decount = Common.CommonClass.Select(SelId);
            string decount = string.Empty;
            foreach (var key in Decount)
            {
                decount = key.Value.ToString();
            }
            //取出供应商ID
            Dictionary<string, object> Sucount = Common.CommonClass.Select(SelSupplierId);
            string sucount = string.Empty;
            foreach (var key in Sucount)
            {
                sucount = key.Value.ToString();
            }
            SqlParameter[] par = new SqlParameter[] {
             new SqlParameter("DepotMent",depatment),new SqlParameter("Sid",id),
             new SqlParameter("PNumber",number),new SqlParameter ("PDepotId",int.Parse(decount)),
             new SqlParameter ("SupplierId",int.Parse(sucount)),new SqlParameter ("Note",note),
             new SqlParameter ("PMan",man),new SqlParameter ("CheckId","0"),
             new SqlParameter ("Type",type),new SqlParameter ("PDeleted","0"),
             new SqlParameter ("Update",DateTime.Now),
            };
            string UpSql = @"UPDATE [Purchase]
                                   SET [PUpdateTime] = @Update
                                      ,[PurchaseNumber] =@PNumber
                                      ,[PurchaseMan] = @PMan
                                      ,[PurchaseType] =@Type
                                      ,[PurchaseNote] = @Note
                                      ,[Supplierid] = @SupplierId
                                      ,[Depotid] = @PDepotId
                                      ,[PDeleted] =@PDeleted
                                      ,[CheckId] = @CheckId
                                      ,[DepartId] =@DepotMent
                              WHERE PurchaseID=@Sid";
            //and convert(varchar(20),PUpdateTime,120)='" + Convert.ToDateTime(Update).ToString("yyyy-MM-dd HH:mm:ss") + "'
            int count = com.ExecutionSqlPar(UpSql, par);
            if (count > 0)
            {
                return Utils.GetResult(200, " 修改成功");
            }
            else
            {
                return Utils.GetResult(300, "保存失败");
            }
        }
    }
}
