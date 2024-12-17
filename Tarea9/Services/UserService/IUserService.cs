using DAO_Entidades.Entities;
using DAO_Entidades.Models;

namespace Services.UserService
{
    public interface IUserService
    {
        public Task<User?> Authenticate(string mail, string password);
        public Task<IEnumerable<User>> GetUsers();
        public Task<User?> GetUser(int id);
        public Task<int> CreateUser(MCreateUser user);
        public Task<int> UpdateUser(MUpdateUser user);
        public Task<bool> DeleteUser(MId userId);
    }
}
