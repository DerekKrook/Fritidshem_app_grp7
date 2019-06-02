using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1
{
    public class Attendancecategory
    {
        public int Id { get; set; }
        public string Name_type { get; set; }
        public bool Present { get; set; }

        public string Fullinformation
        {
            get
            {

                return $"{Name_type}";

            }
        }
    }
}
