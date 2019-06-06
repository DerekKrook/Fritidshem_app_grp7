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
        public static int Age { get; set; }
        public static string Class { get; set; }
        public static string Guardian { get; set; }
        public static int Mealsid { get; set; }

        public static string Getactivechild
        {
            get
            {
                return $"{Firstname} {Lastname}";

            }
        }

        public static void Setactivechild(Child child)
        {
            if (child != null)
            {
                Activechild.Id = child.Id;
                Activechild.Firstname = child.Firstname;
                Activechild.Lastname = child.Lastname;
                Activechild.LeaveAlone = child.LeaveAlone;
                Activechild.Age = child.Age;
                Activechild.Class = child.Class;
                Activechild.Guardian = child.Guardian;
                Activechild.Mealsid = child.Id;
            }
        }
    }
}