using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using Microsoft.AspNetCore.Mvc;

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


            return View();
        }
    }
}
