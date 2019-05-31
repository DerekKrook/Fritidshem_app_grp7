using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{

    public static class Activechild
    {
        public static int Id { get; set; }
        public static string Firstname { get; set; }
        public static string Lastname { get; set; }
        public static bool LeaveAlone { get; set; }
        public static string Class { get; set; }

        public static string Getactivechild
        {
            get
            {
                return $"{Firstname} {Lastname}";

            }
        }

        public static string Setactivechild(Child child)
        {
            Activechild.Id = child.Id;
            Activechild.Firstname = child.Firstname;
            Activechild.Lastname = child.Lastname;
            Activechild.LeaveAlone = child.LeaveAlone;
            Activechild.Class = child.Class;

            return null;
        }
    }
}