using GymManagementSystem.BLL.Services.Members.Interfaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GymManagementSystem.PL.Controllers
{

    
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<IActionResult> Index()
        {
            var members = await _memberService.GetMembersAsync();
            return View(members);
        }

        // two create actions just for view and adding members

        // by default action is http get
        // we made for it view and inside it linked with the create which is the post one
        // and also we bind with creatememberviewmodel 
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model)
        {
            // 1) Input validation (controller)
            if (!ModelState.IsValid) return View(nameof(Create),model);// takes name of view , model

            // 2) Business rules (service)
            bool created = await _memberService.AddMemberAsync(model);

            if (created) return RedirectToAction(nameof(Index));
            return View(nameof(Create), model);// takes name of view , model
        }


    }
}
