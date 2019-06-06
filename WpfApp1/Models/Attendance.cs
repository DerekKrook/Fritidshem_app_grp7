using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Attendance
    {
        public int Id { get; set; }
        public string Category_attendance { get; set; }
        public string Comment { get; set; }
        public string Child { get; set; }
        public string Guardian { get; set; }
        public string Day { get; set; }
        public string LeaveAlone { get; set; }
        public string Week { get; set; }
        public string HomeAlone { get; set; }
        //public string Fullinformation
        //{
        //    get
        //    {

        //        return $"{Child} {Guardian} {Day} {HomeAlone} {Comment}";

        //    }
        //}


        public void UpdateLeaveAlone()
        {
            if (LeaveAlone == "True")
            {
                LeaveAlone = "Ja";
            }
            else if (true)
            {
                LeaveAlone = "Nej";
            }
            
        }

        
    }
}
