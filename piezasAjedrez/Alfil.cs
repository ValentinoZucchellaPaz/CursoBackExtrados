using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public class Alfil : Pieza
    {
        public Alfil(int tamaño_de_tablero = 8, char inicialPieza = 'B') : base(tamaño_de_tablero, inicialPieza)
        {
        }

        public override bool esValido(int col)
        {
            for (int i = 0; i < col; i++)
            {
                //chequea si el alfil esta en una diagonal
                if (Math.Abs(tablero[i] - tablero[col]) == Math.Abs(i - col))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
