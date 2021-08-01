using ArtPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private ArtPortfolioDbContext data;

        public HomeController(ArtPortfolioDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var selection = this.data.Artworks
                .OrderByDescending(a => a.Likes)
                .Select(a => new ArtListingViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    ImageUrl = a.ImageUrl,
                    Likes = a.Likes
                });

            var numberOfImages = selection.Count() <= 5 ? selection.Count() : 5;

            var artworks = selection.Take(numberOfImages).ToList();

            return View(artworks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
