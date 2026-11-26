using AutoMapper;
using GymManagementSystem.BLL.Common;
using GymManagementSystem.BLL.Services.Trainers.Interfaces;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Services.Trainers.Class
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TrainerViewModel>>> GetAllTrainersAsync()
        {
            var trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();

            var mapped = _mapper.Map <IEnumerable<TrainerViewModel>> (trainers);
            return Result<IEnumerable<TrainerViewModel>>.OK(mapped);


        }
    }
}
