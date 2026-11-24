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
        private readonly IRepository<MemberShip> _memberShipRepository;
        private readonly IRepository<Plan> _planRepository;
        private readonly IRepository<HealthRecord> _healthRecordRepository;
        public MemberService(IRepository<Member> memberRepository ,IRepository<MemberShip> memberShip,IRepository<Plan> planRepository,IRepository<HealthRecord> healthRecordRepository)
        {
            _memberRepository = memberRepository;
            _memberShipRepository = memberShip;
            _planRepository = planRepository;
            _healthRecordRepository = healthRecordRepository;
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

        public async Task<HealthRecordViewModel?> GetHealthRecordDetails(int? Id)
        {
            var healthrecords = await _healthRecordRepository.GetAllAsync();
            if (healthrecords != null)
            {
                var healthrecord = healthrecords.Where(x => x.MemberId == Id).FirstOrDefault();
                if (healthrecord != null)
                    return new HealthRecordViewModel()
                    {
                        BloodType = healthrecord.BloodType,
                        Height = healthrecord.Height,
                        Weight = healthrecord.Weight,
                        Note = healthrecord.Note
                    };
            }
            return null;
        }

        public async Task<MemberDetailsViewModel?> GetMemberAsync(int? Id)
        {
            if (Id == null || Id.Value == 0)
                return null;
            var member = await _memberRepository.GetByIdAsync(Id.Value);// as it is nullable and it should take value anyway
            if (member == null)
                return null;
            var model = new MemberDetailsViewModel()
            {
                Email = member.Email,
                Gender = member.Gender.ToString(),
                Phone = member.Phone,
                Photo = member.Photo,
                Name = member.Name,
                DateOfBirth = member.DateOfBirth.ToString("d/M/yyyy"),
                Address = $"{member.Address?.BuildingNumber} - {member.Address?.Street} - {member.Address?.City}",
               
                // we could not do that as by default it does not load the navigation properties
                // and we could not edit in generic repo to include the related navigation properties
                // so we could do special repo for member in that case to get related data or make many requests to each table
                // which are membership , plan 
                
                // if we made first way , so we would leave the three lines of code as they will be correct
                // but we remove them if we applied the second one 

                // which is better 
                // one request with related data so joins
                // or many requests better than join ? depends on size of data, so  both are fine
                // if data large so joins better as one request , but if small data use many requests
                // perfomance point of view

                //MembershipStartDate = member.MemberShips.Where(x => x.IsActive).Select(x => x.CreatedAt).FirstOrDefault().ToString(),
                //MembershipEndDate = member.MemberShips.Where(x => x.IsActive).Select(x => x.CreatedAt).FirstOrDefault().ToString()
                //PlanName = member.MemberShips.Where(x => x.IsActive).Select(x => x.Plan.Name).FirstOrDefault()

            };

            var memberships = await _memberShipRepository.GetAllAsync();
            if (memberships.Any())
            {
                /*
                 Rule:
                IQueryable (still a database query): use FirstOrDefaultAsync, ToListAsync, AnyAsync with await.
                IEnumerable or List (data already loaded): use FirstOrDefault, ToList, Any without await.*/


                var membershipsActive = memberships.Where(x => x.MemberId == member.Id && x.IsActive).FirstOrDefault();
                if (membershipsActive != null)
                {
                    model.MembershipStartDate = membershipsActive.CreatedAt?.ToString("d/M/yyyy"); // we added nullable as it is nullable
                    model.MembershipEndDate = membershipsActive.EndDate.ToString("d/M/yyyy");

                    var plan = await _planRepository.GetByIdAsync(membershipsActive.PlanId);
                    if (plan != null)
                        model.PlanName = plan.Name;
                }
            }

            return model;
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

        public async Task<MemberToUpdateViewModel?> GetMemberToUpdateDetails(int? Id)
        {
            if (Id == null || Id.Value == 0)
                return null;
            var member = await _memberRepository.GetByIdAsync(Id.Value);// as it is nullable and it should take value anyway
            if (member == null)
                return null;

            var model = new MemberToUpdateViewModel()
            {
                Id = member.Id,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Name = member.Name,
                BuildingNumber = member.Address?.BuildingNumber??0,
                City = member.Address?.City??"Not found",
                Street = member.Address?.Street ?? "Not found"

            };
            return model;
        }

        public async Task<bool> UpdateMemberAsync(MemberToUpdateViewModel model)
        {
            try
            {
                if (model == null) return false;

                var members = await _memberRepository.GetAllAsync();

                bool IsNotUniquePhone = members.Any(x => x.Phone == model.Phone && x.Id != model.Id);
                bool IsNotUniqueEmail = members.Any(x => x.Email == model.Email && x.Id != model.Id);

                if (IsNotUniqueEmail || IsNotUniquePhone) return false;

                var member = await _memberRepository.GetByIdAsync(model.Id);

                if (member == null) return false;

                member.Address.BuildingNumber = model.BuildingNumber;
                member.Address.City = model.City;
                member.Address.Street = model.Street;
                member.Email = model.Email;
                member.Phone = model.Phone;

                return await _memberRepository.UpdateAsync(member) > 0;

            }
            catch
            {
                return false;
            }
        }
    }
}
