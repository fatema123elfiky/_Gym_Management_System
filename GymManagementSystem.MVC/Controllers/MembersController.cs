using GymManagementSystem.BLL.Services.Members.Interfaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.MVC.Controllers;
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
            var result = await _memberService.GetMembersAsync();
            return result.Success ? View(result.Value) : RedirectToAction(nameof(Index), nameof(HomeController));
           
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
            var result = await _memberService.AddMemberAsync(model);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Member is created successfuly";// goes from request to request which gone 
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = result.error;
            return View(nameof(Create), model);// takes name of view , model
        }

        public async Task<IActionResult> Details(int ? Id)
        {
            var result = await _memberService.GetMemberAsync(Id);
            if(!result.Success)
            {
                TempData["ErrorMessage"] = result.error;
                return RedirectToAction(nameof(Index));

            }

            return View(result.Value);
        }

        public async Task<IActionResult> HealthRecordDetails(int ? Id)
        {
            var result = await _memberService.GetHealthRecordDetails(Id);
            if(!result.Success)
            {
                TempData["ErrorMessage"] = result.error;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Value);
        }

        public async Task<IActionResult> EditMember(int ? Id)
        {
            var result = await _memberService.GetMemberToUpdateDetails(Id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.error;
                return RedirectToAction(nameof(Index));

            }
            return View(result.Value);

        }

        [HttpPost]
        public async Task<IActionResult> EditMember(MemberToUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(nameof(EditMember), model);// takes name of view , model
            var result = await _memberService.UpdateMemberAsync(model);
            if(result.Success)
            {
                TempData["SuccessMessage"] = "Member is edited successfuly";// goes from request to request which gone 
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = result.error;
            return View(nameof(EditMember), model);

        }

        public async Task<IActionResult> Delete(int ? Id)
        {
            return View(Id);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int ? Id)
        {
            var result = await _memberService.DeleteMemberAsync(Id);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Member is deleted successfuly";// goes from request to request which gone 
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = result.error;
            return View(nameof(Index));
        }


    }
}
