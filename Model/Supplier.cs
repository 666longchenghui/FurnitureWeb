using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
 public   class Supplier
    {
        private int _sid;
        private string _sCompanyName;
        private string _sContacts;
        private string _sAddress;
        private string _sPhone;
        private string _sEmail;
        private DateTime _sCreateTime;
        private DateTime _sUpdateTime;
        private string _sFax;
        private int _sDelete;
        private string _sRemark;
        private string _sSex;

        public int Sid
        {
            get
            {
                return _sid;
            }

            set
            {
                _sid = value;
            }
        }

        public string SCompanyName
        {
            get
            {
                return _sCompanyName;
            }

            set
            {
                _sCompanyName = value;
            }
        }

        public string SContacts
        {
            get
            {
                return _sContacts;
            }

            set
            {
                _sContacts = value;
            }
        }

        public string SAddress
        {
            get
            {
                return _sAddress;
            }

            set
            {
                _sAddress = value;
            }
        }

        public string SPhone
        {
            get
            {
                return _sPhone;
            }

            set
            {
                _sPhone = value;
            }
        }

        public string SEmail
        {
            get
            {
                return _sEmail;
            }

            set
            {
                _sEmail = value;
            }
        }

        public DateTime SCreateTime
        {
            get
            {
                return _sCreateTime;
            }

            set
            {
                _sCreateTime = value;
            }
        }

        public DateTime SUpdateTime
        {
            get
            {
                return _sUpdateTime;
            }

            set
            {
                _sUpdateTime = value;
            }
        }

        public string SFax
        {
            get
            {
                return _sFax;
            }

            set
            {
                _sFax = value;
            }
        }

        public int SDelete
        {
            get
            {
                return _sDelete;
            }

            set
            {
                _sDelete = value;
            }
        }

        public string SRemark
        {
            get
            {
                return _sRemark;
            }

            set
            {
                _sRemark = value;
            }
        }

        public string SSex
        {
            get
            {
                return _sSex;
            }

            set
            {
                _sSex = value;
            }
        }
    }
}
