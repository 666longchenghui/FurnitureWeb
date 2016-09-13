using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class Output
    {
        private int o_Outid;
        private DateTime o_OutDate;
        private string o_OutNumber;
        private int o_AccountId;
        private int o_Mid;
        private int o_OutSum;
        private decimal o_OutTotalMoney;
        private decimal o_RealPrice;

        public int O_Outid
        {
            get
            {
                return o_Outid;
            }

            set
            {
                o_Outid = value;
            }
        }

        public DateTime O_OutDate
        {
            get
            {
                return o_OutDate;
            }

            set
            {
                o_OutDate = value;
            }
        }

        public string O_OutNumber
        {
            get
            {
                return o_OutNumber;
            }

            set
            {
                o_OutNumber = value;
            }
        }

        public int O_AccountId
        {
            get
            {
                return o_AccountId;
            }

            set
            {
                o_AccountId = value;
            }
        }

        public int O_Mid
        {
            get
            {
                return o_Mid;
            }

            set
            {
                o_Mid = value;
            }
        }

        public int O_OutSum
        {
            get
            {
                return o_OutSum;
            }

            set
            {
                o_OutSum = value;
            }
        }

        public decimal O_OutTotalMoney
        {
            get
            {
                return o_OutTotalMoney;
            }

            set
            {
                o_OutTotalMoney = value;
            }
        }

        public decimal O_RealPrice
        {
            get
            {
                return o_RealPrice;
            }

            set
            {
                o_RealPrice = value;
            }
        }
    }
}
