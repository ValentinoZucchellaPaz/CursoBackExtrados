using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1
{
    public class Reina
    {
        public int[] tablero { get; set; }
        private bool encontrado;

        public Reina(int tamaño_de_tablero = 8, char inicialPieza = 'P')
        {
            tablero = new int[tamaño_de_tablero];
            encontrado = false;

            for (int i = 0; i < tablero.Length; i++)
            {
                tablero[i] = -1;
            }
        }
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
        public bool esValido(int col)
        {
            for (int i = 0; i < col; i++)
            {
                //chequea si la reina esta en la misma fila o en una diagonal
                if (tablero[i] == tablero[col] || Math.Abs(tablero[i] - tablero[col]) == Math.Abs(i - col))
                {
                    return false;
                }
            }
            return true;
        }
        public void imprimirTablero()
        {
            Console.WriteLine("Array de solución \n(el indice es la columna \ny el valor es la fila): ");
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
