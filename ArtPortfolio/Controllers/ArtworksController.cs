using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artworks;

namespace ArtPortfolio.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly IArtworkService artworkService;

        public ArtworksController(IArtworkService artworkService)
        {
            this.artworkService = artworkService;
        }

        public IActionResult Art(string id)
        {
            var artwork = artworkService.GetArtworkById(id);
            if (artwork == null)
            {
                return NotFound();
            }

            var artData = new ArtViewModel()
            {
                Id = artwork.Id,
                Title = artwork.Title,
                ImageUrl = artwork.ImageUrl,
                Description = artwork.Description,
                Likes = artwork.Likes,
                Views = artwork.Views
            };

            return View(artData);
        }


        public IActionResult All()
        {
            var data = artworkService.GetListOfArtworks();

            var selection = data
                .OrderByDescending(a => a.Likes)
                .Select(a => new ArtListingViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    ImageUrl = a.ImageUrl,
                    Likes = a.Likes
                }).ToList();


            return View(selection);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ArtCreateModel artModel)
        {
            if (!ModelState.IsValid)
            {
                return View(artModel);
            }

            var id = artworkService.CreateArtwork(artModel);

            return Redirect($"Artworks/Art/{id}");
        }

        public IActionResult Like(string id)
        {
            var artwork = artworkService.GetArtworkById(id);
            if (artwork == null)
            {
                return NotFound();
            }
            artworkService.Like(artwork);

            return RedirectToAction("Art", "Artworks", new {id = artwork.Id});
        }

    }
}
