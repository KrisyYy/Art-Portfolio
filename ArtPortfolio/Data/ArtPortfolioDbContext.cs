﻿using ArtPortfolio.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Data
{
    public class ArtPortfolioDbContext : IdentityDbContext
    {
        public ArtPortfolioDbContext(DbContextOptions<ArtPortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Commission> Commissions { get; set; }

    }
}
