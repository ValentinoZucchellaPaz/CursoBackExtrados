using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.Models
{
    public class User(int id, string name, int age, string mail, string password, string salt, string? unsub_date)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string Mail { get; set; } = mail;
        private string Password { get; set; } = password;
        private string Salt { get; set; } = salt;
        public string? UnsubDate { get; set; } = unsub_date;

        public string GetPass()
        {
            return Password;
        }
        public string GetSalt()
        {
            return Salt;
        }


        public override string ToString()
        {
            return $"id: {Id} - name: {Name} - age: {Age} - mail: {Mail} - unsub_date: {UnsubDate ?? "null"}";
        }
    }
}
