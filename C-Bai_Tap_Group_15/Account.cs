using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Account
    {
        public string name { set; get; }
        public string password { set; get; }
        public Account() { }
        public Account(string name, string password)
        {
            this.password = password;
            this.name = name;
        }
    }
}
