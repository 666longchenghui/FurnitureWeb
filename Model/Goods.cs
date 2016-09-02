using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Goods
    {
        private int _gId;
        private string _gName;
        private int _gClass;
        private decimal _gPrice;   
        private string _gUnit;
        private int _gStock;
        private int _gNumber;
        private int _gSupplierId;
        private decimal _gBid;
        private int _gLimit;
        private int _gLower;
        private int _gSalePrice;
        private DateTime _gCreateTime;
        private DateTime _gUpdateTime;

        public int GId
        {
            get
            {
                return _gId;
            }

            set
            {
                _gId = value;
            }
        }

        public string GName
        {
            get
            {
                return _gName;
            }

            set
            {
                _gName = value;
            }
        }

        public int GClass
        {
            get
            {
                return _gClass;
            }

            set
            {
                _gClass = value;
            }
        }

        public decimal GPrice
        {
            get
            {
                return _gPrice;
            }

            set
            {
                _gPrice = value;
            }
        }

        public string GUnit
        {
            get
            {
                return _gUnit;
            }

            set
            {
                _gUnit = value;
            }
        }

        public int GStock
        {
            get
            {
                return _gStock;
            }

            set
            {
                _gStock = value;
            }
        }

        public int GNumber
        {
            get
            {
                return _gNumber;
            }

            set
            {
                _gNumber = value;
            }
        }

        public int GSupplierId
        {
            get
            {
                return _gSupplierId;
            }

            set
            {
                _gSupplierId = value;
            }
        }

        public decimal GBid
        {
            get
            {
                return _gBid;
            }

            set
            {
                _gBid = value;
            }
        }

        public int GLimit
        {
            get
            {
                return _gLimit;
            }

            set
            {
                _gLimit = value;
            }
        }

        public int GLower
        {
            get
            {
                return _gLower;
            }

            set
            {
                _gLower = value;
            }
        }

        public int GSalePrice
        {
            get
            {
                return _gSalePrice;
            }

            set
            {
                _gSalePrice = value;
            }
        }

        public DateTime GCreateTime
        {
            get
            {
                return _gCreateTime;
            }

            set
            {
                _gCreateTime = value;
            }
        }

        public DateTime GUpdateTime
        {
            get
            {
                return _gUpdateTime;
            }

            set
            {
                _gUpdateTime = value;
            }
        }
    }
}
