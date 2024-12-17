namespace Services.Security.Exceptions
{
    public class InvalidRoleException : Exception
    {
        public InvalidRoleException() : base("El correo no es válido. Solo se aceptan correos de Gmail.")
        {
        }

        public InvalidRoleException(string message) : base(message)
        {
        }

        public InvalidRoleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
