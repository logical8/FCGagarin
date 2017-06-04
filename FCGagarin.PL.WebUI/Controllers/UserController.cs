using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FCGagarin.DAL.EF;
using FCGagarin.PL.ViewModels;
using FCGagarin.PL.WebUI.Extensions;
using FCGagarin.PL.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;
using FCGagarin.DAL.Entities;

namespace FCGagarin.PL.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ActionResult Index()
        {
            List<UsersRolesViewModel> model = new List<UsersRolesViewModel>();
            foreach (var item in UserManager.Users)
            {
                UserProfile userProfile = item.GetUserProfile();
                model.Add(new UsersRolesViewModel
                {
                    Id = userProfile.Id,
                    DateOfBirth = userProfile.DateOfBirth,
                    Email = userProfile.Email,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    GUID = userProfile.GUID,
                    Roles = ConvertToRoleViewModelList(RoleManager.GetRolesByUserId(item.Id))
                });
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            UserProfile userProfile = null;
            using (var db = new FCGagarinContext())
            {
                userProfile = db.UserProfiles.Find(id);
            }
            if (userProfile != null)
            {
                var viewModel = new UsersRolesFormModel
                {
                    Id = userProfile.Id,
                    DateOfBirth = userProfile.DateOfBirth,
                    Email = userProfile.Email,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    GUID = userProfile.GUID,
                    SelectedRoleIds = RoleManager.GetRolesByUserId(userProfile.GUID).Select(x=>x.Id),
                    Roles = ConvertToRoleViewModelList(RoleManager.Roles.ToList())
                };
                return View(viewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        private List<ApplicationRoleViewModel> ConvertToRoleViewModelList(List<ApplicationRole> roleManagerRoles)
        {
            var result = new List<ApplicationRoleViewModel>();
            foreach (var applicationRole in roleManagerRoles)
            {
                result.Add(new ApplicationRoleViewModel
                {
                    Name = applicationRole.Name
                });
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UsersRolesFormModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = new UserProfile
                {
                    Id = model.Id,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    GUID = model.GUID
                };
                using (var db = new FCGagarinContext())
                {
                    db.Entry(userProfile).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                ApplicationUser appUser = await UserManager.FindByEmailAsync(userProfile.Email);
                foreach (var role in RoleManager.Roles.ToList())
                {
                    if (model.SelectedRoleIds.Contains(role.Id))
                    {
                        await UserManager.AddToRoleAsync(appUser.Id, role.Name);
                    }
                    else
                    {
                        await UserManager.RemoveFromRoleAsync(appUser.Id, role.Name);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}