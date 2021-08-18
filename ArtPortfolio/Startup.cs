using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Extensions;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Artworks;
using ArtPortfolio.Services.Comments;
using ArtPortfolio.Services.Commissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArtPortfolio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ArtPortfolioDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ArtPortfolioDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IArtworkService, ArtworkService>();
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<ICommissionService, CommissionService>();
            services.AddTransient<ICommentService, CommentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.MigrateDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "create",
                        pattern: "create",
                        defaults: new { controller = "Artworks", action = "Create" });

                    endpoints.MapControllerRoute(
                        name: "library",
                        pattern: "library",
                        defaults: new { controller = "Artworks", action = "All" });

                    endpoints.MapControllerRoute(
                        name: "profile",
                        pattern: "profile/{id}",
                        defaults: new { controller = "Artists", action = "Profile" });

                    endpoints.MapControllerRoute(
                        name: "art",
                        pattern: "art/{id}",
                        defaults: new { controller = "Artworks", action = "Art" });

                    endpoints.MapControllerRoute(
                        name: "artworks",
                        pattern: "artworks/{id}",
                        defaults: new { controller = "Artworks", action = "Artworks" });

                    endpoints.MapControllerRoute(
                        name: "commissions",
                        pattern: "commissions/{id}",
                        defaults: new { controller = "Commissions", action = "Commissions" });

                    endpoints.MapControllerRoute(
                        name: "settings",
                        pattern: "settings",
                        defaults: new { controller = "Artists", action = "Settings" });

                    endpoints.MapControllerRoute(
                        name: "commission",
                        pattern: "commission/{id}",
                        defaults: new { controller = "Commissions", action = "Info" });

                    endpoints.MapControllerRoute(
                        name: "request",
                        pattern: "request/{id}",
                        defaults: new { controller = "Commissions", action = "NewCommission" });

                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
