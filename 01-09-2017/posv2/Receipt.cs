using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posv2
{
    public class Receipt
    {
        public string id { get; set; }
        public string product { get; set; }

        public string size { get; set; }
        public int qty { get; set; }

        public double unit_price { get; set; }

        public double total { get { return (unit_price * qty); } }
    }





}