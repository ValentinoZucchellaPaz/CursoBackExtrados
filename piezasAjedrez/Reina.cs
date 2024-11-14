using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public class Reina: Pieza
    {

        public Reina(int tamaño_de_tablero=8, char inicialPieza='Q') : base(tamaño_de_tablero, inicialPieza)
        {
        }

        public override bool esValido(int col)
        {
            for (int i = 0; i < col; i++)
            {
                //chequea si la reina esta en la misma fila o en una diagonal
                if (tablero[i] == tablero[col] || Math.Abs(tablero[i] - tablero[col]) == Math.Abs(i-col))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
