using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InHostory
    {
        private int r_RestockId;
        private int r_SId;
        private int r_Mid;
        private DateTime r_RCreateDate;
        private int r_RDelete;

        public int R_RestockId
        {
            get
            {
                return r_RestockId;
            }

            set
            {
                r_RestockId = value;
            }
        }

        public int R_SId
        {
            get
            {
                return r_SId;
            }

            set
            {
                r_SId = value;
            }
        }

        public int R_Mid
        {
            get
            {
                return r_Mid;
            }

            set
            {
                r_Mid = value;
            }
        }

        public DateTime R_RCreateDate
        {
            get
            {
                return r_RCreateDate;
            }

            set
            {
                r_RCreateDate = value;
            }
        }

        public int R_RDelete
        {
            get
            {
                return r_RDelete;
            }

            set
            {
                r_RDelete = value;
            }
        }
    }
}
