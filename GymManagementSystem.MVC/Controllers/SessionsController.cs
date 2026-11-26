using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.PL.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index()
        {
            var Sessions = await _sessionService.GetAllSessionsAsync();


            return View(Sessions.Value);
        }

        public async Task<IActionResult> Create()
        {                                                                                       
            await LoadTrainerandCategoryForSelectedList();
            return View();

        }

        [HttpPost]

        public async Task<IActionResult>Create(CreateSessionViewModel model)
        {
            var result = await _sessionService.CreateSessionAsync(model);

            if (result.Success)
            {
                TempData["SucessMessage"] = "Session Created successfuly";
                return RedirectToAction(nameof(Index));
            }
            await LoadTrainerandCategoryForSelectedList();// incase failed same drop down list should be displayed
            TempData["ErorrMessage"] = result.error;
            return View(nameof(Create),model);

        }

        #region Helper Methods
        private async Task LoadTrainerandCategoryForSelectedList()
        {
                                                                                                // what we take - what we display
            ViewBag.Trainers = new SelectList((await _sessionService.GetTrainerForDropDownAsync()).Value, "Id", "Name");
            ViewBag.Categories = new SelectList((await _sessionService.GetCategoryForDropDownAsync()).Value, "Id", "CategoryName");

        }

        #endregion
    }
}
