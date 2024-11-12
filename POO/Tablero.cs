namespace Tarea1_8Reinas
{
    internal class Tablero
    {
        public int[,] Casillas { get; private set; }

        public Tablero()
        {
            Casillas = new int[8, 8];

            // Llenar el tablero con ceros explícitamente
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Casillas[i, j] = 0;
                }
            }
        }


        private bool EsPosicionSegura(int fila, int columna)
        {
            // Verificar columna hacia arriba
            for (int i = 0; i < fila; i++)
            {
                if (Casillas[i, columna] == 1)
                    return false;
            }

            // Verificar diagonal superior izquierda
            for (int i = fila, j = columna; i >= 0 && j >= 0; i--, j--)
            {
                if (Casillas[i, j] == 1)
                    return false;
            }

            // Verificar diagonal superior derecha
            for (int i = fila, j = columna; i >= 0 && j < 8; i--, j++)
            {
                if (Casillas[i, j] == 1)
                    return false;
            }

            // Si pasó todas las verificaciones, la posición es segura
            return true;
        }

        public bool Resolver(int fila = 0)
        {
            // salida de f recursiva
            if (fila >= 8)
                return true;

            // Intentar colocar una reina en cada columna de la fila actual
            for (int columna = 0; columna < 8; columna++)
            {
                if (EsPosicionSegura(fila, columna))
                {
                    // Coloca la reina en la posición segura
                    Casillas[fila, columna] = 1;

                    // Llama recursivamente para intentar colocar la siguiente reina en la siguiente fila
                    if (Resolver(fila + 1))
                        return true;

                    // Si colocar la reina en esta posición no lleva a una solución, retrocede
                    Casillas[fila, columna] = 0; // Quitar la reina (backtrack)
                }
            }

            // Si no se puede colocar ninguna reina en esta fila, retorna false para retroceder
            return false;
        }

        public int[,] GetCasillas()
        {
            return Casillas;
        }

        public void MostrarTablero()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(Casillas[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
