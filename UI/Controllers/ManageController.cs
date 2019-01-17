using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models.AccountModels;

namespace UI.Controllers
{
    public class ManageController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public async Task<ActionResult> AccountIndex()
        {
            var userId = User.Identity.GetUserId();
            var model = new AccountIndexViewModel
            {
                Id = userId,
                Email = await UserManager.GetEmailAsync(userId)
            };
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                return RedirectToAction("CategoryIndex", "Home");
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteAccount(string userId = "nodel")
        {
            if(userId == "nodel")
            {
                AppUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    IdentityResult result = await UserManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Logout", "Account");
                    }
                }
            }
            else
            {
                AppUser user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    IdentityResult result = await UserManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AllUsers", "Account");
                    }
                }
            }
            
            return RedirectToAction("CategoryIndex", "Home");
        }

        [Authorize(Roles = "administrator, moderator")]
        [HttpGet]
        public async Task<ActionResult> Edit(string userId = "noedit")
        {
            if(userId == "noedit")
            {
                AppUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    return View(user);
                }
            }
            else
            {
                AppUser user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    return View(user);
                }
            }
            
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "administrator, moderator")]
        [HttpPost]
        public async Task<ActionResult> Edit(AppUser model)
        {
            if (model != null)
            {
                IdentityResult result = await UserManager.UpdateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        [Authorize(Roles = "administrator")]
        [HttpGet]
        public async Task<ActionResult> ChangeRole(string userId)
        {
            AppUser user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                List<string> roles = new List<string>();
                foreach(var item in RoleManager.Roles)
                {
                    roles.Add(item.Name);
                }
                ViewBag.Roles = roles;
                return View(user);
            }

            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "administrator")]
        [HttpPost]
        public async Task<ActionResult> ChangeRole(AppUser model)
        {
            if (model != null)
            {
                AppUser user = await UserManager.FindByIdAsync(model.Id);
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    IdentityResult resultOfCreate = await UserManager.CreateAsync(model);
                    if (resultOfCreate.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(model.Id, model.RoleName);
                        return RedirectToAction("AllUsers", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
    }
}