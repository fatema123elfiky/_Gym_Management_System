
using GymManagementSystem.DAL.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.MVC.Controllers
{
    public class PlansController : Controller
    {
        // before
        //private IPlanRepository _PlanRepository;
        //after
        private IRepository<Plan> _PlanRepository;
        public PlansController(IRepository<Plan> planRepository)
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
