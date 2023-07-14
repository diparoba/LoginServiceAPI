using API_PVIAcademico.Connection;
using API_PVIAcademico.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PVIAcademico.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceUser(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                List<User> result = await _dataContext.User.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                User result = await _dataContext.User.Where(x => x.Email == email).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                User result = await _dataContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

        public async Task<bool> Save(User user)
        {
            try
            {
                User infoUser = await GetByEmail(user.Email);
                if(infoUser == null)
                {
                    return false;
                }
                else
                {
                    user.CreatedDate = DateTime.Now;
                    user.UpdateDate = DateTime.Now;
                    await _dataContext.User.AddAsync(user);
                    await _dataContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Update(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateById(int id)
        {
            throw new NotImplementedException();
            
        }
    }
}
