using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public static class ActiveAttendance
    {
        public static int Id { get; set; }

        public static int GetActiveAttendance
        {
            get
            {
                return Id;

            }
        }
        public static void Setactiveattendance(Attendance attendance)
        {
            ActiveAttendance.Id = attendance.Id;

        }
    }
    
}
