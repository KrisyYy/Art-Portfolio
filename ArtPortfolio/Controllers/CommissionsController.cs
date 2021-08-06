using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Models.Commissions;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    public class CommissionsController : Controller
    {
        [Authorize]
        public IActionResult Info()
        {
            return View();
        }

        [Authorize]
        public IActionResult Request()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Request(CommissionRequestFormModel requestModel)
        {
            return RedirectToAction("Info", "Commission");
        }

        [Authorize]
        public IActionResult MyCommissions(int id)
        {
            return View();
        }
    }
}
