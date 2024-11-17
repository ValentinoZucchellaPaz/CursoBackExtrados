using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piezasAjedrez
{
    public abstract class Pieza
    {
        public abstract char InicialPieza { get; }
        public abstract bool EsValido(int col, int[] tablero);
    }
}
