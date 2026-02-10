namespace Montcrest.API.DTOs.Auth
{
    public class RegisterRequestDto
    {
       
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }
    }
}
