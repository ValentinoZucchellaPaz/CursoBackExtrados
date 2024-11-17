using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public class Alfil : Pieza
    {
        public override char InicialPieza => 'B';

        public override bool EsValido(int col, int[] tablero)
        {
            for (int i = 0; i < col; i++)
            {
                // verifico que no haya piezas en las diagonales
                if (Math.Abs(tablero[i] - tablero[col]) == Math.Abs(i - col))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
