using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.DTOs.User
{
    public class DTOUser(int id, string name, int age, string mail, DateTime? unsub_date, string role)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string Mail { get; set; } = mail;
        public DateTime? UnsubDate { get; set; } = unsub_date;
        public string Role { get; set; } = role;
    }
}
