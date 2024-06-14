namespace FinalProject.Models.Identity
{
    public class RegistrationRequestDTO
    {
        public string Password {  get; set; }
        public string Email { get; set; }
        public string PhoneNUmber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
