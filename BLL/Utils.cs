using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Utils
    {
        public static string GetResult(int StatusCode,string message, string closeCurrent="false")
        {
            return "{\"statusCode\":\"" + StatusCode + "\",\"message\":\"" + message + "\",\"tabid\":\"\",	\"closeCurrent\":" + closeCurrent + ",\"forward\":\"\", \"forwardConfirm\":\"\"}";
        }
 
        private  static System.Web.SessionState.HttpSessionState sess = HttpContext.Current.Session;
        public static Model.Users GetSession()
        {  
           return  sess["LoginUserInfo"] as Model.Users;
        }
        public static void SetSession(Model.Users user)
        {
            sess["LoginUserInfo"] = user;
        }
        public static bool IsLogin() {
            if (sess["LoginUserInfo"]==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
