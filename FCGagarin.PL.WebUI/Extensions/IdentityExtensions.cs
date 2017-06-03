using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.WebUI.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FCGagarin.PL.WebUI.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            using (var db = new FCGagarinContext())
            {
                try
                {
                    var firstName = db.UserProfiles.First(x => x.Email == identity.Name).FirstName;
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
                UserProfile userProfile = db.UserProfiles.FirstOrDefault(x => x.Email == identity.Name);
                if (userProfile != null)
                {
                    return userProfile;
                }
                else
                {
                    throw new Exception("Error in IdentityExtensions");
                }
            }
        }

        public static UserProfile GetUserProfile(this ApplicationUser applicationUser)
        {
            using (var db = new FCGagarinContext())
            {
                UserProfile userProfile = db.UserProfiles.FirstOrDefault(x => x.Email == applicationUser.Email);
                if (userProfile != null)
                {
                    return userProfile;
                }
                else
                {
                    throw new Exception("Error in IdentityExtensions");
                }
            }
        }

        public static List<ApplicationRole> GetRolesByUserId(this ApplicationRoleManager manager, string userId)
        {
            List<IdentityRole> identityRoles;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.Find(userId);
                identityRoles = db.Roles.Where(x => x.Users.Select(y => y.UserId).Contains(user.Id)).ToList();
                
            }
            var result = identityRoles.SelectMany(x => manager.Roles.Where(z => z.Id == x.Id)).ToList();
            return result;
        }
    }
}