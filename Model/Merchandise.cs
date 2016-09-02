using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Merchandise
    {
        private int m_Mid;
        private string m_Mname;
        private string m_Mnumber;
        private int m_Unitid;
        private string m_Mspecification;
        private decimal m_Msellingprice;
        private string m_Mnote;
        private int m_MDelete;
        private int m_MSum;
        private DateTime m_MCreateDate;
        private DateTime m_MUpdateDate;
        private int Uid;

        public int Mid
        {
            get
            {
                return m_Mid;
            }

            set
            {
                m_Mid = value;
            }
        }

        public string Mname
        {
            get
            {
                return m_Mname;
            }

            set
            {
                m_Mname = value;
            }
        }

        public string Mnumber
        {
            get
            {
                return m_Mnumber;
            }

            set
            {
                m_Mnumber = value;
            }
        }

        public int Unitid
        {
            get
            {
                return m_Unitid;
            }

            set
            {
                m_Unitid = value;
            }
        }

        public string Mspecification
        {
            get
            {
                return m_Mspecification;
            }

            set
            {
                m_Mspecification = value;
            }
        }

        public decimal Msellingprice
        {
            get
            {
                return m_Msellingprice;
            }

            set
            {
                m_Msellingprice = value;
            }
        }

        public string Mnote
        {
            get
            {
                return m_Mnote;
            }

            set
            {
                m_Mnote = value;
            }
        }

        public int MDelete
        {
            get
            {
                return m_MDelete;
            }

            set
            {
                m_MDelete = value;
            }
        }

        public int MSum
        {
            get
            {
                return m_MSum;
            }

            set
            {
                m_MSum = value;
            }
        }

        public DateTime MCreateDate
        {
            get
            {
                return m_MCreateDate;
            }

            set
            {
                m_MCreateDate = value;
            }
        }

        public DateTime MUpdateDate
        {
            get
            {
                return m_MUpdateDate;
            }

            set
            {
                m_MUpdateDate = value;
            }
        }

        public int Uid1
        {
            get
            {
                return Uid;
            }

            set
            {
                Uid = value;
            }
        }
    }
}
