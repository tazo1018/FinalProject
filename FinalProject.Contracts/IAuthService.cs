using FinalProject.Models.Identity;

namespace FinalProject.Contracts
{
    public interface IAuthService
    {
        // aq mgoni sheeshala
        Task Register(RegistrationRequestDTO registrationRequestDTO);
        Task RegisterAdmin(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
