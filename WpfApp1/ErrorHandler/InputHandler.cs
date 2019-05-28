using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ErrorHandler
{
    class InputHandler
    {
        public string Uppercase(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Sökfält är tomt");

            char[] a = input.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
