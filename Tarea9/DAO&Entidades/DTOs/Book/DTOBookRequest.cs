namespace DAO_Entidades.DTOs.Book
{
    public class DTOBookRequest(string name)
    {
        public string Name { get; set; } = name;
    }
}
