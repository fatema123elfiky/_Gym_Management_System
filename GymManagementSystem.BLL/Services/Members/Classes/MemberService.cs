using GymManagementSystem.BLL.Services.Members.Interfaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Services.Members.Classes
{
    public class MemberService : IMemberService
    {
        // talk to repo to deal with data and apply your bussiness logic
        private readonly IRepository<Member> _memberRepository;
        public MemberService(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<bool> AddMemberAsync(CreateMemberViewModel model)
        {
            try
            {
                // email , phone should be unique so we should cheeck here
                var members = await _memberRepository.GetAllAsync();
                bool IsNotUniquePhone = members.Any(x => x.Phone == model.Phone);
                bool IsNotUniqueEmail = members.Any(x => x.Email == model.Email);

                if (IsNotUniqueEmail || IsNotUniquePhone) return false;
                var member = new Member()
                {
                    Name = model.Name,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    Address = new Address()
                    {
                        BuildingNumber = model.BuildingNumber,
                        City = model.City,
                        Street = model.City
                    },
                    HealthRecord = new HealthRecord()
                    {
                        BloodType = model.HealthRecordViewModel.BloodType,
                        Height = model.HealthRecordViewModel.Height,
                        Weight = model.HealthRecordViewModel.Weight,
                        Note = model.HealthRecordViewModel.Note
                    }

                };

                return await _memberRepository.AddAsync(member) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
        {
           // return await _memberRepository.GetAllAsync();
           var members = await _memberRepository.GetAllAsync();
            //var MemberViews = new List<MemberViewModel>();
            //// manaual mapping

            //foreach(var member in members)
            //{
            //    var memberVm = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Email = member.Email,
            //        Gender = member.Gender.ToString(),
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Name=member.Name
            //    };
            //    MemberViews.Add(memberVm);

            //}
            //return MemberViews;

            // some better mapping using select

            return members.Select(x => new MemberViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                Gender = x.Gender.ToString(),
                Phone = x.Phone,
                Photo = x.Photo,
                Name = x.Name

            });


        }
    }
}
