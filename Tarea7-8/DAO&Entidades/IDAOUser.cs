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
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User?> GetUser(int id);
        public Task<User?> GetUserByMail(string mail);
        public Task<int> CreateUser(string name, int age, string mail, string password, string salt, string role);
        public Task<bool> DeleteUser(int id, DateTime date);
        public Task<int> UpdateUser(int id, string name, int age);
        public Task<bool> IsMailInUse(string mail);
        public Task<IEnumerable<Book>> GetBooks();
        public Task<Book?> GetBook(string bookname);
        public Task<bool> RentBook(string bookName, DateTime rentDate, DateTime expirationDate, int userId);
    }
}
