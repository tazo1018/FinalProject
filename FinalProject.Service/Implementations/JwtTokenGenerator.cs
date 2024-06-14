using FinalProject.Contracts;
using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProject.Service.Implementations
{
    public class JwtTokenGenerator : IJwtGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions) 
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(IdentityUser applicationUser, IEnumerable<string> roles)
        {
            //infos claim ebshi damaxsovreba
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub ,applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim("UserId",applicationUser.Id),
                //rorame chamateba mojna kide sacxeli gvari an rame
            };
            //rolebis gatanac maqven
            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //exla token is awyoba
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddDays(2),
                //ra parametrebi swrideba sistemashi shesasvlelad
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                //token sheqmnilia
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
