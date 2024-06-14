using FinalProject.Models.Identity;

namespace FinalProject.Contracts
{
    public interface IAuthService
    {
        Task Register(RegistrationRequestDTO registrationRequestDTO);
        Task RegisterAdmin(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
