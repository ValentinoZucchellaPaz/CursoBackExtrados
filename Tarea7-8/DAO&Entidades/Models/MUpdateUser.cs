namespace DAO_Entidades.Models
{
    public class MUpdateUser(string name, int age)
    {
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
    }
}
