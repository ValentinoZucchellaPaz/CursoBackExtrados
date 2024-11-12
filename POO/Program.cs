using Tarea1_8Reinas;

class Program
{
    static void Main(string[] args)
    {
        Tablero tablero = new Tablero();

        if (tablero.Resolver())
        {
            Console.WriteLine("Solución encontrada:");
            tablero.MostrarTablero();
        }
        else
        {
            Console.WriteLine("No se encontró una solución.");
        }
    }
}