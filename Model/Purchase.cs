using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Purchase
    {
        private int p_purchaseID;
        private DateTime p_purchaseDate;
        private string p_purchaseNumber;
        private string p_purchaseDepart;
        private string p_purchaseMan;
        private string p_purchaseType;
        private string p_forwardingWay;
        private string p_purchaseNote;

        public int P_purchaseID
        {
            get
            {
                return p_purchaseID;
            }

            set
            {
                p_purchaseID = value;
            }
        }

        public DateTime P_purchaseDate
        {
            get
            {
                return p_purchaseDate;
            }

            set
            {
                p_purchaseDate = value;
            }
        }

        public string P_purchaseNumber
        {
            get
            {
                return p_purchaseNumber;
            }

            set
            {
                p_purchaseNumber = value;
            }
        }

        public string P_purchaseDepart
        {
            get
            {
                return p_purchaseDepart;
            }

            set
            {
                p_purchaseDepart = value;
            }
        }

        public string P_purchaseMan
        {
            get
            {
                return p_purchaseMan;
            }

            set
            {
                p_purchaseMan = value;
            }
        }

        public string P_purchaseType
        {
            get
            {
                return p_purchaseType;
            }

            set
            {
                p_purchaseType = value;
            }
        }

        public string P_forwardingWay
        {
            get
            {
                return p_forwardingWay;
            }

            set
            {
                p_forwardingWay = value;
            }
        }

        public string P_purchaseNote
        {
            get
            {
                return p_purchaseNote;
            }

            set
            {
                p_purchaseNote = value;
            }
        }
    }
}
