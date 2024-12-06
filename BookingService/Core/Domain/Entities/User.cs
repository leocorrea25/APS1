using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                if (!Utils.ValidateEmail(value))
                    throw new InvalidEmailException(value);
                email = value;
            }
        }
        public string Password { get; set; }
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber; set
            {
                phoneNumber = value;
            }
        }
        public bool IsSeller { get; set; } // Flag indicando se é vendedor ou comprador
        public int AddressId { get; set; } // FK para Address
        public Address Address { get; set; } // Endereço do usuário
        public string Token { get; set; }
    }


    public static class Utils
    {
        private static string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        private static string PhoneRegex = @"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$";

        /// <summary>
        /// Validates a email address using a specific regex pattern.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True if the email address matches the regex pattern; otherwise, false.</returns>
        public static bool ValidateEmail(string email)
        {
            // testei essa budega com "a@a" e retornou true, então não é uma validação muito boa
            // var addr = new System.Net.Mail.MailAddress(email);
            return Regex.IsMatch(email, EmailRegex);
        }

        /// <summary>
        /// Validates a phone number using a specific regex pattern.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>True if the phone number matches the regex pattern; otherwise, false.</returns>
        public static bool ValidatePhone(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, PhoneRegex);
        }
    }
}
