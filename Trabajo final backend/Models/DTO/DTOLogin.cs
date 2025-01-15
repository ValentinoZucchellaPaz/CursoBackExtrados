using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DTOLogin (string email, string contraseña)
    {
        public string Email { get; set; } = email;
        public string Contraseña { get; set; } = contraseña;
    }
}
