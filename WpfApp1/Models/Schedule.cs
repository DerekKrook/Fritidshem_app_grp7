using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Classname { get; set; }
        public string Lecturename { get; set; }
        public string Day { get; set; }
        public DateTime Timestart { get; set; }
        public DateTime Timefinish { get; set; }       
        public string Time { get; set; }

        public string Fullinformation
        {
            get
            {

                return $"{Lecturename} {Day} {Time}";

            }
        }

        public void changeDateTime()
        {

            Time = Timestart.ToString("HH:mm");
            
        }
    }
}
