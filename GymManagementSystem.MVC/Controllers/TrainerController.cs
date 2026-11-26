
using GymManagementSystem.BLL.Services.Trainers.Interfaces;
using GymManagementSystem.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GymManagementSystem.PL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public async Task <IActionResult> Index()
        {
            var result = await _trainerService.GetAllTrainersAsync();
            if (!result.Success)
            {
              
                return RedirectToAction(nameof(Index),nameof(HomeController));
            }
            return View(result.Value);
        }
    }
}
