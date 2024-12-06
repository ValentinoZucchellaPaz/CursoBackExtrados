namespace API.Auxiliar.Exceptions
{
    public class InvalidMailException : Exception
    {
        public InvalidMailException() : base("El correo no es válido. Solo se aceptan correos de Gmail.")
        {
        }

        public InvalidMailException(string message) : base(message)
        {
        }

        public InvalidMailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
