using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Services.Artists;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [Authorize]
        public IActionResult BecomeArtist()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult BecomeArtist(BecomeArtistFormModel artistModel)
        {
            var currentUserId = this.User.GetId();
            if (_artistService.IsArtist(currentUserId))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(artistModel);
            }

            var artistId = _artistService.CreateArtist
            (
                artistModel.Name, 
                artistModel.Description, 
                currentUserId
            );

            return RedirectToAction("MyArtworks", "Artworks", new {id = artistId});
        }

        [Authorize]
        public IActionResult Profile(int id)
        {
            var currentArtist = _artistService.GetArtist(id);
            if (currentArtist == null)
            {
                return BadRequest();
            }

            var artistData = new ArtistProfileViewModel()
            {
                Name = currentArtist.Name,
                AvailableToCommission = currentArtist.AvailableToCommission,
                Description = currentArtist.Description,
                Followers = currentArtist.Followers
            };

            return View(artistData);
        }

        [Authorize]
        public IActionResult Follow(int id)
        {
            _artistService.Follow(id);

            return RedirectToAction("Profile", "Artists", new {id = id});
        }
    }
}
