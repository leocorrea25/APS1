namespace Application.User.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsSeller { get; set; } 
        public UpdateUserAddressRequest? Address { get; set; } 
    }


    public class UpdateUserAddressRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public int Number { get; set; }
    }
}
