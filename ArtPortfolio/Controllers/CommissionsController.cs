using ArtPortfolio.Extensions;
using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Models.Commissions;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Commissions;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    [Authorize]
    public class CommissionsController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly ICommissionService _commissionService;

        public CommissionsController(IArtistService artistService, ICommissionService commissionService)
        {
            _artistService = artistService;
            _commissionService = commissionService;
        }
        


        public IActionResult Info(int id)
        {
            var commission = _commissionService.GetCommissionById(id);
            if (commission == null)
            {
                return BadRequest();
            }

            return View(new CommissionInfoViewModel()
            {
                Commission = commission,
                Props = _commissionService.GetProps(id)
            });
        }
        

        public IActionResult NewCommission(int id)
        {
            var artistName = _artistService.GetName(id);
            ViewBag.ArtistName = artistName;
            ViewBag.ArtistId = id;
            return View();
        }


        [HttpPost]
        public IActionResult NewCommission(CommissionRequestFormModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View(requestModel);
            }

            var commId = _commissionService.Create(
                requestModel.Title,
                requestModel.NoteFromClient, 
                requestModel.CommissionType, 
                requestModel.SceneryType,
                requestModel.IsPrivate, 
                requestModel.IsForCommercialUse,
                requestModel.ArtistId,
                requestModel.Price
            );

            if (requestModel.Props != null)
            {
                foreach (var prop in requestModel.Props)
                {
                    _commissionService.AddProp(
                        prop.Name,
                        prop.Quantity,
                        prop.Description,
                        commId
                    );
                }
            }

            return RedirectToAction("Info", "Commissions", new {id = commId});
        }


        public IActionResult Commissions(int id)
        {
            if (!_artistService.IsArtist(User.Id()))
            {
                return RedirectToAction("BecomeArtist", "Artists");
            }

            var data = _commissionService.GetListOfCommissions(id);

            return View(data);
        }


        [HttpPost]
        public IActionResult UpdateCommission(int id, int status)
        {
            var artistId = _artistService.GetIdByUser(User.Id());

            if (_commissionService.ArtistId(id) != artistId)
            {
                return Unauthorized();
            }

            _commissionService.UpdateCommission(id, status);

            return RedirectToAction("Info", "Commissions", new { id = id });
        }
    }
}
