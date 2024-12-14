namespace DAO_Entidades.Entities
{
    public class Book(int id, string name, DateTime? rent_date, DateTime? expiration_date, int user_id)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public DateTime? RentDate { get; set; } = rent_date;
        public DateTime? ExpirationDate { get; set; } = expiration_date;
        public int userId { get; set; } = user_id;
    }
}
