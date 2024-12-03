namespace Domain.Order.Requests
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSeller { get; set; } // Flag indicando se é vendedor ou comprador
        public string PostalCodeAddress { get; set; }
        public int NumberAddress { get; set; }
    }
}
