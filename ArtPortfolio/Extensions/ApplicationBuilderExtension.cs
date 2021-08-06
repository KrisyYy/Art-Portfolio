using ArtPortfolio.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtPortfolio.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scopeService = app.ApplicationServices.CreateScope();
            var databaseService = scopeService.ServiceProvider.GetService<ArtPortfolioDbContext>();
            databaseService.Database.Migrate();

            return app;
        }
    }
}
