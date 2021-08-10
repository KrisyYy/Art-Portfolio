using ArtPortfolio.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Data
{
    public class ArtPortfolioDbContext : IdentityDbContext<User>
    {
        public ArtPortfolioDbContext(DbContextOptions<ArtPortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<Prop> Props { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Commission>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");

                builder
                    .Entity<Artist>()
                    .HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Artist>(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder
                    .Entity<Artwork>()
                    .HasOne(a => a.Artist)
                    .WithMany(a => a.Artworks)
                    .HasForeignKey(a => a.ArtistId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder
                    .Entity<Comment>()
                    .HasOne(c => c.Artwork)
                    .WithMany(a => a.Comments)
                    .HasForeignKey(c => c.ArtworkId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder
                    .Entity<Comment>()
                    .HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder
                    .Entity<Commission>()
                    .HasOne(c => c.Artist)
                    .WithMany(a => a.Commissions)
                    .HasForeignKey(c => c.ArtistId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Prop>()
                .HasOne(p => p.Commission)
                .WithMany(c => c.Props)
                .HasForeignKey(p => p.CommissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Follow>()
                .HasKey(f => new {f.ArtistId, f.UserId});
            builder
                .Entity<Follow>()
                .HasOne(f => f.User)
                .WithMany(u => u.Followed)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .Entity<Follow>()
                .HasOne(f => f.Artist)
                .WithMany(a => a.Follows)
                .HasForeignKey(f => f.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<Like>()
                .HasKey(l => new {l.ArtworkId, l.UserId });
            builder
                .Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Liked)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .Entity<Like>()
                .HasOne(l => l.Artwork)
                .WithMany(a => a.Likes)
                .HasForeignKey(l => l.ArtworkId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
