
namespace Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email) : base($"Invalid email: {email}")
        {
        }
    }
}
