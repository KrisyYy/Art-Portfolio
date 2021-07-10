using ArtPortfolio.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtPortfolio.Middleware
{
    public static class Extensions
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
