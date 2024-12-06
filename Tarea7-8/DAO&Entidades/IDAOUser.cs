using DAO_Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades
{
    public interface IDAOUser
    {
        public List<User> GetAllUsers();
        public User? GetUser(int id);
        public User? GetUserByMail(string mail);
        public int CreateUser(string name, int age, string mail, string password, string salt);
        public bool DeleteUser(int id, string date);
        public int UpdateUser(int id, string name, int age);
        public bool IsMailInUse(string mail);
    }
}
