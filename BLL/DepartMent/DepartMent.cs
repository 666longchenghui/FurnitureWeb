using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;

namespace BLL.DepartMent
{
   public class DepartMent
    {
        Common.CommonClass Com = new Common.CommonClass();
        public string GetDepartMentInfo() {
            string SelDepartSql = "select DepartId,Department from DepartMent";        
            string result =Common.CommonClass.DataTableToJson( Com.Selcets(SelDepartSql));
            return result;
        }
        /// <summary>
        /// 显示部门表、分页s
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string,object> PageDepartMent(string index, string size,string departname)
        {
            string AddSQL = "";
            if (departname.Trim() != "")
            {
                AddSQL += " and Department like '%" + departname + "%'";
            }
            //string PageSQl = @"Select * from (select Department,d.DepartId,u.Uid,u.UserName,uu.UDelete,Dcreatedate,count(uu.uid) Total ,ROW_NUMBER() OVER(ORDER BY DepartId) as rank from dbo.DepartMent d left join Users u on d.Uid=u.Uid
            //        left join users uu on uu.UDepartId =d.DepartId where Ddelete=0  " + AddSQL+ " group by Department,DepartId,u.Uid, u.UserName ,Dcreatedate,uu.UDelete) as t where t.rank  between (((" + index+" - 1) * "+size+")+1) and("+index+" * "+size+")";
            string PageSQL = @"select top "+size+ " * from (select ROW_NUMBER() OVER(ORDER BY d.DepartId desc) rowIndex, Department, d.DepartId, u.Uid, u.UserName,Dcreatedate, (select count(uid) from users uu where uu.udelete = 0 and uu.udepartid = d.departid) sumUsers from dbo.DepartMent d full join Users u on d.Uid = u.Uid  where d.Ddelete = 0 " + AddSQL+ "    group by Department,DepartId,u.Uid, u.UserName ,Dcreatedate) t where  rowIndex > " + size+" * ("+index+" - 1)";
            string CountSql= @"select count(*) from (select Department,d.DepartId,u.Uid,u.UserName,Dcreatedate,count(uu.uid) a from dbo.DepartMent d left join Users u on d.Uid=u.Uid
                        left join users uu on uu.UDepartId =d.DepartId where Ddelete=0 " + AddSQL+ " group by Department,DepartId,u.Uid, u.UserName,Dcreatedate ) DepartMent";
            string ReturnPage =Common.CommonClass.DataTableToJson( Com.Selcets(PageSQL));
            string ReturnCount = Common.CommonClass.DataTableToJson(Com.Selcets(CountSql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ReturnPage", ReturnPage);
            dic.Add("ReturnCount", ReturnCount);
            return dic;
        }
        public string GetDepartMentList(string id) {
            string SelSql = @"SELECT Department,DepartId from DepartMent where DepartId=" + id +"";
            string data = Common.CommonClass.DataTableToJson(Com.Selcets(SelSql));
            return data;
        }

        /// <summary>
        /// 修改部门名称
        /// </summary>
        /// <param name="depart"></param>
        /// <param name="man"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SaveDepartMentList(string id, string name)
        {
            string Exist = @"select Department,DepartId from dbo.DepartMent where Department='" + name + "'";

            Dictionary<string, object> Diccount = Common.CommonClass.Select(Exist);
            if (Diccount != null)
            {
                return Utils.GetResult(300, "该部门已存在,请重新添加部门");
            }
            else {
                SqlParameter[] par;
                par = new SqlParameter[]
                {
                  new SqlParameter("Ddelete","0"),new SqlParameter("SaveDepartName",name),new SqlParameter("DUpdatedate",DateTime.Now),
                };
                string UpSql = @"UPDATE [DepartMent] SET [Department] =@SaveDepartName,[Ddelete] = @Ddelete ,[DUpdatedate] = @DUpdatedate WHERE DepartId=" + id + "";
                int count = Com.ExecutionSqlPar(UpSql, par);
                if (count > 0)
                {
                    return Utils.GetResult(200, "修改成功");
                }
                else
                {
                    return Utils.GetResult(300, "修改失败");
                }
            }
        }
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelUser(string id)
        {
            string SelSQL = "select * from Users where UDepartId='" + id + "'";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelSQL);
            if (dic != null)
            {
                return Utils.GetResult(300, "该部门已被使用");
            }
            string DelSql = "Update DepartMent set Ddelete=1 where DepartId=" + id + "";
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
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string AddDepart(string name) {
            string Exist = @"select * from dbo.DepartMent where Department='" + name + "'";
            Dictionary<string, object> Diccount = Common.CommonClass.Select(Exist);
            if (Diccount != null)
            {
                return Utils.GetResult(300, "该部门已存在,请重新添加部门");
            }
            SqlParameter[] par;
            //取得当前登录系统用户
            Model.Users u = Utils.GetSession();
            string sessionname = u.U_UserName;
            int SessionId = u.U_UId;
            par = new SqlParameter[] {
                    new SqlParameter("DeparmentName",name),
                    new SqlParameter("Ddelete","0"), 
                    new SqlParameter("Dcreatedate",DateTime.Now ),
                    new SqlParameter ("Sessionid", SessionId),
            };                    
            string AddSql = @"insert into DepartMent(Department,Ddelete,Dcreatedate,Uid) values(@DeparmentName,@Ddelete,@Dcreatedate,@Sessionid)";
            int count = Com.ExecutionSqlPar(AddSql, par);
            if (count>0)
            {
                return Utils.GetResult(200, "添加成功","true");
            }
            else
            {
                return Utils.GetResult(300, "添加失败");
            }
        }    
    }
}
