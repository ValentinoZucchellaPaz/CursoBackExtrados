using DAO_Entidades.Models;

namespace API.Services.UserService
{
    public interface IUserService
    {
        public User? Authenticate(string mail, string password);
        public List<User> GetUsers();
        public User? GetUser(int id);
        public int CreateUser(MCreateUser user);
        public int UpdateUser(MUpdateUser user);
        public bool DeleteUser(MId userId);
        public List<Book> GetBooks();
        public bool RentBook(string bookName);
    }
}
