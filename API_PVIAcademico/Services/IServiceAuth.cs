using API_PVIAcademico.DTO;
using API_PVIAcademico.Models;

namespace API_PVIAcademico.Services
{
    public interface IServiceAuth
    {
        Task<User> Login(AuthResponse user);
        object generateJwtToken(User user);
    }
}
