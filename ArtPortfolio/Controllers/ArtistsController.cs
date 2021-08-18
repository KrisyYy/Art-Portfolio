using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Services.Artists;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        


        public IActionResult BecomeArtist()
        {
            if (_artistService.IsArtist(this.User.Id()))
            {
                return BadRequest();
            }

            return View();
        }


        [HttpPost]
        public IActionResult BecomeArtist(BecomeArtistFormModel artistModel)
        {
            var userId = this.User.Id();
            if (_artistService.IsArtist(userId))
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
                userId
            );

            return RedirectToAction("Profile", "Artists", new {id = artistId});
        }
        

        public IActionResult Profile(int id)
        {
            var artist = _artistService.GetArtistById(id, this.User.Id());
            if (artist == null)
            {
                return BadRequest();
            }

            return View(artist);
        }
        

        public IActionResult Follow(int id)
        {
            _artistService.Follow(id, this.User.Id());

            return RedirectToAction("Profile", "Artists", new {id = id});
        }


        public IActionResult Settings()
        {
            var userId = this.User.Id();
            var artistId = _artistService.GetIdByUser(userId);

            if (artistId == 0)
            {
                RedirectToAction("BecomeArtist", "Artists");
            }

            var artist = _artistService.GetArtistById(artistId, userId);

            var data = new SettingsFormModel()
            {
                Name = artist.Name,
                AvatarUrl = artist.AvatarUrl,
                Description = artist.Description,
                AvailableToCommission = artist.AvailableToCommission
            };

            return View(data);
        }


        [HttpPost]
        public IActionResult EditProfile(string change = "", string name = "", string avatarUrl = "", string description = "")
        {
            var userId = this.User.Id();
            var artistId = _artistService.GetIdByUser(userId);

            if (artistId == 0)
            {
                RedirectToAction("BecomeArtist", "Artists");
            }


            var success = _artistService.EditProfile(artistId, change, name, avatarUrl, description);
            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction("Profile", "Artists", new { id = artistId });
        }
    }
}
