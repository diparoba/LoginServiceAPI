using API_PVIAcademico.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PVIAcademico.Connection
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        static DataContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

    }
}
