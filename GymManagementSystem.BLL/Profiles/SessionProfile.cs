using AutoMapper;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Profiles
{
    public class SessionProfile:Profile
    {
        public SessionProfile()
        {
            /*The mapping only has data to read if the navigation properties are loaded. 
             * That's why GetAllSessionsWithTrainerAndCategory() uses Include for the trainer and category. 
             * Without it, Trainer and Category would be null and the mapped names would come out empty.*/
            // i think because it is navigational so we should doo that and  we could add the part of avaliable slots but advanced
            // will be discussed later
            CreateMap<Session, SessionViewModels>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        }
    }
}
