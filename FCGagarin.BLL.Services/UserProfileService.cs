using System;
using FCGagarin.BLL.Models;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
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

        private UserProfileDTO ConvertToUserProfileDTO(UserProfileModel userProfile)
        {
            throw new NotImplementedException();
        }

        private UserProfileModel ConvertToUserProfileModel(UserProfileDTO userProfileDto)
        {
            throw new NotImplementedException();
        }
    }
}