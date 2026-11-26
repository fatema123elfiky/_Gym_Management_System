using AutoMapper;
using GymManagementSystem.BLL.Common;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Models.Enums;
using GymManagementSystem.DAL.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Services.Sessions.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> CreateSessionAsync(CreateSessionViewModel model)
        {
            try
            {
                if (model.EndDate <= model.StartDate)
                    return Result.Validation("End date must be after start date");

                if (model.StartDate <= DateTime.Now)
                    return Result.Fail("start date must be in future");

                if (model.Capacity < 1 || model.Capacity > 25)
                    return Result.Validation("capacity must be bewteen 1 and 25");

                var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.TrainerId);
                if (trainer == null) return Result.NotFound("trainer not found");

                var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId);
                if (category == null) return Result.NotFound("category not found");

                var isValid = Enum.TryParse<Specialty>(category.CategoryName, true, out var categorySpeciality);
                if (!isValid || trainer.Specialty != categorySpeciality)
                    return Result.Validation("can not create this session because trainer not match with category");

                var session = _mapper.Map<Session>(model);
                await _unitOfWork.GetRepository<Session>().AddAsync(session);
                var result = await _unitOfWork.SaveChangesAsync() > 0;
                return result ? Result.OK() : Result.Fail("Failed to create session");

            }
            catch
            {
                return Result.Fail("Internal server error");
            }
        }

        public async Task<Result<IEnumerable<SessionViewModels>?>> GetAllSessionsAsync()
        {
            var sessions = await _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategory();
            //if (sessions == null) return null; // handled in the frontend

            // manual mapping
            // we will do automatic mapping
            //var MappedSessions = sessions.Select(s => new SessionViewModels()
            //{
            //    Id = s.Id,
            //    Capacity = s.Capacity,
            //    CategoryName = s.Category.CategoryName,
            //    Description = s.Description,
            //    TrainerName = s.Trainer.Name,
            //    EndDate = s.EndDate,
            //    StartDate = s.StartDate
            //});

            var MappedSessions = _mapper.Map<IEnumerable<SessionViewModels>>(sessions);//dest , src
            foreach(var session in MappedSessions)
            {
                session.AvailableSlots = session.Capacity - 
                    await _unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id);
                    // n+1 problem as one request up and another n calls for getting avaliable slots
                    // it has solution , we will disscuss it later
                    // join the needed data in one request 
            }

            return Result<IEnumerable<SessionViewModels>?>.OK(MappedSessions);
        }

        public async Task<Result<IEnumerable<CategorySelectedViewModel>>> GetCategoryForDropDownAsync()
        {
            var result = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            var model = _mapper.Map<IEnumerable<CategorySelectedViewModel>>(result);

            return Result<IEnumerable<CategorySelectedViewModel>>.OK(model);

        }

        public async Task<Result<IEnumerable<TrainerSelectedViewModel>>> GetTrainerForDropDownAsync()
        {
            var result = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();
            var model = _mapper.Map<IEnumerable<TrainerSelectedViewModel>>(result);

            return Result< IEnumerable<TrainerSelectedViewModel>>.OK(model);
        }
    }
}
