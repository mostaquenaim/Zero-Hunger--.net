using ASSIGNMENTMID.DB;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASSIGNMENTMID.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {

            if (Session["Login"] != null)
            {
                return RedirectToAction("Welcome", "Admin");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string uname,string pass)
        {
            if(uname=="Admin" && pass == "Admin")
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            Session["Login"] = 1;

            return RedirectToAction("Welcome", "Admin");


        }

        public ActionResult Welcome()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult Addemployee()
        {

            var db = new zero_hungerEntities();
            var employees = db.Employees.ToList();



            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {

                return View(employees);
            }
        }

        [HttpPost]
        public ActionResult Addemployee(Employee employees)
        {
            var db = new zero_hungerEntities();
            db.Employees.Add(employees);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Checkreq()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var db = new zero_hungerEntities();
                var requests = db.Requests.ToList();

               

                return View(requests);
            }
        }

        [HttpGet]
        public ActionResult Assignem(int id)
        {
            

            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            var db = new zero_hungerEntities();
            var employees = db.Employees.ToList();
            return View(employees);

            
            
        }

        [HttpPost]
        public ActionResult Assignem( Request request, Employee employee, int Id, int EmId)
        {
            var db = new zero_hungerEntities();
            var req = (from row in db.Requests
                        where row.id == Id
                        select row).SingleOrDefault();

            request = req;
            request.Employeeid = EmId;
            db.Entry(req).CurrentValues.SetValues(request);
            db.SaveChanges();

            var emp = (from row in db.Employees
                       where row.id == EmId
                       select row).SingleOrDefault();

            employee = emp;
            
            db.Entry(emp).CurrentValues.SetValues(employee);
            db.SaveChanges();

            return RedirectToAction("Checkreq");

        }

        public ActionResult Seehistory()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var db = new zero_hungerEntities();
                var requests = db.Requests.ToList();



                return View(requests);
            }
        }

        public ActionResult Deletehistory(int id)
        {
            Request req = new Request() { id = id };

            using (var context = new zero_hungerEntities())
            {
                context.Requests.Attach(req);
                context.Requests.Remove(req);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["Login"] = null;
            return RedirectToAction("Index");
        }
    }

}