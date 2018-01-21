using AVGM.DAL;
using AVGM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVGM.Controllers
{
    public class AccountController : Controller
    {

        protected SchoolContext db = new SchoolContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            //If they came to the page by url without being signed in, kick em out
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account");
            }
            
            Guardian thisUser = db.Guardians.First(m => m.Email == User.Identity.Name);
            return View(thisUser);
        }

        [HttpPost]
        public ActionResult ChangeImage(string Email)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ImageUploader uploader = new ImageUploader();
            int i = uploader.UploadTheGuardianImage(file, Email);

            return (i == 1 ? RedirectToAction("Index", "Account") : RedirectToAction("Index","Home"));
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Apply()
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
            ViewBag.Error = new string[] { "Username or Password are incorrect." };
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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if (user != null)
            {
                string resetToken = userManager.GeneratePasswordResetToken(user.Id);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword?email=" + email + "&token=" + resetToken;
                string link = string.Format("<a href=\"{0}\">Reset your password</a>", resetUrl);
                //userManager.SendEmail(user.Id, "your password reset token", message);
                EmailMessageMaker forgotPasswordEmail = new EmailMessageMaker();
                forgotPasswordEmail.Line.Add("<h2>Thank you for contacting us!</h2>");
                forgotPasswordEmail.Line.Add("<p>To reset your password, please click on the link below.</p>");
                forgotPasswordEmail.Line.Add(link);
                forgotPasswordEmail.Line.Add("<p>Have a great day!</p>");
                MailerService passwordReset = new MailerService(user.Email, "Instructions to reset your VGM password", forgotPasswordEmail);
                passwordReset.SendMail();
            }

            return RedirectToAction("ForgotPasswordSent");
        }

        public ActionResult ForgotPasswordSent()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email, string token, string newPassword)
        {

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if (user != null)
            {
                IdentityResult result = userManager.ResetPassword(user.Id, token, newPassword);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Your password has been updated successfully";
                    return RedirectToAction("SignIn", "Account");
                }

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Student(int identifier, string FName, string LName)
        {
            Guid tempGuid = new Guid();
            var students = db.Students;
            foreach(var student in students)
            {
                var identifierChecker = student.StudentID.GetHashCode();
                if (identifierChecker == identifier && student.LName == LName && student.FName == FName)
                {
                    tempGuid = student.StudentID;
                }
            }
            var child = db.Students.First(m => m.StudentID == tempGuid);
            return View(child);
        }

        public ActionResult EditProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account");
            }
            Guardian thisUser = db.Guardians.First(m => m.Email == User.Identity.Name);
            return View(thisUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(string identity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account");
            }
            Guardian thisUser = db.Guardians.First(m => m.Email == User.Identity.Name);
            return View(thisUser);
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}