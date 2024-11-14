using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public class Caballo : Pieza
    {
        public Caballo(int tamaño_de_tablero = 8, char inicialPieza = 'H') : base(tamaño_de_tablero, inicialPieza)
        {
        }

        public override bool esValido(int col)
        {
            for (int i = 0; i < col; i++)
            {
                //chequea si el caballo puede comer hacia abajo izq o der
                if (Math.Abs(tablero[i] - tablero[col]) == Math.Abs(i - col))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
