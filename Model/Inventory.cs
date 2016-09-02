using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Inventory
    {
        private int i_InventoryId;
        private int i_Mid;
        private int i_inventoryDelete;
        private int i_invertorySum;

        public int I_InventoryId
        {
            get
            {
                return i_InventoryId;
            }

            set
            {
                i_InventoryId = value;
            }
        }

        public int I_Mid
        {
            get
            {
                return i_Mid;
            }

            set
            {
                i_Mid = value;
            }
        }

        public int I_inventoryDelete
        {
            get
            {
                return i_inventoryDelete;
            }

            set
            {
                i_inventoryDelete = value;
            }
        }

        public int I_invertorySum
        {
            get
            {
                return i_invertorySum;
            }

            set
            {
                i_invertorySum = value;
            }
        }
    }
}
