using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public class DepartMent
    {
        private int d_DepartId;
        private string d_Department;
        private int d_Ddelete;
        private DateTime d_Dcreatedate;
        private DateTime d_DUpdatedate;
        private int d_DepartUser;

        public int D_DepartId
        {
            get
            {
                return d_DepartId;
            }

            set
            {
                d_DepartId = value;
            }
        }

        public string D_Department
        {
            get
            {
                return d_Department;
            }

            set
            {
                d_Department = value;
            }
        }

        public int D_Ddelete
        {
            get
            {
                return d_Ddelete;
            }

            set
            {
                d_Ddelete = value;
            }
        }

        public DateTime D_Dcreatedate
        {
            get
            {
                return d_Dcreatedate;
            }

            set
            {
                d_Dcreatedate = value;
            }
        }

        public DateTime D_DUpdatedate
        {
            get
            {
                return d_DUpdatedate;
            }

            set
            {
                d_DUpdatedate = value;
            }
        }

        public int D_DepartUser
        {
            get
            {
                return d_DepartUser;
            }

            set
            {
                d_DepartUser = value;
            }
        }
    }
}
