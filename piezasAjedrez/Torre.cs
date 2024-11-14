using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public class Torre : Pieza
    {
        public Torre(int tamaño_de_tablero = 8, char inicialPieza = 'T') : base(tamaño_de_tablero, inicialPieza)
        {
        }

        public override bool esValido(int col)
        {
            for (int i = 0; i < col; i++)
            {
                //chequea si la torre esta en la misma fila o columna
                if (tablero[i] == tablero[col])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
