using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1_8Reinas
{
    public class Reina
    {
        //el indice del array representa las columnas del tablero de ajedrez, el valor del indice representa la fila
        const int N = 8;
        public int[] tablero {  get; set; }
        private bool encontrado = false;

        public Reina() { 
            tablero = new int[N];

            for (int i = 0; i < N; i++)
            {
                tablero[i] = -1;
            }
        }

        private bool esValido(int col)
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


        public void colocarReina(int k=0)
        {
            //si ya encontro una sol, que salga de la recursividad
            if (encontrado) return;

            //sea k el nivel de arbol
            //salida de recursividad: se llegó al ultimo nivel del arbol
            if (k==N)
            {
                encontrado = true;
                imprimirTablero();
                return;
            }
            //me muevo por las filas hacia abajo
            for (int fila = 0; fila < N; fila++)
            {
                tablero[k] = fila;
                //si la pos es valida, entonces me muevo una col a la der y vuelvo a bajar por las filas de esa col
                if (esValido(k))
                {
                    colocarReina(k + 1);
                    if (encontrado) return;
                }

                //si no es valido, se elimina la reina de la columna actual
                //tablero[k] = -1;
            }

        }

        public void imprimirTablero()
        {
            Console.WriteLine("Array de solución (el indice es la columna y el valor es la fila en la que se encuentra la reina): ");
            Console.Write("[ ");
            for (int i = 0; i < N; i++)
            {
                Console.Write(tablero[i] + " ");
            }
            Console.Write("]");

            // Imprimir el tablero visualmente según las posiciones del array `tablero`
            Console.WriteLine("\nTablero visual:");
            for (int fila = 0; fila < N; fila++)
            {
                for (int columna = 0; columna < N; columna++)
                {
                    if (tablero[columna] == fila)
                        Console.Write("Q ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
