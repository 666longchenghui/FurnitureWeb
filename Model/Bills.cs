using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Bills
    {
        private int _billsID;
        private string _billsStatus;
        private string _billsName;

        private DateTime _bCreateDate;
 
        private string _billsNote;
        private int _delete;

        public int BillsID
        {
            get
            {
                return _billsID;
            }

            set
            {
                _billsID = value;
            }
        }

        public string BillsStatus
        {
            get
            {
                return _billsStatus;
            }

            set
            {
                _billsStatus = value;
            }
        }

        public string BillsName
        {
            get
            {
                return _billsName;
            }

            set
            {
                _billsName = value;
            }
        }


        public DateTime BCreateDate
        {
            get
            {
                return _bCreateDate;
            }

            set
            {
                _bCreateDate = value;
            }
        }

    

        public string BillsNote
        {
            get
            {
                return _billsNote;
            }

            set
            {
                _billsNote = value;
            }
        }

        public int Delete
        {
            get
            {
                return _delete;
            }

            set
            {
                _delete = value;
            }
        }
    }
}
