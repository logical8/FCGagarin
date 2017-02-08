using FCGagarin.WebUI.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCGagarin.WebUI.Extensions;
using System.Threading.Tasks;
using FCGagarin.WebUI.Models;

namespace FCGagarin.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

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
                    Roles = RoleManager.GetRolesByUserId(item.Id)
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
                    Roles = RoleManager.Roles
                };
                return View(viewModel);
            }
            else
            {
                return HttpNotFound();
            }
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