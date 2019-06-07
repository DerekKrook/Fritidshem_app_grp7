using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class Activeguardian
    {
        public static int Id { get; set; }
        public static string Firstname { get; set; }
        public static string Lastname { get; set; }
        public static string Email { get; set; }
        public static string Phone { get; set; }

        public static string Getactiveguardian
        {
            get
            {
                //Istället för override tostring skapar vi en property som endast hämtar information
                return $"{Firstname} {Lastname} ({Email}) {Phone.ToString()}";

            }
        }

        public static void Setactiveguardian(Guardian guardian)
        {
            if (guardian != null)
            {
                Activeguardian.Id = guardian.Id;
                Activeguardian.Firstname = guardian.Firstname;
                Activeguardian.Lastname = guardian.Lastname;
                Activeguardian.Email = guardian.Email;
                Activeguardian.Phone = guardian.Phone.ToString();
            }
        }
    }
}
