using API_PVIAcademico.Connection;
using API_PVIAcademico.Models;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Http.HttpClientLibrary;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class UserSyncService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly GraphServiceClient _graphClient;
    private readonly IConfiguration _configuration;
    public UserSyncService(IServiceScopeFactory scopeFactory, GraphServiceClient graphClient, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _graphClient = graphClient;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                // Obtener los usuarios de Azure AD
                //var users = await _graphClient.Users.GetAsync();
                var clientId = _configuration["AzureAd:ClientId"];
                var clientSecret = _configuration["AzureAd:ClientSecret"];
                var tenantId = _configuration["AzureAd:TenantId"];
                var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var graphClient = new GraphServiceClient(clientSecretCredential);

                var result = await graphClient.Users.GetAsync();
                foreach (var azureUser in result.Value)
                {
                    // Buscar el usuario en la base de datos local
                    var localUser = dbContext.User.FirstOrDefault(u => u.AzureID == azureUser.Id);

                    if (localUser == null)
                    {
                        // Si el usuario no existe en la base de datos local, crear un nuevo registro
                        localUser = new User
                        {
                            AzureID = azureUser.Id,
                            Name = azureUser.GivenName,
                            Lastname = azureUser.Surname,
                            Email = azureUser.Mail,
                            CreatedDate = DateTime.UtcNow,
                            UpdateDate = DateTime.UtcNow,
                            UserRole = new List<UserRole>
                            {
                                new UserRole { RoleId = 3 } // Asignar el rol "Import"
                            }
                        };

                        dbContext.User.Add(localUser);
                    }
                    else
                    {
                        // Si el usuario ya existe en la base de datos local, actualizar el registro
                        localUser.Name = azureUser.GivenName;
                        localUser.Lastname = azureUser.Surname;
                        localUser.Email = azureUser.Mail;
                        localUser.UpdateDate = DateTime.UtcNow;
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}

