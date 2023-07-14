using API_PVIAcademico.Connection;

namespace API_PVIAcademico.FirstData
{
    public class DbInitial
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            //User Exist Verification
            if (context.Role.Any())
            {
                return;
            }
            //Role Create
            var rols = new Models.Role[]
            {
                new Models.Role
                {
                    Id= 1,
                    Description = "Administrator",
                    Status = "A",
                    Add = DateTime.Now
                },
                new Models.Role
                {
                    Id= 2,
                    Description = "Student",
                    Status = "A",
                    Add = DateTime.Now
                },
                new Models.Role
                {
                    Id= 3,
                    Description = "Import",
                    Status = "A",
                    Add = DateTime.Now
                }
            };
            foreach (var rol in rols)
            {
                context.Role.Add(rol);
            }
            //User Create
            var users = new Models.User[]
            {
                new Models.User
                {
                    Id = 1,
                    Name = "Administrator",
                    Lastname = "GBM Paulo VI",
                    Status = "A",
                    Email = "admin@gbmpaulovi.edu.ec",
                    Password = "Admin"
                }
            };
            foreach (var user in users)
            {
                context.User.Add(user);
            }
            //UserRole Assign
            var UserRoles = new Models.UserRole[]
            {
                new Models.UserRole
                {
                    Id = 1,
                    UserId = 1,
                    RoleId = 1
                }
            };
            foreach (var userRole in UserRoles)
            {
                context.UserRole.Add(userRole);
            }
            context.SaveChanges();
        }

    }
}
