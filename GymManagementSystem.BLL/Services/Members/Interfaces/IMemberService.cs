

using GymManagementSystem.BLL.Services.Members.ViewModels;


namespace GymManagementSystem.BLL.Services.Members.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberViewModel>> GetMembersAsync();
        public Task<bool> AddMemberAsync(CreateMemberViewModel model);
    }
}
