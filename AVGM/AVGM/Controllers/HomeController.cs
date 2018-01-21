using AVGM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVGM.Controllers
{
    public class HomeController : Controller
    {

        protected SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View(db);
        }

        [ChildActionOnly]
        public ActionResult NavBar()
        {
            if (User.Identity.IsAuthenticated)
            {
                var navbar = db.Guardians.First(m => m.Email == User.Identity.Name);
                return PartialView("_navBar", navbar);
            }
            else
            {
                return PartialView("_navBar");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}