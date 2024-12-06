

namespace Domain.Exceptions
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string phoneNumber) : base($"Invalid phone number: {phoneNumber}, use the international format E.164")
        {
        }
    }
}
