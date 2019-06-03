using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Activestaff.Id = staff.Id;
            Activestaff.Firstname = staff.Firstname;
            Activestaff.Lastname = staff.Lastname;
            Activestaff.Email = staff.Email;
            Activestaff.Class = staff.Class;
            



            //blir fel måste fixa ett sätt att fixa detta på
            if (staff.Department == 1 )
            {
                Activestaff.Department = "Årskurs 1";
            }
            else if (staff.Department == 2)
            {
                Activestaff.Department = "Årskurs 2";
            }
            else if (staff.Department == 3)
            {
                Activestaff.Department = "Fritids";
            }
            else if (staff.Department == 4)
            {
                Activestaff.Department = "Årskurs 3";
            }
        }
    }
}