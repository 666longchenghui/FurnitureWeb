using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Stock
{
   public class Stock
    {
        Common.CommonClass com = new Common.CommonClass();
 
        /// <summary>
        /// 入库单分页显示
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string,object> PageStock(string index, string size) {
            string PageSql = @"select top "+size+" s.Stockid,s.Mid,u.Mnumber,u.Mname,u.Msellingprice,u.MCreateDate,t.UnitName ,d.DataText,UserName from StockIn s inner join Merchandise u on s.Mid=u.Mid inner join Unit t on u.Unitid=t.Unitid inner join Dictionary d on u.DictionaryID=d.DictionaryID  left join Users r on r.UId=u.UId where StockDelete=0 and (Stockid not in (select top ("+size+"*("+index+"-1)) s.Stockid from StockIn order by s.Stockid)) order by Stockid ";
            string CountSql = @"select count(*) from StockIn";//计算总条数
            string PageStr=Common.CommonClass.DataTableToJson( com.Selcets(PageSql));
            string CountStr = Common.CommonClass.DataTableToJson(com.Selcets(CountSql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("PageStr", PageStr);
            dic.Add("CountStr", CountStr);
            return dic;
        }
    }
}
