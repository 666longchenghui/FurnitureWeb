using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class PurchaseProperty
    {
        private int p_PurchasePropertyid;
        private int p_mid;
        private int p_Superid;
        private string p_PurchasePropertynumber;
        private int p_PurchasePropertyDelete;
        private int p_PurchasePropertyNote;

        public int P_PurchasePropertyid
        {
            get
            {
                return p_PurchasePropertyid;
            }

            set
            {
                p_PurchasePropertyid = value;
            }
        }

        public int P_mid
        {
            get
            {
                return p_mid;
            }

            set
            {
                p_mid = value;
            }
        }

        public int P_Superid
        {
            get
            {
                return p_Superid;
            }

            set
            {
                p_Superid = value;
            }
        }

        public string P_PurchasePropertynumber
        {
            get
            {
                return p_PurchasePropertynumber;
            }

            set
            {
                p_PurchasePropertynumber = value;
            }
        }

        public int P_PurchasePropertyDelete
        {
            get
            {
                return p_PurchasePropertyDelete;
            }

            set
            {
                p_PurchasePropertyDelete = value;
            }
        }

        public int P_PurchasePropertyNote
        {
            get
            {
                return p_PurchasePropertyNote;
            }

            set
            {
                p_PurchasePropertyNote = value;
            }
        }
    }
}
