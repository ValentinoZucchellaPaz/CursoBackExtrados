using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public class Caballo : Pieza
    {
        public override char InicialPieza => 'H';
        public override bool EsValido(int col, int[] tablero)
        {
            //caballo come dos hacia un sentido uno para el otro
            //caso1: dos hacia los costados uno hacia arriba
            //caso2: dos hacia arriba uno hacia los costados
            for (int i = 0; i < col; i++)
            {
                int dif_x = Math.Abs(col - i);
                int dif_y = Math.Abs(tablero[col] - tablero[i]);
                if ((dif_x == 2 && dif_y == 1) || (dif_x == 1 && dif_y == 2))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
