namespace API.Auxiliar.Exceptions
{
    public class RentBookException: Exception
    {
        public RentBookException() : base("El correo no es válido. Solo se aceptan correos de Gmail.")
        {
        }

        public RentBookException(string message) : base(message)
        {
        }

        public RentBookException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
