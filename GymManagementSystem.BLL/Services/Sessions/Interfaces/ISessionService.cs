using GymManagementSystem.BLL.Services.Sessions.ViewModels;


namespace GymManagementSystem.BLL.Services.Sessions.Interfaces
{
    public interface ISessionService
    {
        public Task<IEnumerable<SessionViewModels>?> GetAllSessionsAsync();
    }
}
