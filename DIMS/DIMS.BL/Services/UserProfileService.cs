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

        private IUnitOfWork dimsDatabase { get; }
        private IProcedureManager Pm { get; }
        private UserService userService { get; }
        private UserProfileRepository repository { get; }

        private UserTaskService userTasks { get; }

        public UserProfileService(IUnitOfWork unitOfWork, IProcedureManager pm, UserProfileRepository userProfileRepository, UserService userService, UserTaskService userTaskService)
        {
            dimsDatabase = unitOfWork;
            Pm = pm;
            repository = userProfileRepository;
            this.userService = userService;
            userTasks = userTaskService;
        }


        public void DeleteItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The User Profile's id value is not set", String.Empty);
            dimsDatabase.UserProfiles.Delete(id.Value);
            dimsDatabase.Save();
        }

        public async Task<OperationDetails> DeleteUserProfileByEmailAsync(string email)
        {
            if (email == null)
                throw new ValidationException("The User Profile's email is not set", String.Empty);

            OperationDetails operationDetails = await userService.DeleteUserByEmail(email);

            if (operationDetails.Succedeed || operationDetails.Message == "The user with such Email not found! Email: ")
            {
                repository.DeleteByEmail(email);
                return operationDetails;
            }
            else
                throw new ValidationException(operationDetails.Message, operationDetails.Property);
        }

        public async Task<OperationDetails> DeleteItemAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The User Profile's id value is not set!", String.Empty);

            var userProfileEmail = dimsDatabase.UserProfiles.Get(id.Value).Email;

            return await DeleteUserProfileByEmailAsync(userProfileEmail);
        }

        public void Dispose()
        {
            dimsDatabase.Dispose();
        }

        public UserProfileDTO GetItem(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The User Profile's id value is not set", String.Empty);

            var userProfile = dimsDatabase.UserProfiles.Get(id.Value);

            if (userProfile == null)
                throw new ValidationException($"The User Profile with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<UserProfile, UserProfileDTO>(userProfile);
        }

        public IEnumerable<UserProfileDTO> GetItems()
        {
            return Mapper.Map<IEnumerable<UserProfile>, List<UserProfileDTO>>(dimsDatabase.UserProfiles.GetAll());
        }

        public void SaveItem(UserProfileDTO userProfile)
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
                StartDate = userProfile.StartDate
            };

            dimsDatabase.UserProfiles.Create(_userProfile);
            dimsDatabase.Save();
        }

        public void UpdateItem(UserProfileDTO userProfile)
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

            var _userProfile = dimsDatabase.UserProfiles.Get(userProfile.UserId);

            var userTasks = this.userTasks.GetByUserId(userProfile.UserId);
            
            if (_userProfile != null)
            {
                Mapper.Map<UserProfileDTO, UserProfile>(userProfile, _userProfile);
                //_userProfile.UserTasks = Mapper.Map<IEnumerable<UserTaskDTO>, ICollection<UserTask>>(userTasks);

                dimsDatabase.Save();
            }

        }

    
    }
}
