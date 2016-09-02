using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Model;

namespace BLL.Users
{
    public class Users
    {

        Common.CommonClass Com = new Common.CommonClass();
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetUserList(string index, string size, string username, string departname)
        {
            string AddSQL = "";
            if (username.Trim() != "")
            {
                AddSQL = " and UserName like '%" + username + "%'";
            }
            if (departname.Trim() != "")
            {
                AddSQL = " and Department like '%" + departname + "%'";
            }
            string UserSql = " select top " + size + " u.UId,UserName,Department,LoginName,UCreateTime,UPhone,UEmail from  dbo.Users u  inner join DepartMent d on u.UDepartId=d.DepartId where UDelete=0 " + AddSQL + " and (u.Uid Not in(select top(" + size + "*(" + index + "-1)) u.Uid from Users order by u.UId))order by u.UId desc";
            string UserCount = "select count(*) from Users u inner join DepartMent d on u.UDepartId=d.DepartId where UDelete=0 " + AddSQL + " ";
            string resultData = Common.CommonClass.DataTableToJson(Com.Selcets(UserSql));
            string resultCount = Common.CommonClass.DataTableToJson(Com.Selcets(UserCount));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("resultData", resultData);
            dic.Add("resultCount", resultCount);
            return dic;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="loginname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="Deportid"></param>
        /// <param name="Node"></param>
        /// <returns></returns>

        public string AddUsers(string loginname, string username, string email, string phone, string Deportid, string Note)
        {
            string SelLogin = "select LoginName from Users where LoginName='" + loginname + "' and UDelete=0";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelLogin);
            if (dic == null)
            {
                SqlParameter[] par;
                par = new SqlParameter[] {
                new SqlParameter("LoginName",loginname),
                new SqlParameter("Upwd","123456"),
                new SqlParameter ("username",username),
                new SqlParameter ("Email",email),
                new SqlParameter("Phone",phone),
                new SqlParameter("Deportid",Deportid),
                new SqlParameter("Note",Note),
                new SqlParameter("UCreateTime",DateTime.Now),
                new SqlParameter("UDelete","0"),
            };
                string AddSql = @"INSERT INTO [Users]
                               ([UserName]
                               ,[LoginName]
                               ,[UCreateTime]
                               ,[UPhone]
                               ,[UEmail]
                               ,[UDelete]
                               ,[Upwd]
                               ,[UDepartId]
                               ,[UNote])
                         VALUES
                               (@userName,@LoginName,@UCreateTime,@Phone,@Email,@UDelete,@Upwd,@Deportid,@Note)";
                int count = Com.ExecutionSqlPar(AddSql, par);
                if (count > 0)
                {
                    return Utils.GetResult(200, "添加成功", "true");
                }
                else
                {
                    return Utils.GetResult(300, "添加失败");
                }
            }
            else {
                return Utils.GetResult(300, "此用户名被占用");
            }

        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelUsers(string id)
        {
            string DelSQL = "update Users set UDelete=1 where UId=" + id + "";
            int count = Common.CommonClass.ExecutionSQL(DelSQL);
            if (count > 0)
            {
                return Utils.GetResult(200, "删除成功");
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }
        }

        public string SelPersonnerl(string uid)
        {
            string SelPerSQL = @"select UId,UserName,UDepartId,LoginName,UPhone,UEmail,UNote from Users where UId=" + uid + "";
            return Common.CommonClass.DataTableToJson(Com.Selcets(SelPerSQL));
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="name"></param>
        /// <param name="uemail"></param>
        /// <param name="phone"></param>
        /// <param name="depart"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public string SavePersonnerl(string Sid, string name, string uemail, string phone, string depart, string note)
        {
            //string SelLogin = "select LoginName from Users where LoginName='" + loginname + "' and UDelete=0";
            //Dictionary<string, object> dic = Common.CommonClass.Select(SelLogin);

            SqlParameter[] par = new SqlParameter[]
            {
                //new SqlParameter ("SLoginName",loginname),
                //new SqlParameter ("SPwd",upwd),
                new SqlParameter ("SUserName",name),new SqlParameter ("SEmail",uemail),new SqlParameter ("SPhone",phone),
                new SqlParameter ("UUpdateTime",DateTime.Now), new SqlParameter ("SDepart",depart),new SqlParameter ("SNote",note),
                new SqlParameter ("UDelete",'0'),
            };
            string SaveSQL = @"UPDATE [Users]
                           SET [UserName] = @SUserName
                              ,[UUpdateTime] = @UUpdateTime
                              ,[UPhone] =@SPhone
                              ,[UEmail] =@SEmail
                              ,[UDelete] = @UDelete                    
                              ,[UDepartId] = @SDepart
                              ,[UNote] = @SNote
                         WHERE UId=" + Sid + "";
            int count = Com.ExecutionSqlPar(SaveSQL, par);
            if (count > 0)
            {
                return Utils.GetResult(200, "修改成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "修改失败");
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <param name="confirmpwd"></param>
        /// <returns></returns>
        public string ResetPwd(string oldpwd, string newpwd, string confirmpwd)
        {
            Model.Users session = Utils.GetSession();
            string SessionName = session.U_LoginName;//取得当前登陆系统的登录名
            string SelLoginName = @"select Upwd from Users where LoginName='" + SessionName + "'";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelLoginName);
            string upwd = dic["Upwd"].ToString();//取出当前登录密码
            //判断用户输入的旧密码与数据库中密码是否一致
            if (upwd == oldpwd)
            {
                if (newpwd != confirmpwd)
                {
                    return Utils.GetResult(300, "两次密码输入不一致");
                }
                else
                {
                    string Setpwd = "UPDATE [Users] SET [Upwd] ='" + newpwd + "' WHERE LoginName='" + SessionName + "'";
                    int count = Common.CommonClass.ExecutionSQL(Setpwd);
                    if (count > 0)
                    {
                        return Utils.GetResult(200, "修改成功", "true");
                    }
                    else
                    {
                        return Utils.GetResult(300, "修改失败");
                    }
                }
            }
            else
            {
                return Utils.GetResult(300, "旧密码输入错误");
            }
        }
        /// <summary>
        /// 加载个人资料
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> MyProfile() {
            Model.Users session = Utils.GetSession();
            string SessionName = session.U_LoginName;//取得当前登陆系统的登录名
            string SelLoginName = @"select u.UId,UserName,LoginName,UDepartId,UCreateTime,UNote,Upwd,UPhone,UEmail,Department from Users u inner join DepartMent d on u.UDepartId=d.DepartId where LoginName='" + SessionName + "'";
            Dictionary<string, object> dicc = Common.CommonClass.Select(SelLoginName);
            string date = Convert.ToDateTime(dicc["UCreateTime"].ToString()).ToString("yyyy-MM-dd");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("date", date);
            dic.Add("dicc", dicc);
            return dic;
        }
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string ResetPassword(string uid) {
            string ResetSQL = "UPDATE [Users] SET [Upwd] ='123456' WHERE UId=" + uid + "";
            int count= Common.CommonClass.ExecutionSQL(ResetSQL);
            if (count>0)
            {
                return Utils.GetResult(200, "密码重置成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "密码重置失败", "true");
            }
        }
    }
}
