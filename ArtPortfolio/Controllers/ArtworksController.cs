using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Controllers
{
    public class ArtworksController : Controller
    {
        public IActionResult Art(string id)
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ArtCreateModel artModel)
        {
            var artwork = new Artwork()
            {
                Title = artModel.Title,
                Description = artModel.Description,
                ImageUrl = artModel.ImageUrl
            };

            return Redirect($"artworks/id={artwork.Id}");
        }
    }
}
