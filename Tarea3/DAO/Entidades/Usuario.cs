using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Mail { get; set; }
        public string? FechaBaja { get; set; }


        public Usuario(int id, string nombre, int edad, string mail, string fecha_baja)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Edad = edad;
            this.Mail = mail;
            this.FechaBaja = fecha_baja;
        }

        public override string ToString()
        {
            return $"id: {Id} - nombre: {Nombre} - edad: {Edad} - mail: {Mail} - fecha_baja: {FechaBaja ?? "null"} ";
        }

    }
}
