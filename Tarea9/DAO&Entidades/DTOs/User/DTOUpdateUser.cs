namespace DAO_Entidades.DTOs.User
{
    public class DTOUpdateUser(string name, int age)
    {
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
    }
}
