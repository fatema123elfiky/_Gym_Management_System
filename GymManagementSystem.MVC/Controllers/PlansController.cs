
using GymManagementSystem.DAL.Repositories.interfaces;
using GymManagementSystem.DAL.Repositories.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DAL.Data.Contexts;

namespace GymManagementSystem.MVC.Controllers
{
    public class PlansController : Controller
    {
        private IPlanRepository _PlanRepository;
        public PlansController(IPlanRepository planRepository)
        {
            // still problem as should be dependency injection and not creating instance of repository here
            // controller should not communicate with DAL , should communicate with service layer
            // (BLL) and service layer should communicate with DAL

            //wrong
            //_PlanRepository = new PlanRepository();

            //correct
            _PlanRepository = planRepository;
        }
        public async Task<IActionResult> Index()
        {
           var plans = await _PlanRepository.GetAllAsync();
            return View(plans); // give the view with same name of action the model or data 
            // return the view itself with that data 
        }

        public async Task<IActionResult> Details(int id)
        {
            var plan = await _PlanRepository.GetByIdAsync(id);
            return View(plan);

        }
    }
}
