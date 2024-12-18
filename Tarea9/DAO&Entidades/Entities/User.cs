namespace DAO_Entidades.Entities
{
    public class User(int id, string name, int age, string mail, string password, string salt, DateTime? unsub_date, string role)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string Mail { get; set; } = mail;
        public string Password { get; set; } = password;
        public string Salt { get; set; } = salt;
        public DateTime? UnsubDate { get; set; } = unsub_date;
        public string Role { get; set; } = role;

        public override string ToString()
        {
            return $"id: {Id} - name: {Name} - age: {Age} - mail: {Mail} - unsub_date: {UnsubDate}";
        }
    }
}
