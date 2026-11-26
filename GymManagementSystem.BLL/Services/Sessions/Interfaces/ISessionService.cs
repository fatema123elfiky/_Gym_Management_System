using GymManagementSystem.BLL.Common;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;


namespace GymManagementSystem.BLL.Services.Sessions.Interfaces
{
    public interface ISessionService
    {
        public Task<Result<IEnumerable<SessionViewModels>?>> GetAllSessionsAsync();
        public Task<Result> CreateSessionAsync(CreateSessionViewModel model);
        public Task<Result<IEnumerable<TrainerSelectedViewModel>>> GetTrainerForDropDownAsync();
        public Task<Result<IEnumerable<CategorySelectedViewModel>>> GetCategoryForDropDownAsync();
    }
}
