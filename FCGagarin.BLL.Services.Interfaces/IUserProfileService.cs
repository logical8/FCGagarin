using FCGagarin.BLL.Models;

namespace FCGagarin.BLL.Services.Interfaces
{
    public interface IUserProfileService
    {
        UserProfileModel GetUserProfile(string identityName);

        void CreateUserProfile(UserProfileModel model);
    }
}