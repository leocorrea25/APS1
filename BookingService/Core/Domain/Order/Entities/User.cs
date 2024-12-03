namespace Domain.Order.Entities
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
                {
                    throw new ArgumentException("Email inválido.");
                }
                email = value;
            }
        }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSeller { get; set; } // Flag indicando se é vendedor ou comprador
        public int AddressId { get; set; } // FK para Address
        public Address Address { get; set; } // Endereço do usuário
        public string Token { get; set; }
    }


    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
