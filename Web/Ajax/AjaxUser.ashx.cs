using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ajax
{
    /// <summary>
    /// AjaxUser 的摘要说明
    /// </summary>
    public class AjaxUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
     
            BLL.Users.Users users = new  BLL.Users.Users ();
            try
            {
                switch (action)
                {               
                    case "GetUserList":
                        string UName = req.Params["UserName"];
                        string DepartName = req.Params["DepartName"];
                        string Uindex = req.Params["UserIndex"];
                        string Usize = req.Params["UserSize"];
                        res.Write(Common.CommonClass.ToJosn((users.GetUserList(Uindex,Usize,UName,DepartName))));
                        break;    
                    case "AddUsers":
                        string LoginName = req.Params["users.lgname"];                    
                        string userName = req.Params["users.name"];
                        string Email = req.Params["users.email"];
                        string Phone = req.Params["users.phone"];
                        string Deportid = req.Params["users.depart"];
                        string Note = req.Params["users.note"];
                        res.Write(users.AddUsers(LoginName, userName, Email, Phone, Deportid, Note));
                        break;
                    case "DelUser":
                        string Uid = req.Params["UId"];
                        res.Write(users.DelUsers( Uid));
                        break;
                    case "SelPersonnerl":
                        string Userid = req.Params["UId"];
                        res.Write(users.SelPersonnerl(Userid));
                        break;
                    case "SavePersonnerl":
                        string Sid = req.Params["Uid"];          
                        string SUserName = req.Params["users.name"];
                        string SEmail= req.Params["users.email"];
                        string SPhone= req.Params["users.phone"];                 
                        string SDepart = req.Params["users.depart"];
                        string SNote = req.Params["users.note"];
                        res.Write(users.SavePersonnerl(Sid,SUserName,SEmail,SPhone,SDepart,SNote));
                        break;
                        //修改当前登录密码
                    case "Resetpwd":
                        string oldpwd = req.Params["oldpwd"];
                        string newpwd = req.Params["newpwd"];
                        string confirmpwd = req.Params["confirmpwd"];
                        res.Write(users.ResetPwd(oldpwd, newpwd, confirmpwd));
                        break;
                    case "myprofile":
                        res.Write(Common.CommonClass.ToJosn( users.MyProfile()));
                        break;
                    //设置用户密码
                    case "ResetPassword":
                        string ResetUid = req.Params["UId"];                  
                        res.Write(users.ResetPassword(ResetUid));
                        break;
                }
            }
            catch (Exception ex)
            {
                res.Write("{ \"statusCode\":\"300\",\"message\":\"操作失败," + ex.Message + "\"}");
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}