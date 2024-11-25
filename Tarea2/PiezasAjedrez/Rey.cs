using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiezasAjedrez
{
    public class Rey: Pieza
    {
        public override char InicialPieza => 'K';

        public override bool EsValido(int col, int[] tablero)
        {
            // si esta en una casilla inmediata no es valido
            for (int i = 0; i < col; i++)
            {
                if (Math.Abs(col - i) <= 1 || Math.Abs(tablero[col] - tablero[i]) <= 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
