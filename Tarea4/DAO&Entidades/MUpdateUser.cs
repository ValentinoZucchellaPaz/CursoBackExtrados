using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades
{
    public class MUpdateUser(int id, string name, int age, string mail)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string Mail { get; set; } = mail;
    }
}
