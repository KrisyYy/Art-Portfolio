using System;
using System.Threading.Tasks;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtPortfolio.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scopeService = app.ApplicationServices.CreateScope();

            var services = scopeService.ServiceProvider;
            var data = services.GetRequiredService<ArtPortfolioDbContext>();

            data.Database.Migrate();

            SeedAdministrator(services);

            return app;
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Administrator" };
                    await roleManager.CreateAsync(role);

                    var adminEmail = "admin@email.com";
                    var adminPass = "admin12";
                    var adminUsername = "admin";

                    var user = new User()
                    {
                        Email = adminEmail,
                        UserName = adminUsername
                    };

                    await userManager.CreateAsync(user, adminPass);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();

        }
    }
}
