using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTest
{
    public class Employees
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public Employees(string first_name, string last_name, string email)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
        }
    }

}
