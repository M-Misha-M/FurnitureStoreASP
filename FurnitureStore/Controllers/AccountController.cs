using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using FurnitureStore.Models;
using FurnitureStore.ModelView;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace FurnitureStore.Controllers
{
     [Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        DataBaseEntity context;


        public AccountController()
        {
            context = new DataBaseEntity();
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                _signInManager = value;
            }
        }




        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


      
        public ActionResult Register()
        {
            ViewBag.Name = new SelectList(context.Roles
                                            .ToList(), "Name", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegistryModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Year = model.Year };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    //Ends Here 
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Name = new SelectList(context.Roles
                                          .ToList(), "Name", "Name");

            }
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model , string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    var user = await UserManager.FindAsync(model.UserName, model.Password);
                    var roles = await UserManager.GetRolesAsync(user.Id);

                    if (roles.Contains("admin"))
                    {
                        return RedirectToAction("Index", "Furnitures");
                    }
                   
                    else if (roles.Contains("user"))
                    {
                        return RedirectToAction("About", "Home");
                    }
                    else
                    {
                        return RedirectToLocal(returnUrl);
                    }

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Невірний логін або пароль, спробуйте знову");
                    return View(model);
            }
        }

      


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("List", "Furniture");
        }



        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion
    }
}