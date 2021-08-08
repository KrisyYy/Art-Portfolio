using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Artworks;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly IArtworkService _artworkService;
        private readonly IArtistService _artistService;

        public ArtworksController(IArtworkService artworkService, IArtistService artistService)
        {
            _artworkService = artworkService;
            _artistService = artistService;
        }

        public IActionResult Art(int id)
        {
            var artwork = _artworkService.GetArtworkById(id);
            if (artwork == null)
            {
                return NotFound();
            }

            _artworkService.View(id);

            return View(artwork);
        }


        public IActionResult All()
        {
            var data = _artworkService.GetListOfArtworks()
                .OrderByDescending(a => a.Likes)
                .ThenByDescending(a => a.Id)
                .ToList();

            return View(data);
        }

        [Authorize]
        public IActionResult MyArtworks(int id)
        {
            var data = _artworkService.GetListOfArtworks()
                .OrderByDescending(a => a.Id)
                .Where(a => a.ArtistId == id).ToList();

            return View(data);
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ArtCreateModel artModel)
        {
            var userId = this.User.GetId();
            var artistId = _artistService.GetIdByUser(userId);

            if (!ModelState.IsValid)
            {
                return View(artModel);
            }

            var artId = _artworkService.CreateArtwork
            (
                artModel.Title,
                artModel.Description,
                artModel.ImageUrl,
                artistId
            );

            return RedirectToAction("Art", "Artworks", new {id = artId});
        }

        [Authorize]
        public IActionResult Like(int id)
        {
            _artworkService.Like(id);

            return RedirectToAction("Art", "Artworks", new { id = id});
        }

    }
}
