using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1
{
    public static class ActiveAttendancecategory
    {
        public static int Id { get; set; }
        public static string Name_type { get; set; }
        public static bool Present { get; set; }

        public static void Set(Attendancecategory attendancecategory)
        {
            ActiveAttendancecategory.Id = attendancecategory.Id;
        }
    }
}
