using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BLL;

namespace Web.Admin
{


    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.CommonClass com = new Common.CommonClass();
            try
            {
                if (Request.Params["action"] == "Logout")
                {
                    // Session["LoginUserInfo"] = null;//清空一个session
                    Session.Clear();//清空所有session
                }
                if (Request.HttpMethod == "POST")
                {
                    //   string pwd = Common.CommonClass.MD5String(Request.Params["passwordhash"]);
                    Dictionary<string, object> dic = Common.CommonClass.Select(@"SELECT [UId] ,[UserName],[LoginName],[UCreateTime],[UUpdateTime],[UPhone],[UEmail],[UDepartId],[UDelete],[Upwd]
                                        FROM[DB_MgooERP].[dbo].[Users] where LoginName='" + Request.Params["LoginName"] + "' and Upwd='" + Request.Params["passwordhash"] + "' and UDelete=0");
                    if (dic==null)
                    {
                        Response.Write("<script language=javascript> alert('用户名或密码错误');</script> ");                
                        return;
                    }
                    else
                    {     
                        Model.Users LoginUserInfo = new Model.Users();
                        LoginUserInfo.U_UserName = dic["UserName"].ToString();//用户名
                        LoginUserInfo.U_DepartId = Convert.ToInt32(dic["UDepartId"].ToString());//部门ID
                        LoginUserInfo.U_UId = Convert.ToInt32(dic["UId"].ToString());//用户ID
                        LoginUserInfo.U_LoginName = dic["LoginName"].ToString();//登陆名
                        Session["LoginUserInfo"] = LoginUserInfo;
                        Utils.SetSession(LoginUserInfo);
                        //Session["name"] = dic["LoginName"];
                        Response.Redirect("Index.aspx", false);
                    }                  
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
    }
}