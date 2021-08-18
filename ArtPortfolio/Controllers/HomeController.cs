using System.Collections.Generic;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Models.Home;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Artworks;

namespace ArtPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtworkService _artworkService;
        private readonly IArtistService _artistService;

        public HomeController(IArtworkService artworkService, IArtistService artistService)
        {
            _artworkService = artworkService;
            _artistService = artistService;
        }



        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return View(new IndexViewModel());
            }

            var userId = this.User.Id();
            var artworks = _artworkService.ArtworksFromFollowed(userId);
            var artists = _artistService.RecommendedArtists(userId);


            var indexModel = new IndexViewModel()
            {
                Artists = artists,
                Artworks = artworks
            };

            return View(indexModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
