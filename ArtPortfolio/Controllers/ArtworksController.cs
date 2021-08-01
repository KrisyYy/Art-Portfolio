using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Controllers
{
    public class ArtworksController : Controller
    {
        private ArtPortfolioDbContext data;

        public ArtworksController(ArtPortfolioDbContext data)
        {
            this.data = data;
        }

        public IActionResult Art(string id)
        {
            var artwork = this.data.Artworks.FirstOrDefault(a => a.Id == id);
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
            var selection = this.data.Artworks
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

            var artwork = new Artwork()
            {
                Title = artModel.Title,
                Description = artModel.Description,
                ImageUrl = artModel.ImageUrl
            };

            this.data.Artworks.Add(artwork);
            this.data.SaveChanges();

            return Redirect($"Artworks/Art/{artwork.Id}");
        }

        public IActionResult Like(string id)
        {
            var artwork = this.data.Artworks.FirstOrDefault(a => a.Id == id);
            if (artwork == null)
            {
                return NotFound();
            }

            artwork.Likes++;
            this.data.SaveChanges();

            return RedirectToAction("Art", "Artworks", new {id = artwork.Id});
        }

    }
}
