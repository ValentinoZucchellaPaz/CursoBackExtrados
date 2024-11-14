namespace piezasAjedrez
{
    public class Pieza
    {

        public int[] tablero { get; set; }
        public char inicialPieza;
        private bool encontrado;

        public Pieza(int tamaño_de_tablero=8, char inicialPieza='P')
        {
            tablero = new int[tamaño_de_tablero];
            encontrado = false;

            for (int i = 0; i < tablero.Length; i++)
            {
                tablero[i] = -1;
            }

            this.inicialPieza = inicialPieza;
        }

        public virtual bool esValido(int col)
        {
            return true;
        }

        //funcion recursiva
        public void colocarPieza(int k = 0)
        {
            //si ya encontro una sol, que salga de la recursividad
            if (encontrado) return;

            //sea k el nivel de arbol
            //salida de recursividad: se llegó al ultimo nivel del arbol
            if (k == tablero.Length)
            {
                encontrado = true;
                imprimirTablero();
                return;
            }
            //me muevo por las filas hacia abajo
            for (int fila = 0; fila < tablero.Length; fila++)
            {
                tablero[k] = fila;
                //si la pos es valida, entonces me muevo una col a la der y vuelvo a bajar por las filas de esa col
                if (esValido(k))
                {
                    colocarPieza(k + 1);
                    if (encontrado) return;
                }

                //si no es valido, se elimina la reina de la columna actual
                tablero[k] = -1;
            }
        }
        public void imprimirTablero()
        {
            Console.WriteLine("Array de solución (el indice es la columna y el valor es la fila en la que se encuentra la reina): ");
            Console.Write("[ ");
            for (int i = 0; i < tablero.Length; i++)
            {
                Console.Write(tablero[i] + " ");
            }
            Console.Write("]");

            // Imprimir el tablero visualmente según las posiciones del array `tablero`
            Console.WriteLine("\nTablero visual:");
            for (int fila = 0; fila < tablero.Length; fila++)
            {
                for (int columna = 0; columna < tablero.Length; columna++)
                {
                    if (tablero[columna] == fila)
                        Console.Write(inicialPieza + " ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
