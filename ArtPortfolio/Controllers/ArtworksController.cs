using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Artworks;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    [Authorize]
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

            return View(new ArtViewModel()
            {
                Artwork = artwork,
                Comments = _artworkService.GetListOfComments(id),
                IsLiked = _artworkService.IsLiked(id, this.User.GetId())
            });
        }

        [HttpPost]
        public IActionResult Art(int id, CommentFormModel commentModel)
        {
            if (!ModelState.IsValid)
            {
                var artwork = _artworkService.GetArtworkById(id);
                if (artwork == null)
                {
                    return NotFound();
                }

                return View(artwork);
            }

            _artworkService.CreateComment(commentModel.Content, id, this.User.GetId());

            return RedirectToAction("Art", "Artworks", new { id = id });
        }

        public IActionResult Delete(int id)
        {
            var delete = _artworkService.Delete(id);
            if (!delete)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteComment(int id)
        {
            var artId = _artworkService.DeleteComment(id);

            if (artId == -1)
            {
                return BadRequest();
            }

            return RedirectToAction("Art", "Artworks", new { id = artId });
        }


        public IActionResult All([FromQuery]AllArtworksViewModel artModel)
        {
            var artworks = _artworkService.GetListOfArtworks(artModel.Search, artModel.Order, artModel.Page, AllArtworksViewModel.ArtPerPage);

            artModel.Artworks = artworks.Artworks;
            artModel.MaxPage = artworks.MaxPage;

            return View(artModel);
        }
        
        public IActionResult Artworks(int id)
        {
            var data = _artworkService.GetListOfArtworks();
            data.Artworks = data.Artworks
                .Where(a => a.ArtistId == id)
                .ToList();

            ViewBag.ArtistId = id;

            return View(data);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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


        
        public IActionResult Like(int id)
        {
            _artworkService.Like(id, this.User.GetId());

            return RedirectToAction("Art", "Artworks", new { id = id});
        }
    }
}
