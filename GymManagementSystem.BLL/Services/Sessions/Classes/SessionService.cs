using AutoMapper;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
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

 

        public async Task<IEnumerable<SessionViewModels>?> GetAllSessionsAsync()
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

            return MappedSessions;
        }
    }
}
