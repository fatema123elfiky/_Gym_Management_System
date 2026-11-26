using GymManagementSystem.BLL.Common;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Services.Trainers.Interfaces
{
    public interface ITrainerService
    {
        public  Task<Result<IEnumerable<TrainerViewModel>>> GetAllTrainersAsync();
        public Task<Result> AddTrainerAsync(CreateTrainerViewModel model);
        public Task<Result<TrainerDetailsViewModel>> GetTrainerDetails(int Id);
    }
}
