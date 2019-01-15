using BL.Interfaces;
using Common.Logger;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models.AccountModels;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        IBridge bridge;

        public AccountController(IBridge bridge)
        {
            this.bridge = bridge;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Email, Email = model.Email, RoleName = "user" };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "user");
                    Logger.Log.Info($"Был зарегистрирован новый юзер: {user.Email}");
                    return RedirectToAction("CategoryIndex", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    HttpContext.Response.Cookies["id"].Value = user.Id;
                    var context = new ApplicationContext();
                    var roles = await context.Users
                                        .Where(u => u.Id == user.Id)
                                        .SelectMany(u => u.Roles)
                                        .Join(context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
                                        .ToListAsync();
                    string role = roles[0].Name;
                    if(role == "administrator")
                    {
                        HttpContext.Response.Cookies["role"].Value = "administrator";
                    }
                    if (role == "moderator")
                    {
                        HttpContext.Response.Cookies["role"].Value = "moderator";
                    }
                    if (role == "user")
                    {
                        HttpContext.Response.Cookies["role"].Value = "user";
                    }


                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("CategoryIndex", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            HttpContext.Response.Cookies["id"].Value = "";
            HttpContext.Response.Cookies["role"].Value = "";
            return RedirectToAction("CategoryIndex", "Home");
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult AllUsers()
        {
            IEnumerable<AppUser> users = UserManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult Delete(string id)
        {
            return RedirectToAction("DeleteAccount", "Manage", new { userId = id });
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult Edit(string id)
        {
            return RedirectToAction("Edit", "Manage", new { userId = id });
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult ChangeRole(string id)
        {
            return RedirectToAction("ChangeRole", "Manage", new { userId = id });
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult UserAds(string id)
        {
            return View(bridge.GetAdsByUserId(id));
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult EditAd(string id)
        {
            return RedirectToAction("EditAd", "Home", new { adId = id });
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult DeleteAd(string id)
        {
            return RedirectToAction("DeleteAd", "Home", new { adId = id });
        }

        public ActionResult ListOfUserAds()
        {
            string userId = User.Identity.GetUserId();
            return View(bridge.GetAdsByUserId(userId));
        }

        public ActionResult EditPersonalAd(string id)
        {
            return RedirectToAction("EditAd", "Home", new { adId = id });
        }

        public ActionResult DeletePersonalAd(string id)
        {
            return RedirectToAction("DeleteAd", "Home", new { adId = id });
        }
    }
}