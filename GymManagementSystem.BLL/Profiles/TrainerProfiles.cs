using AutoMapper;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using GymManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Profiles
{
    public class TrainerProfiles : Profile
    {
        public TrainerProfiles()
        {
            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty.ToString()));

        }
    }
}
