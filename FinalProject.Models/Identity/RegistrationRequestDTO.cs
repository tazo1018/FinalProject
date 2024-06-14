namespace FinalProject.Models.Identity
{
    public class RegistrationRequestDTO
    {
        //identity useris parametrebs unda emtxveodnen eseni imitiro mere bazashi unda gadavmapo aspnetUser shi
        public string Password {  get; set; }
        public string Email { get; set; }
        public string PhoneNUmber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
