using System;
using System.Data.Entity;
using System.Linq;
using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Repositories.Interfaces;
using FCGagarin.DAL.Entities;

namespace FCGagarin.DAL.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DbContext context) : base(context)
        {
        }

        public UserProfileDTO GetUserProfile(string name)
        {
            var userProfile = _dbSet.FirstOrDefault(x => x.Email == name);
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
            _dbSet.Add(userProfile);
        }

        private UserProfile ConvertToUserProfile(UserProfileDTO userProfileDTO)
        {
            throw new NotImplementedException();
        }

    }
}