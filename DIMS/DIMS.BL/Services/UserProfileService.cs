using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Repositories;

namespace HIMS.BL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork Database { get; }
        private UserService UserService { get; }
        private UserProfileRepository Repository { get; }
        private UserTaskService UserTasks { get; }

        public UserProfileService(IUnitOfWork unitOfWork, 
                                  UserProfileRepository userProfileRepository, 
                                  UserService userService, 
                                  UserTaskService userTaskService)
        {
            Database = unitOfWork;
            Repository = userProfileRepository;
            UserService = userService;
            UserTasks = userTaskService;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The User Profile's id value is not set", string.Empty);
            }
            
            Database.UserProfiles.DeleteById(id.Value);
            Database.Save();
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ValidationException("The User Profile's email is not set", string.Empty);
            }

            OperationDetails operationDetails = await UserService.DeleteByEmail(email);

            // TODO: create enum for general messages or rewrite this checking
            if (operationDetails.Succedeed || operationDetails.Message == "The user with such Email not found! Email: ")
            {
                Repository.DeleteByEmail(email);
                
                return true;
            } 
            else
            {
                throw new ValidationException(operationDetails.Message, operationDetails.Property);
            }
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The User Profile's id value is not set!", string.Empty);
            }

            var userProfileEmail = Database.UserProfiles.GetById(id.Value).Email;

            return await DeleteByEmailAsync(userProfileEmail);
        }

        public UserProfileDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The User Profile's id value is not set", string.Empty);
            }

            var userProfile = Database.UserProfiles.GetById(id.Value);

            if (userProfile == null)
            {
                throw new ValidationException($"The User Profile with id = {id.Value} was not found", string.Empty);
            }

            return Mapper.Map<UserProfile, UserProfileDTO>(userProfile);
        }

        public IEnumerable<UserProfileDTO> GetAll()
        {
            var userProfiles = Database.UserProfiles.GetAll();
            
            return Mapper.Map<IEnumerable<UserProfile>, List<UserProfileDTO>>(userProfiles);
        }

        public void Save(UserProfileDTO userProfile)
        {
            // TODO: improve validation - maybe create validation service with methods
            if (userProfile.Name.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Name)} must be less than 25"
                    , nameof(userProfile.Name));
            }

            if (userProfile.LastName.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.LastName)} must be less than 25"
                    , nameof(userProfile.LastName));
            }

            if (userProfile.Address != null && userProfile.Address.Length > 255)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Address)} must be less than 25"
                    , nameof(userProfile.Address));
            }

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
                StartDate = userProfile.StartDate
            };

            Database.UserProfiles.Create(_userProfile);
            Database.Save();
        }

        public void Update(UserProfileDTO userProfile)
        {
            // TODO: improve validation - maybe create validation service with methods
            if (userProfile.Name.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Name)} must be less than 25"
                    , nameof(userProfile.Name));
            }

            if (userProfile.LastName.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.LastName)} must be less than 25"
                    , nameof(userProfile.LastName));
            }

            if (userProfile.Address.Length > 255)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Address)} must be less than 25"
                    , nameof(userProfile.Address));
            }

            var _userProfile = Database.UserProfiles.GetById(userProfile.UserId);

            var userTasks = UserTasks.GetByUserId(userProfile.UserId);
            
            if (_userProfile != null)
            {
                Mapper.Map<UserProfileDTO, UserProfile>(userProfile, _userProfile);

                _userProfile.UserTasks = Mapper.Map<IEnumerable<UserTaskDTO>, ICollection<UserTask>>(userTasks);

                Database.Save();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
