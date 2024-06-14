using AutoMapper;
using FinalProject.Contracts;
using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Models.Identity;
using FinalProject.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        private const string _adminRole = "administrator";
        private const string _customerRole = "user";


        public AuthService(ApplicationDbContext context,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtTokenGenerator, IHttpContextAccessor httpContextAccessor )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _httpContextAccessor = httpContextAccessor;
            _mapper = MappingInitializer.Initialize();
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var users = await _context.Users.ToListAsync();
            var user = await _context.Users.FirstOrDefaultAsync( x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            if (user.IsBlocked)
            {
                throw new Exception("Your user is blocked by Admin");
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    Token = string.Empty,
                    User = null
                };
            }

            // tu zemot rac miweria ar moxda, vagenerireb tokens

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles); //gaaketa tokeni

            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return new LoginResponseDTO()
            {
                User = userDto,
                Token = token
            };
        }

        public async Task Register(RegistrationRequestDTO registrationRequestDTO)
        {
            // identityuser unda shevkra amisgan
            User user = new()
            {
                UserName = registrationRequestDTO.Email,
                NormalizedUserName = registrationRequestDTO.Email.ToUpper(),
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                PhoneNumber = registrationRequestDTO.PhoneNUmber,
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName,
                IsBlocked = false
            };


            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    //unda movdzebno user
                    var userToReturn = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == registrationRequestDTO.Email.ToLower());
                    if (userToReturn != null)
                    {
                        if(!await _roleManager.RoleExistsAsync(_customerRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(_customerRole));
                        }
                        await _userManager.AddToRoleAsync(userToReturn, _customerRole);

                        UserDTO userDto = new()
                        {
                            Email = userToReturn.Email,
                            Id = userToReturn.Id,
                            PhoneNumber = userToReturn.PhoneNumber
                        };
                    }
                }
                else
                {
                    throw new RegistrationFailureException(result.Errors.FirstOrDefault().Description);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RegisterAdmin(RegistrationRequestDTO registrationRequestDTO)
        {
            User user = new()
            {
                UserName = registrationRequestDTO.Email,
                NormalizedUserName = registrationRequestDTO.Email.ToUpper(),
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                PhoneNumber = registrationRequestDTO.PhoneNUmber,
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName,
                IsBlocked = false
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == registrationRequestDTO.Email.ToLower());
                    if (userToReturn != null)
                    {
                        if (!await _roleManager.RoleExistsAsync(_adminRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(_adminRole));
                        }
                        await _userManager.AddToRoleAsync(userToReturn, _adminRole);

                        UserDTO userDto = new()
                        {
                            Email = userToReturn.Email,
                            Id = userToReturn.Id,
                            PhoneNumber = userToReturn.PhoneNumber
                        };
                    }
                }
                else
                {
                    throw new RegistrationFailureException(result.Errors.FirstOrDefault().Description);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
