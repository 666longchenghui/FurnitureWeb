using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data.SqlClient;
namespace BLL.WareHouse
{
public class WareHouse
    {
        Common.CommonClass Com = new Common.CommonClass();
        public Dictionary<string, object> WareHousePage(string index, string size, string WareName)
        {
            string AddSQl = "";
            if (WareName.Trim() != "")
            {
                AddSQl += " and WareName like '%" + WareName + "%'";
            }
            string PageSql = @"select top " + size + " Depotid,WareName,WareHousDate,WareHouseAddress,WHContacts,WHPhone from WareHouse  where WDelete=0  " + AddSQl + " and (Depotid not in (select top (" + size + "*(" + index + "-1))Depotid from WareHouse order by Depotid)) order by Depotid desc";
            string CountSql = @"select count(*) from WareHouse where WDelete=0 " + AddSQl + "";
            string Page = Common.CommonClass.DataTableToJson(Com.Selcets(PageSql));
            string Count = Common.CommonClass.DataTableToJson(Com.Selcets(CountSql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Page", Page);
            dic.Add("Count", Count);
            return dic;
        }
        /// <summary>
        /// 查询出仓库状态
        /// </summary>
        /// <returns></returns>
        public string GetDepot()
        {
            string SelSql = "select WHid,DepotStatus from Depot";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(SelSql));
            return result;
        }
        /// <summary>
        /// 查询仓库名
        /// </summary>
        /// <returns></returns>
        public string GetDepotName()
        {
            string SelDepot = "select Wid,DepotName from WareName";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(SelDepot));
            return result;
        }
        /// <summary>
        /// 显示修改数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string EditWareHouse(string id)
        {
            string SelSql = @"select Depotid, WareName,WHContacts,WHPhone,WareHouseAddress,WNote,WDelete,WareHousDate from WareHouse  where Depotid=" + id + "";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(SelSql));
            return result;
        }
        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="contacts"></param>
        /// <param name="phone"></param>
        /// <param name="address"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public string SaveWareHouse(string id, string name, string contacts, string phone, string address, string note)
        {
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("SName",name),
                new SqlParameter ("SContacts",contacts),new SqlParameter ("SPhone",phone),
                new SqlParameter ("SAddress",address),new SqlParameter ("WUpdatedate",DateTime.Now),
               new SqlParameter ("SNote",note),new SqlParameter("WDelete",'0'),
            };
            string UpSql = @" UPDATE [WareHouse]
                                   SET [WareHouseAddress] =@SAddress                       
                                      ,[WDelete] =@WDelete
                                      ,[WHContacts] = @SContacts
                                      ,[WHPhone] = @SPhone                            
                                      ,[WareName] = @SName
                                      ,[WUpdatedate] =@WUpdatedate
                                      ,[WNote] = @SNote
                                WHERE Depotid=" + id + "";
            int count = Com.ExecutionSqlPar(UpSql, par);
            if (count > 0)
            {
                return Utils.GetResult(200, "保存成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "修改失败");
            }
        }
        /// <summary>
        /// 添加仓库
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contacts"></param>
        /// <param name="phone"></param>
        /// <param name="address"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public string AddWareHouseName(string name, string contacts, string phone, string address, string note)
        {
            string Exist = @"select WareName from dbo.WareHouse where WareName='" + name + "' and WDelete=0";
            Dictionary<string, object> Diccount = Common.CommonClass.Select(Exist);
            if (Diccount != null)
            {
                return Utils.GetResult(300, "仓库名已存在");
            }
            else
            {
                SqlParameter[] par = new SqlParameter[]
                {
                new SqlParameter("AName",name),new SqlParameter("AContacts",contacts), new SqlParameter("APhone",phone),
                new SqlParameter("AAdress",address),new SqlParameter("ANote",note),new SqlParameter("WDelete",'0'),
                new SqlParameter("WareHousDate",DateTime.Now),
                  };
                string AddSQL = @"INSERT INTO [WareHouse](WareName,WHContacts,WHPhone,WareHouseAddress,WNote,WDelete,WareHousDate)
                            VALUES(@AName,@AContacts,@APhone,@AAdress,@ANote,@WDelete,@WareHousDate)";
                int ExecutionSQL = Com.ExecutionSqlPar(AddSQL, par);
                if (ExecutionSQL > 0)
                {
                    return Utils.GetResult(200, "添加成功", "true");
                }
                else
                {
                    return Utils.GetResult(300, "添加失败");
                }
            }
        }
        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelPurchase(string id)
        {
            string DelSql = "UPDATE WareHouse set WDelete=1 where Depotid=" + id + "";
            int count = Common.CommonClass.ExecutionSQL(DelSql);
            if (count > 0)
            {
                return Utils.GetResult(200, "删除成功");
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }
        }
    }
}
