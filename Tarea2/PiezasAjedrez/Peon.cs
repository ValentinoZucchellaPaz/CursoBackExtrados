namespace PiezasAjedrez
{
    public class Peon: Pieza
    {
        public override char InicialPieza => 'P';

        public override bool EsValido(int col, int[] tablero)
        {
            // si esta en una diagonal inmediata no es valido
            for (int i = 0; i < col; i++)
            {
                if (Math.Abs(col - i) == 1 && Math.Abs(tablero[col] - tablero[i]) == 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
