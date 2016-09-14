using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class JsonMerchandise
    {
        public int history_id { get; set; }
        //public string history_name { get; set; }
        //public string history_model { get; set; }
        //public string history_number { get; set; }
        //public string history_unit { get; set; }
        public int history_sum { get; set; }
        public decimal history_money { get; set; }    
        public string history_note { get; set; }
    }
}
