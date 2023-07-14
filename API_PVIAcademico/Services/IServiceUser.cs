using API_PVIAcademico.Models;

namespace API_PVIAcademico.Services
{
    public interface IServiceUser
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task<bool> Save(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
        Task<bool> DeleteByEmail(string email);
        Task<bool> UpdateById(int id);
    }
}
