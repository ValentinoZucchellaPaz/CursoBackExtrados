using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.Models
{
    public class MCreateUser(string name, int age, string mail, string password, string role = "user")
    {
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string Mail { get; set; } = mail;
        public string Password { get; set; } = password;
        public string Role { get; set; } = role;
    }
}
