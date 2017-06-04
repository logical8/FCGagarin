using System;
using System.Data.Entity;
using FCGagarin.BLL.Models;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Repositories.Interfaces;
using FCGagarin.DAL.Entities;

namespace FCGagarin.BLL.Services
{
    public class UserProfileService : EntityService<UserProfile>, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository, DbContext context) : base(userProfileRepository, context)
        {
            _userProfileRepository = userProfileRepository;
        }
        
        public UserProfileModel GetUserProfile(string identityName)
        {
            var userProfile = _userProfileRepository.GetUserProfile(identityName);
            return ConvertToUserProfileModel(userProfile);
        }

        public void CreateUserProfile(UserProfileModel userProfile)
        {
            var userProfileDTO = ConvertToUserProfileDTO(userProfile);
            _userProfileRepository.CreateUserProfile(userProfileDTO);
        }

        private UserProfileModel ConvertToUserProfileModel(UserProfileDTO userProfileDto)
        {
            throw new NotImplementedException();
        }

        private UserProfileDTO ConvertToUserProfileDTO(UserProfileModel userProfile)
        {
            throw new NotImplementedException();
        }
    }
}