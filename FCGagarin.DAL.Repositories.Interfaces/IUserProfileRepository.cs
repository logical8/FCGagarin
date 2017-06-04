using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Entities;

namespace FCGagarin.DAL.Repositories.Interfaces
{
    public interface IUserProfileRepository : IGenericRepository<UserProfile>
    {
        UserProfileDTO GetUserProfile(string identityName);

        string GetUserFirstName(string name);

        void CreateUserProfile(UserProfileDTO userProfileDTO);
    }
}