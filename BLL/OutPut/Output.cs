using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace BLL.OutPut
{
  public class Output
    {
        Common.CommonClass com = new Common.CommonClass();
        /// <summary>
        /// lookup加载客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> SelectAccount(string name,string index,string size) {
            string AddSQl = "";
            if (name.Trim() != "")
            {
                AddSQl += " and AccountName like '%" + name + "%'";
            }        
            string GetAccount = "select * from (select AccountId,AccountNo,AccountName,ROW_NUMBER()OVER(ORDER by AccountId desc)as rank from Account where AccountDelete=0 " + AddSQl + ") as t where t.rank between(((" + index + "-1)*" + size + ")+1 )and (" + index + "*" + size + ") ";
            string GetAccountCount = "select count(*) from Account  where AccountDelete=0 " + AddSQl;
            string DoGetAccount = Common.CommonClass.DataTableToJson(com.Selcets(GetAccount));
            string DoGetAccountCount = Common.CommonClass.DataTableToJson(com.Selcets(GetAccountCount));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Page", DoGetAccount);
            dic.Add("Count", DoGetAccountCount);
            return dic;

        }
        public string SaveOutPut(string list,string accountid) {
            //往出库单表中增加一条数据 
            int count = 0;
            var serialize = new JavaScriptSerializer();
            string rand = RandNumber();
            SqlParameter[] par;    
            List<JsonOutPut> model = serialize.Deserialize<List<JsonOutPut>>(list);//反序列化类中字段必须与传入的字段名称对应
            par = new SqlParameter[] {
             new SqlParameter ("RandNumber",rand),new SqlParameter ("CreateTime",DateTime.Now),
             new SqlParameter("AccountId",accountid),new SqlParameter("Delete",'0'),
            };
            string OutPut = @"INSERT INTO [OutPut] ([Out_Number],[Out_Create],[Out_Accountid],[Out_Delete])
                                        VALUES(@RandNumber,@CreateTime,@AccountId,@Delete);SELECT @@IDENTITY";//取得最后一次增长的ID
            int Out_id = int.Parse(com.ExecuteScalar(OutPut, par).ToString());
            for (int i = 0; i < model.Count; i++)
            {
                //将商品的进行退货，出库详情表单增加两条数据，库存表相应减少
                par = new SqlParameter[] {
                     new SqlParameter("Mid",model[i].out_mid),new SqlParameter("sum",model[i].out_sum),new SqlParameter("TotalMoney",model[i].out_totalmoney),
                     new SqlParameter("Rand",rand),new SqlParameter("Create",DateTime.Now),new SqlParameter("RealPrice",'0'),
                     new SqlParameter("Oid",Out_id),new SqlParameter("DetailDelete",'0'),new SqlParameter("OutNote",model[i].out_sum)
                };
                //取得出库表的ID，将数据添加至出库详情表中
                string OutPutDetail = @"INSERT INTO [dbo].[OutPutDetail]([OutDate],[OutNumber],[Mid],[OutSum],[OutTotalMoney],[RealPrice],Out_id,DetailDelete,OutNote)
                                     VALUES(@Create,@Rand,@Mid,@sum,@TotalMoney,@RealPrice,@Oid,@DetailDelete,@OutNote)";
                count = com.ExecutionSqlPar(OutPutDetail,par);
                //库存数量相应减少
                string UpdateStore = @"UPDATE [dbo].[Inventory] set [InvertorySum] = InvertorySum-@sum WHERE Mid=@Mid";
                count = com.ExecutionSqlPar(UpdateStore, par);
            }
            if (count>0)
            {
                return Utils.GetResult(200, "保存成功", "true");
            }
            else
            {
                return Utils.GetResult(400, "保存失败");
            }
        }
        public string RandNumber()
        {
            long ticks = DateTime.Now.Ticks;//新建一个时间戳
            Random rad = new Random();
            string number = "OP" + ticks + rad.Next(10000) + "";
            return number;
        }
        /// <summary>
        /// 退货历史
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetOutPage(string index, string size,string no,string begin,string end ,string account) {
            string addsql = "";
            if (no.Trim()!="")
            {
                addsql += " and Out_Number like '%"+no+"%'";
            }
            if (begin.Trim() != "" && end.Trim() != "")
            {
                addsql += " and '" + begin + "'< Out_Create and Out_Create<'" + end + "' ";
               
            }
            if (account.Trim()!="")
            {
                addsql += " and AccountName like '%" + account + "%'";
            }
            string OutPage = @"select * from (select Out_id,Out_Number,Out_Create,Out_Delete,AccountName,ROW_NUMBER() OVER(Order by Out_id desc) as rank from output as o inner join Account as b on o.Out_Accountid=b.AccountId
            where Out_Delete=0 "+addsql+" )as t where t.rank between((" + index+" - 1) * "+size+")and("+index+" * "+size+")";
            string OutCount = @"select count(*) from output as o inner join Account as b on o.Out_Accountid=b.AccountId where Out_Delete=0  " + addsql + "";
            string  page=Common.CommonClass.DataTableToJson( com.Selcets(OutPage));
            string count= Common.CommonClass.DataTableToJson(com.Selcets(OutCount));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("page", page);
            dic.Add("count", count);
            return dic;
        }
    }
}
