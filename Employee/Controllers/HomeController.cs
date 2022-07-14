using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee.Models;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        ChandruEntities db = new ChandruEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Emp_Inf.ToList());
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Emp_Inf emp_Inf)
        {
            if (db.Emp_Inf.Any(x => x.Emp_name == emp_Inf.Emp_name))
            {
                ViewBag.Notification = "This Account is already exist";
                return View();
            }
            else
            {
                db.Emp_Inf.Add(emp_Inf);
                db.SaveChanges();

                Session["IdEmpSS"] = emp_Inf.Emp_id.ToString();
                Session["EmpNameSS"] = emp_Inf.Emp_name.ToString();
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Emp_Inf emp_Inf)
        {
            var checkLogin = db.Emp_Inf.Where(x => x.Emp_name.Equals(emp_Inf.Emp_name) && x.Emp_password.Equals(emp_Inf.Emp_password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdEmpSS"] = emp_Inf.Emp_id.ToString();
                Session["EmpNameSS"] = emp_Inf.Emp_name.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Username or password";
            }
            return View();
        }

    }
}
