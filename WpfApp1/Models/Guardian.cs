using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Guardian
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

        public string Fullinfo
        {
            get
            {
                //Istället för override tostring skapar vi en property som endast hämtar information
                return $"{Firstname} {Lastname} ({Email}) {Phone.ToString()}";
                    
            }
                
        }

       
    }
}
