﻿using DAO_Entidades.Entities;

namespace DAO_Entidades.DAO.DAOUser
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
    }
}
