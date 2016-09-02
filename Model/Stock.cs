using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Stock
    {
        private int Stockid;
        private int PurchaseID;
        private DateTime PurchaseDate;
        private int SId;
        private decimal Total;

        public int Stockid1
        {
            get
            {
                return Stockid;
            }

            set
            {
                Stockid = value;
            }
        }

        public int PurchaseID1
        {
            get
            {
                return PurchaseID;
            }

            set
            {
                PurchaseID = value;
            }
        }

        public DateTime PurchaseDate1
        {
            get
            {
                return PurchaseDate;
            }

            set
            {
                PurchaseDate = value;
            }
        }

        public int SId1
        {
            get
            {
                return SId;
            }

            set
            {
                SId = value;
            }
        }

        public decimal Total1
        {
            get
            {
                return Total;
            }

            set
            {
                Total = value;
            }
        }
    }
}
