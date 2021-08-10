using ArtPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artworks;

namespace ArtPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtworkService _artworkService;

        public HomeController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        public IActionResult Index()
        {
            var selection = _artworkService.GetListOfArtworks()
                .OrderByDescending(a => a.Likes)
                .ToList();

            var numberOfImages = selection.Count() <= 5 ? selection.Count() : 5;

            var artworks = selection.Take(numberOfImages).ToList();

            return View(artworks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
