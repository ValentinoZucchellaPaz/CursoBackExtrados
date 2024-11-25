namespace DAO_Entidades
{
    public class User(int id, string name, int age, string mail, string? unsub_date)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string Mail { get; set; } = mail;
        public string? UnsubDate { get; set; } = unsub_date;

        public override string ToString()
        {
            return $"id: {Id} - name: {Name} - age: {Age} - mail: {Mail} - unsub_date: {UnsubDate ?? "null"}";
        }
    }
}
