using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
            string AddAccountInfo = @"INSERT INTO [dbo].[Account]([AccountNo],[AccountName],[AccountMoney],[AccountComTel],[AccountPhone],[AccountEmail],[AccountNote],[ACCountCreate],[AccountDelete])
                                     VALUES(@AcccountNo,@AccountName, @AccountComtel,@AccountMoney,@AccountPhone,@AccountEmail,@AccountNote,AccountCreate, AccountDelete)";
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
    }
}
