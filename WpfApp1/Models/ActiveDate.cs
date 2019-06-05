using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class ActiveDate
    {
        public static int Id { get; set; }
        public static string Day { get; set; }
        public static int Week { get; set; }
        public static DateTime? Timestart { get; set; }
        public static DateTime? Timefinish { get; set; }

        public static void Setactivatedate(Date date)
        {
            ActiveDate.Id = date.Id;
            ActiveDate.Day = date.Day;
            ActiveDate.Week = date.Week;
            ActiveDate.Timestart = date.Timestart;
            ActiveDate.Timefinish = date.Timefinish;
        }
    }
}
