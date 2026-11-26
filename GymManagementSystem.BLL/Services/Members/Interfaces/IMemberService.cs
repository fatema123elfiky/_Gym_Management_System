

using GymManagementSystem.BLL.Common;
using GymManagementSystem.BLL.Services.Members.ViewModels;


namespace GymManagementSystem.BLL.Services.Members.Interfaces
{
    public interface IMemberService
    {
        public Task<Result<IEnumerable<MemberViewModel>>> GetMembersAsync();
        public Task<Result> AddMemberAsync(CreateMemberViewModel model);
        public Task<Result<MemberDetailsViewModel?>> GetMemberAsync(int? Id);

        public Task<Result<HealthRecordViewModel?>> GetHealthRecordDetails(int? Id);
        
        // we made new one as the mapping is different from details one
        public Task<Result<MemberToUpdateViewModel?>> GetMemberToUpdateDetails(int ? Id);
        public Task<Result> UpdateMemberAsync(MemberToUpdateViewModel model);

        public Task<Result> DeleteMemberAsync(int? Id);

    }
}
