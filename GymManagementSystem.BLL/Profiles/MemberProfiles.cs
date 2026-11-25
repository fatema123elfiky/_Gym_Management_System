using AutoMapper;

using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;


namespace GymManagementSystem.BLL.Profiles
{
    public class MemberProfiles :Profile
    {
        public MemberProfiles()
        {
            // we mapped from src to dest
            // through same naming and same datatype


            //      src         dest
            CreateMap<Member, MemberDetailsViewModel>()
                // dest we map to , opt , src we mapp from
                .ForMember(dest => dest.Address,opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("d/M/yyyy")));

            //CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap(); 
            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));
            
            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) => //that one takes the whole object instead of formember one by one
                {
                    dest.Address.City = src.City;
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                });

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));// no need to do as address as made up reverse map



        }
    }
}
