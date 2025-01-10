using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.Entidades
{
    public class Serie (int id, string nombre, DateTime fecha_salida)
    {
        public required int Id { get; set; } = id;
        public required string Nombre { get; set; } = nombre;
        public required DateOnly FechaSalida {  get; set; } = DateOnly.FromDateTime(fecha_salida);
    }
}
