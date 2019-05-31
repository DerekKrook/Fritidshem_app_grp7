using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public static class Activestaff
    {
        public static int Id { get; set; }
        public static string Firstname { get; set; }
        public static string Lastname { get; set; }
        public static int Department { get; set; }

        public static string Getactivestaff
        {
            get
            {
                //Istället för override tostring skapar vi en property som endast hämtar information
                return $"{Firstname} {Lastname} {Department}";

            }
        }

        public static string Setactivestaff(Staff staff)
        {
            Activestaff.Id = staff.Id;
            Activestaff.Firstname = staff.Firstname;
            Activestaff.Lastname = staff.Lastname;
            Activestaff.Department = staff.Department;

            return null;
        }
    }
}