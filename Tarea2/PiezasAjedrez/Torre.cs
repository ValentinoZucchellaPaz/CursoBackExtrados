using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiezasAjedrez
{
    public class Torre : Pieza
    {
        public override char InicialPieza => 'T';
        public override bool EsValido(int col, int[] tablero)
        {
            //no debe estar en la misma col (en la misma fila no se puede x naturaleza de tablero)
            for (int i = 0; i < col; i++)
            {
                if (tablero[i] == tablero[col])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
