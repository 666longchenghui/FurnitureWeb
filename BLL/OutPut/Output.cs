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
        public string SaveOutPut(string info,string accountid) {
            //往退货单表中增加一条数据
            var serialize = new JavaScriptSerializer();
           
            string rand = RandNumber();
            SqlParameter[] par;
            List<JsonOutPut> model = serialize.Deserialize<List<JsonOutPut>>(info);
            for (int i = 0; i < model.Count; i++)
            {
                //将商品的进行退货，退货详情表单增加两条数据，库存表相应减少
                par = new SqlParameter[] {
                     new SqlParameter("Mid",model[i].out_mid),new SqlParameter("sum",model[i].out_sum),
                     new SqlParameter("AccountId",accountid),new SqlParameter("Real",model[i].out_realprice),
                     new SqlParameter("Rand",rand),new SqlParameter("Create",DateTime.Now),
                };
            }
            return "asdas";
        }
        public string RandNumber()
        {
            long ticks = DateTime.Now.Ticks;//新建一个时间戳
            Random rad = new Random();
            string number = "OP" + ticks + rad.Next(10000) + "";
            return number;
        }
    }
}
