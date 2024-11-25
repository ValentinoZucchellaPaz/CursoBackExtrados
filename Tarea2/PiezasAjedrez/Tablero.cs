using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PiezasAjedrez
{
    public class Tablero
    {
        private int N;
        private int[] tablero;
        private bool encontrado = false;
        private Pieza pieza;

        public Tablero(int tamaño, Pieza tipoPieza)
        {
            N = tamaño;
            tablero = Enumerable.Repeat(-1, N).ToArray();
            pieza = tipoPieza;

        }

        public void ColocarPieza(int columna=0)
        {
            if (encontrado) return;

            // Si todas las columnas tienen una pieza colocada, hemos encontrado una solución
            if (columna==N)
            {
                encontrado = true;
                ImprimirTablero();
                return;
            }


            for (int fila = 0; fila < N; fila++)
            {
                tablero[columna] = fila;

                if (pieza.EsValido(columna, tablero))
                {
                    ColocarPieza(columna + 1);
                    if (encontrado) return;
                }

                tablero[columna] = -1;
            }
        }


        public void ImprimirTablero()
        {
            //Console.WriteLine("Array de solución (columna: fila): ");
            //for (int i = 0; i < N; i++)
            //{
            //    Console.WriteLine($"Columna {i}: Fila {tablero[i]}");
            //}

            Console.WriteLine("\nTablero visual:");

            for (int fila = 0; fila < N; fila++)
            {
                for (int columna = 0; columna < N; columna++)
                {
                    if (tablero[columna] == fila)
                        Console.Write(pieza.InicialPieza + " ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
