using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public class Class
    {
        private int _cId;
        private string _cClassName;
        private DateTime _cCreateTime;
        private DateTime _cUpdateTime;

        public int CId
        {
            get
            {
                return _cId;
            }

            set
            {
                _cId = value;
            }
        }

        public string CClassName
        {
            get
            {
                return _cClassName;
            }

            set
            {
                _cClassName = value;
            }
        }

        public DateTime CCreateTime
        {
            get
            {
                return _cCreateTime;
            }

            set
            {
                _cCreateTime = value;
            }
        }

        public DateTime CUpdateTime
        {
            get
            {
                return _cUpdateTime;
            }

            set
            {
                _cUpdateTime = value;
            }
        }
    }
}
