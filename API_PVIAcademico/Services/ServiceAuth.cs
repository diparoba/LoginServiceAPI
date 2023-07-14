using API_PVIAcademico.Connection;
using API_PVIAcademico.DTO;
using API_PVIAcademico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_PVIAcademico.Services
{
    public class ServiceAuth : IServiceAuth
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceAuth(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public object generateJwtToken(User user)
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Password"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _header = new JwtHeader(_signingCredentials);
            //Claims
            var _claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("lastname", user.Lastname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            //Payload
            var _payLoad = new JwtPayload(
                issuer: _configuration["JWT:Dominio"],
                audience: _configuration["JWT:appApi"],
                claims: _claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60)
                );
            //Token
            var _token = new JwtSecurityToken(_header, _payLoad);
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
        public async Task<User> Login(AuthResponse user)
        {
           User userInfo = await authUser(user.Username, user.Password);
            if (userInfo == null)
            {
                return null;
            }
            else
            {
                return userInfo;
            }
        }
        public async Task<User> authUser(string username, string password)
        {
            try
            {
                User result = await _dataContext.User.Where(x => 
                x.Email.Equals(username)
                && x.Password.Equals(password)
                && x.Status.Equals("A")).FirstOrDefaultAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
