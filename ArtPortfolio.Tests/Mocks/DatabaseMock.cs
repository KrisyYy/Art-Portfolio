using System;
using ArtPortfolio.Data;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static ArtPortfolioDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ArtPortfolioDbContext(dbContextOptions);
            }
        }
    }
}