using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Child
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool LeaveAlone { get; set; }

        public string Fullinformation
        {
            get
            {

                return $"{Firstname} {Lastname}";

            }
        }
    }
}
