using DAO_Entidades.DTOs.User;
using DAO_Entidades.Entities;

namespace Services.UserService
{
    public interface IUserService
    {
        public Task<DTOUser?> Authenticate(string mail, string password);
        public Task<List<DTOUser>> GetUsers();
        public Task<DTOUser?> GetUser(int id);
        public Task<int> CreateUser(DTOCreateUser user);
        public Task<int> UpdateUser(DTOUpdateUser user);
        public Task<bool> DeleteUser(DTOId userId);
        public DTOUserClaims GetUserClaims();
    }
}
