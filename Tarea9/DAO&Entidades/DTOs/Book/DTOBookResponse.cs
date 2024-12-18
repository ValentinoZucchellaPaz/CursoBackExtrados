using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO_Entidades.DTOs.Book
{
    public class DTOBookResponse(string name, DateTime? rent_date, DateTime? expiration_date, int? user_id)
    {
        public string Name { get; set; } = name;
        public DateTime? RentDate { get; set; } = rent_date;
        public DateTime? ExpirationDate { get; set; } = expiration_date;
        public int? UserId { get; set; } = user_id;
    }
}
