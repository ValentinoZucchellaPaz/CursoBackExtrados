using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.Entidades
{
    public class Generacion (int id, string nombre, string fecha_de_salida)
    {
        public required int Id { get; set; } = id;
        public required string Nombre { get; set; } = nombre;
        public required string FechaDeSalida {  get; set; } = fecha_de_salida;
    }
}
