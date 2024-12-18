using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.Entities
{
    public class RefreshToken(int id, int user_id, string token, DateTime expiration_date, bool is_revoked)
    {
        public int Id { get; set; } = id;
        public int UserId { get; set; } = user_id;
        public string Token { get; set; } = token;
        public DateTime ExpirationDate { get; set; } = expiration_date;
        public bool IsRevoked = is_revoked;
    }
}
