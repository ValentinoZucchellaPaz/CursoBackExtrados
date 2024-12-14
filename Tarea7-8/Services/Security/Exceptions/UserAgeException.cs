namespace Services.Security.Exceptions
{
    public class UserAgeException : Exception
    {
        public UserAgeException() : base("El usuario no tiene edad suficiente para registrarse") { }

        public UserAgeException(string message) : base(message) { }

        public UserAgeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
