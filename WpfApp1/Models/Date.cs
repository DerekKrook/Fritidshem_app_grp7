using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Date
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public int Week { get; set; }
        public DateTime Timestart { get; set; }
        public DateTime Timefinish { get; set; }

        public string InformationDay
        {
            get
            {
                return $"{Day}";
            }
        }
    }
}
