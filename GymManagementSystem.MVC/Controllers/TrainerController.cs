
using GymManagementSystem.BLL.Services.Trainers.Interfaces;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerViewModel model)
        {
            var result = await _trainerService.AddTrainerAsync(model);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Trainer added successfuly";
                return RedirectToAction(nameof(Index));
            }

            TempData["FailedMessage"] = result.error;
            return View(nameof(Create),model);
        }
    }
}
