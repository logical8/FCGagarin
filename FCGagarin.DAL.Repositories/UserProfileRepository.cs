using System;
using System.Linq;
using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Repositories.Interfaces;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;

namespace FCGagarin.DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly FCGagarinContext _context;

        public UserProfileRepository(FCGagarinContext context)
        {
            _context = context;
        }

        public UserProfileDTO GetUserProfile(string name)
        {
            var userProfile = _context.UserProfiles.FirstOrDefault(x => x.Email == name);
            return ConvertToUserProfileDTO(userProfile);
        }

        private UserProfileDTO ConvertToUserProfileDTO(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public string GetUserFirstName(string name)
        {
            return GetUserProfile(name)?.FirstName;
        }

        public void CreateUserProfile(UserProfileDTO userProfileDTO)
        {
            var userProfile = ConvertToUserProfile(userProfileDTO);
            _context.UserProfiles.Add(userProfile);
        }

        private UserProfile ConvertToUserProfile(UserProfileDTO userProfileDTO)
        {
            throw new NotImplementedException();
        }

    }
}