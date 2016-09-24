using FCGagarin.DAL.Concrete;
using FCGagarin.Domain.Model;
using FCGagarin.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                UserProfile userProfile = db.UserProfiles.Where(x => x.Email == applicationUser.Email).FirstOrDefault();
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
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.Find(userId);
                identityRoles = db.Roles.Where(x => x.Users.Select(y => y.UserId).Contains(user.Id)).ToList();
                
            }
            List<ApplicationRole> result = identityRoles.SelectMany(x => manager.Roles.Where(z => z.Id == x.Id)).ToList();
            return result;
        }
    }
}