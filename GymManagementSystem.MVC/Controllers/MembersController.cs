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

            if (created)
            {
                TempData["SuccessMessage"] = "Member is created successfuly";// goes from request to request which gone 
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Member creation failed";
            return View(nameof(Create), model);// takes name of view , model
        }

        public async Task<IActionResult> Details(int ? Id)
        {
            var member = await _memberService.GetMemberAsync(Id);
            if(member == null)
            {
                TempData["ErrorMessage"] = "Member Not found";
                return RedirectToAction(nameof(Index));

            }

            return View(member);
        }

        public async Task<IActionResult> HealthRecordDetails(int ? Id)
        {
            var healthrecord = await _memberService.GetHealthRecordDetails(Id);
            if(healthrecord == null)
            {
                TempData["ErrorMessage"] = "Has No healthrecord";
                return RedirectToAction(nameof(Index));
            }

            return View(healthrecord);
        }

        public async Task<IActionResult> EditMember(int ? Id)
        {
            var member = await _memberService.GetMemberToUpdateDetails(Id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not found";
                return RedirectToAction(nameof(Index));

            }
            return View(member);

        }

        [HttpPost]
        public async Task<IActionResult> EditMember(MemberToUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(nameof(EditMember), model);// takes name of view , model
            var result = await _memberService.UpdateMemberAsync(model);
            if(result)
            {
                TempData["SuccessMessage"] = "Member is edited successfuly";// goes from request to request which gone 
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Member editing failed";
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
            if (result)
            {
                TempData["SuccessMessage"] = "Member is deleted successfuly";// goes from request to request which gone 
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Member Deletion failed";
            return View(nameof(Index));
        }


    }
}
