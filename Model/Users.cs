using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class Users
    {
        private int u_UId;
        private string u_UserName;
        private string u_LoginName;
        private DateTime u_CreateTime;
        private DateTime u_UpdateTime;
        private string u_UPhone;
        private string u_UEmail;     
        private int u_Delete;
        private string u_pwd;
        private int u_DepartId;
  
        public int U_UId
        {
            get
            {
                return u_UId;
            }

            set
            {
                u_UId = value;
            }
        }

        public string U_UserName
        {
            get
            {
                return u_UserName;
            }

            set
            {
                u_UserName = value;
            }
        }

        public string U_LoginName
        {
            get
            {
                return u_LoginName;
            }

            set
            {
                u_LoginName = value;
            }
        }

        public DateTime U_CreateTime
        {
            get
            {
                return u_CreateTime;
            }

            set
            {
                u_CreateTime = value;
            }
        }

        public DateTime U_UpdateTime
        {
            get
            {
                return u_UpdateTime;
            }

            set
            {
                u_UpdateTime = value;
            }
        }

        public string U_UPhone
        {
            get
            {
                return u_UPhone;
            }

            set
            {
                u_UPhone = value;
            }
        }

        public string U_UEmail
        {
            get
            {
                return u_UEmail;
            }

            set
            {
                u_UEmail = value;
            }
        }


        public int U_Delete
        {
            get
            {
                return u_Delete;
            }

            set
            {
                u_Delete = value;
            }
        }

        public string U_pwd
        {
            get
            {
                return u_pwd;
            }

            set
            {
                u_pwd = value;
            }
        }

        public int U_DepartId
        {
            get
            {
                return u_DepartId;
            }

            set
            {
                u_DepartId = value;
            }
        }
    }
}
