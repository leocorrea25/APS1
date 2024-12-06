namespace Application.User.Request
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSeller { get; set; } // Flag indicando se � vendedor ou comprador
        public int PostalCodeAddress { get; set; }
        public int NumberAddress { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
