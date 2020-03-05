using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Interfaces;
using HIMS.EF.DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class UserProfileService : IUserProfileService
    {

        private IUnitOfWork DimsDatabase { get; }
        private IProcedureManager Pm { get; }
        private UserService UserService { get; }
        private UserProfileRepository Repository { get; }

        public UserProfileService(IUnitOfWork unitOfWork, IProcedureManager pm, UserProfileRepository userProfileRepository, UserService userService)
        {
            DimsDatabase = unitOfWork;
            Pm = pm;
            Repository = userProfileRepository;
            UserService = userService;
        }


        public void DeleteUserProfileById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The User Profile's id value is not set", String.Empty);
            DimsDatabase.UserProfiles.Delete(id.Value);
            DimsDatabase.Save();
        }

        public void DeleteUserProfileByEmail(string email)
        {
            if (email == null)
                throw new ValidationException("The User Profile's email is not set", String.Empty);
            
            Repository.DeleteByEmail(email);
            UserService.DeleteUserByEmail(email);
        }

        public void Dispose()
        {
            DimsDatabase.Dispose();
        }

        public UserProfileDTO GetUserProfileById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The User Profile's id value is not set", String.Empty);

            var userProfile = DimsDatabase.UserProfiles.Get(id.Value);

            if (userProfile == null)
                throw new ValidationException($"The User Profile with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<UserProfile, UserProfileDTO>(userProfile);
        }

        public IEnumerable<UserProfileDTO> GetUserProfiles()
        {
            return Mapper.Map<IEnumerable<UserProfile>, List<UserProfileDTO>>(DimsDatabase.UserProfiles.GetAll());
        }

        public void SaveUserProfile(UserProfileDTO userProfile)
        {

            // Validation (must be improve)
            if (userProfile.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(userProfile.Name)} must be less than 25"
                    , nameof(userProfile.Name));
            if (userProfile.LastName.Length > 25)
                throw new ValidationException($"The length of {nameof(userProfile.LastName)} must be less than 25"
                    , nameof(userProfile.LastName));
            if (userProfile.Address != null && userProfile.Address.Length > 255)
                throw new ValidationException($"The length of {nameof(userProfile.Address)} must be less than 25"
                    , nameof(userProfile.Address));

            var _userProfile = new UserProfile
            {
                Name = userProfile.Name,
                Email = userProfile.Email,
                LastName = userProfile.LastName,
                DirectionId = userProfile.DirectionId,
                Sex = userProfile.Sex,
                Education = userProfile.Education,
                BirthDate = userProfile.BirthDate,
                UniversityAverageScore = userProfile.UniversityAverageScore,
                MathScore = userProfile.MathScore,
                Address = userProfile.Address,
                MobilePhone = userProfile.MobilePhone,
                Skype = userProfile.Skype,
                StartDate = userProfile.StartDate,
                Direction = Mapper.Map<DirectionDTO, Direction>(userProfile.Direction),
                UserTasks = Mapper.Map<List<UserTaskDTO>, ICollection<UserTask>>(userProfile.UserTasks.ToList())
            };

            DimsDatabase.UserProfiles.Create(_userProfile);
            DimsDatabase.Save();
        }

        public void UpdateUserProfile(UserProfileDTO userProfile)
        {
            // Validation (must be improve)
            if (userProfile.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(userProfile.Name)} must be less than 25"
                    , nameof(userProfile.Name));
            if (userProfile.LastName.Length > 25)
                throw new ValidationException($"The length of {nameof(userProfile.LastName)} must be less than 25"
                    , nameof(userProfile.LastName));
            if (userProfile.Address.Length > 255)
                throw new ValidationException($"The length of {nameof(userProfile.Address)} must be less than 25"
                    , nameof(userProfile.Address));

            var _userProfile = DimsDatabase.UserProfiles.Get(userProfile.UserId);
            
            if (_userProfile != null)
            {
                Mapper.Map(userProfile, _userProfile);
                DimsDatabase.Save();
            }

        }
    }
}
