using FCGagarin.DAL.Concrete;
using FCGagarin.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FCGagarin.WebUI.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            using (var db = new FCGagarinContext())
            {
                string firstName;
                try
                {
                    firstName = db.UserProfiles.Where(x => x.Email == identity.Name).First().FirstName;
                    return firstName;
                }
                catch (Exception)
                {
                    return identity.Name;
                }
            }
        }

        public static UserProfile GetUserProfile(this IIdentity identity)
        {
            using (var db = new FCGagarinContext())
            {
                UserProfile userProfile = db.UserProfiles.Where(x => x.Email == identity.Name).FirstOrDefault();
                return userProfile;
            }
        }

    }
}