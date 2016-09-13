using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Common;


namespace BLL.Account
{
public class Account
    {
        Common.CommonClass com = new Common.CommonClass();
        /// <summary>
        /// 添加客户信息
        /// </summary>
        /// <param name="NO"></param>
        /// <param name="Name"></param>
        /// <param name="Comtel"></param>
        /// <param name="Money"></param>
        /// <param name="Phone"></param>
        /// <param name="Email"></param>
        /// <param name="Note"></param>
        /// <returns></returns>
        public string Insert_AccountInfo(string NO, string Name, string Comtel, string Money, string Phone, string Email, string Note) {
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("AccountNo",NO),new SqlParameter("AccountName",Name),
                 new SqlParameter("AccountComtel",Comtel), new SqlParameter("AccountMoney",Money),
                 new SqlParameter("AccountPhone",Phone),  new SqlParameter("AccountEmail",Email),
                 new SqlParameter("AccountNote",Note),  new SqlParameter("AccountCreate",DateTime.Now),
                 new SqlParameter("AccountDelete","0"), 
            };
            string AddAccountInfo = @"INSERT INTO [dbo].[Account]([AccountNo],[AccountName],[AccountMoney],[AccountComTel],[AccountPhone],[AccountEmail],[AccountNote],[AccountCreate],[AccountDelete])
                                     VALUES(@AccountNo,@AccountName, @AccountMoney,@AccountComtel,@AccountPhone,@AccountEmail,@AccountNote,@AccountCreate, @AccountDelete)";
            int count = int.Parse(com.ExecutionSqlPar(AddAccountInfo, par).ToString());
            if (count>0)
            {
                return Utils.GetResult(200, "保存成功", "true");
            }
            else
            {
                return Utils.GetResult(400, "保存失败");
            }
        }
        /// <summary>
        /// 分页显示客户信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> Get_AccountInfo(string name,string begin ,string end,string index, string size) {
            string AddSQl = "";
            if (name.Trim()!="")
            {
                AddSQl += " and AccountName like '%"+name+"%'";
            }
            if (begin.Trim()!="" && end.Trim()!="")
            {
                AddSQl += " and '" + begin + "'< AccountCreate and AccountCreate <'" + end + "' ";
            }
            string GetAccount = "select * from (select AccountId,AccountNo,AccountName,AccountMoney,AccountComTel,AccountPhone,AccountEmail,AccountCreate ,ROW_NUMBER()OVER(ORDER by AccountId desc)as rank from Account where AccountDelete=0 "+AddSQl+") as t where t.rank between(((" + index+"-1)*"+size+")+1 )and ("+index+"*"+size+") ";
            string GetAccountCount = "select count(*) from Account  where AccountDelete=0 "+AddSQl;
            string DoGetAccount =Common.CommonClass.DataTableToJson( com.Selcets(GetAccount));
            string DoGetAccountCount = Common.CommonClass.DataTableToJson(com.Selcets(GetAccountCount));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Page", DoGetAccount);
            dic.Add("Count", DoGetAccountCount);
            return dic;
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete_AccountInfo(string id) {
            string DelAccount= @"UPDATE [DB_MgooERP].[dbo].[Account]SET AccountDelete = 1 WHERE AccountId ="+id;
            int DoCount = Common.CommonClass.ExecutionSQL(DelAccount);
            if (DoCount>0)
            {
                return Utils.GetResult(200, "删除成功", "true");
            }
            else
            {
                return Utils.GetResult(400, "删除失败");
            }
        }
        /// <summary>  
        /// 根据ID查询要显示的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SelAccountinfo(string id) {
            string Accountid = @"select AccountId,AccountNo,AccountName,AccountMoney,AccountComTel,AccountPhone,AccountEmail,AccountNote from Account where AccountId=" + id;
            return Common.CommonClass.DataTableToJson( com.Selcets(Accountid));
        }
        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companytel"></param>
        /// <param name="money"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public string Save_AccountInfo(string id,string name, string companytel, string money, string phone, string email, string note) {
            SqlParameter[] par = new SqlParameter[]{
            new SqlParameter ("AccountName",name),new SqlParameter ("AccountComtel",companytel),
                  new SqlParameter ("AccountMoney",money),new SqlParameter ("AccountPhone",phone),
                   new SqlParameter ("AccountEmail",email),new SqlParameter ("AccountNote",note),
                    new SqlParameter("AccountUpdate",DateTime.Now),
            };
            string UpdateAccountInfo = @"UPDATE [DB_MgooERP].[dbo].[Account]
                                   SET [AccountName] =@AccountName
                                      ,[AccountMoney] = @AccountMoney
                                      ,[AccountComTel] = @AccountComtel
                                      ,[AccountPhone] =@AccountPhone
                                      ,[AccountEmail] = @AccountEmail
                                      ,[AccountNote] = @AccountNote
                                      ,[AccountUpdate] = @AccountUpdate
                                 WHERE AccountId="+id;
           int count= com.ExecutionSqlPar(UpdateAccountInfo, par);
            if (count>0)
            {
                return Utils.GetResult(200, "保存成功", "true");
            }
            else
            {
                return Utils.GetResult(400, "保存失败");
            }
        }

    }
}
