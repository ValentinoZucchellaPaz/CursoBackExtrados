using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.DTOs.User
{
    public class DTOUserClaims (int sid, string mail, string role)
    {
        public int Sid = sid;
        public string Mail = mail;
        public string Role = role;
    }
}
