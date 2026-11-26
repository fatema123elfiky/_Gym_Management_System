using AutoMapper;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Models.Enums;
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
            CreateMap<CreateTrainerViewModel, Trainer>()
                .AfterMap((src, dest) => dest.Address = new Address()
                {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street
                });

            CreateMap<Trainer, TrainerDetailsViewModel>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => $"{src.Specialty.ToString()} Trainer"));
                

        }
    }
}
