using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
   public class Staff
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public string Fullinformation
        {
            get
            {
                return $"{Firstname} {Lastname}";

            }
        }
    }
}
