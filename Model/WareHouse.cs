using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WareHouse
    {
       
            private int _depotid;
            private string _wareHouseNumber;
            private string _wareHouseName;
            private int _wareHouseStatus;
            private string _wareHouseAddress;
            private DateTime _wareHouseDate;
            private int _wDelete;
            private string _wHContacts;
            private string _wHPhone;

            public int Depotid
            {
                get
                {
                    return _depotid;
                }

                set
                {
                    _depotid = value;
                }
            }

            public string WareHouseNumber
            {
                get
                {
                    return _wareHouseNumber;
                }

                set
                {
                    _wareHouseNumber = value;
                }
            }

            public string WareHouseName
            {
                get
                {
                    return _wareHouseName;
                }

                set
                {
                    _wareHouseName = value;
                }
            }

            public int WareHouseStatus
            {
                get
                {
                    return _wareHouseStatus;
                }

                set
                {
                    _wareHouseStatus = value;
                }
            }

            public string WareHouseAddress
            {
                get
                {
                    return _wareHouseAddress;
                }

                set
                {
                    _wareHouseAddress = value;
                }
            }

            public DateTime WareHouseDate
            {
                get
                {
                    return _wareHouseDate;
                }

                set
                {
                    _wareHouseDate = value;
                }
            }

            public int WDelete
            {
                get
                {
                    return _wDelete;
                }

                set
                {
                    _wDelete = value;
                }
            }

            public string WHContacts
            {
                get
                {
                    return _wHContacts;
                }

                set
                {
                    _wHContacts = value;
                }
            }

            public string WHPhone
            {
                get
                {
                    return _wHPhone;
                }

                set
                {
                    _wHPhone = value;
                }
            }
        }
    }
