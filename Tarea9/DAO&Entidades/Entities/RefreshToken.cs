using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.Entities
{
    public class RefreshToken
    {
        public int? Id { get; set; }
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public required bool IsRevoked { get; set; }
    }
}
