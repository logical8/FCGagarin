using System.Collections.Generic;
using FCGagarin.DAL.DTO;

namespace FCGagarin.DAL.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        UserProfileDTO GetUserProfile(string identityName);

        string GetUserFirstName(string name);

        void CreateUserProfile(UserProfileDTO userProfileDTO);
    }
}