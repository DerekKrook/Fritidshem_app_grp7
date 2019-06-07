using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WpfApp1
{
    public static class Activestaff
    {
        
        public static int Id { get; set; }
        public static string Firstname { get; set; }
        public static string Lastname { get; set; }     
        public static string Email { get; set; }
        public static string Department { get; set; }
        public static string Class { get; set; }

        public static string Getactivestaff
        {
            get
            {
                //Istället för override tostring skapar vi en property som endast hämtar information
                return $"{Firstname} {Lastname} {Department}";

            }
        }

        public static void Setactivestaff(Staff staff)
        {
            if (staff != null)
            {
                Activestaff.Id = staff.Id;
                Activestaff.Firstname = staff.Firstname;
                Activestaff.Lastname = staff.Lastname;
                Activestaff.Email = staff.Email;
                Activestaff.Department = staff.Department;
            }
                 
        }
    }
}