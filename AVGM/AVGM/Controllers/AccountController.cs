using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;

namespace AVGM.Controllers
{
    public class AccountController : Controller

    {
        // GET: Account/Register

        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Register(string username, string password)
        {
            var userStore = new Microsoft.AspNet.Identity.EntityFramework.UserStore<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>();
            var manager = new Microsoft.AspNet.Identity.UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>(userStore);
            var user = new Microsoft.AspNet.Identity.EntityFramework.IdentityUser() { UserName = username };
            Microsoft.AspNet.Identity.IdentityResult result = await manager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                //I have some options: log them in, or I can send them an email to "Confirm" their account details.'
                //I don't have email set up this week, so we'll come back to that.
                //This authentication manager will create a cookie for the current user, and that cookie will be exchanged on each request until the user logs out

                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await manager.CreateIdentityAsync(user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { }, userIdentity);
            }
            else
            {
                ViewBag.Error = result.Errors;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string UserName, string Password, bool? staySignedIn)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

            var user = userManager.FindByName(UserName);
            if (user != null)
            {
                bool isPasswordValid = userManager.CheckPassword(user, Password);
                if (isPasswordValid)
                {
                    var claimsIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                    {
                        IsPersistent = staySignedIn ?? false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }, claimsIdentity);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = new string[] { "Unable to sign in" };
            return View();
        }

        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}