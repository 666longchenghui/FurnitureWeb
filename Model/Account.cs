using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Account
    {
        private int a_AccountID;
        private string a_AccountNo;
        private string a_AccountName;
        private decimal a_AccountMoney;
        private string a_AccountComTel;
        private string a_AccountPhone;
        private string a_AccountEmail;
        private string a_AccountNote;
        private DateTime a_AccountCreate;
        private int a_AccountDelete;

        public int A_AccountID
        {
            get
            {
                return a_AccountID;
            }

            set
            {
                a_AccountID = value;
            }
        }

        public string A_AccountNo
        {
            get
            {
                return a_AccountNo;
            }

            set
            {
                a_AccountNo = value;
            }
        }

        public string A_AccountName
        {
            get
            {
                return a_AccountName;
            }

            set
            {
                a_AccountName = value;
            }
        }

        public decimal A_AccountMoney
        {
            get
            {
                return a_AccountMoney;
            }

            set
            {
                a_AccountMoney = value;
            }
        }

        public string A_AccountComTel
        {
            get
            {
                return a_AccountComTel;
            }

            set
            {
                a_AccountComTel = value;
            }
        }

        public string A_AccountPhone
        {
            get
            {
                return a_AccountPhone;
            }

            set
            {
                a_AccountPhone = value;
            }
        }

        public string A_AccountEmail
        {
            get
            {
                return a_AccountEmail;
            }

            set
            {
                a_AccountEmail = value;
            }
        }

        public string A_AccountNote
        {
            get
            {
                return a_AccountNote;
            }

            set
            {
                a_AccountNote = value;
            }
        }

        public DateTime A_AccountCreate
        {
            get
            {
                return a_AccountCreate;
            }

            set
            {
                a_AccountCreate = value;
            }
        }

        public int A_AccountDelete
        {
            get
            {
                return a_AccountDelete;
            }

            set
            {
                a_AccountDelete = value;
            }
        }
    }
}
