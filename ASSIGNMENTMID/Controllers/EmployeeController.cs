using ASSIGNMENTMID.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASSIGNMENTMID.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["EmpLog"] != null)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                var db = new zero_hungerEntities();
                var employees = db.Employees.ToList();

                return View(employees);
            }
        }

        [HttpPost]
        public ActionResult Index(int emId)
        {
            if (Session["EmpLog"] != null)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                return RedirectToAction("Login", "Employee", new { id = emId });
            }
        }

        public ActionResult Login(int id)
        {
            Session["EmpLog"] = 1;

            return RedirectToAction("Welcome", "Employee", new { id = id });
        }

        [HttpGet]
        public ActionResult Welcome(int id)
        {
            if (Session["EmpLog"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var db = new zero_hungerEntities();
                var req= db.Requests.ToList();
                var requests = (from row in db.Requests
                                   where row.Employeeid == id 
                                   select row).ToList();

                return View(requests);
            }
        }

        public ActionResult Collected(int id)
        {
            if (Session["EmpLog"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var db = new zero_hungerEntities();
                
                var requests = (from row in db.Requests
                                where row.id == id
                                select row).FirstOrDefault();

                requests.isCollected = "Yes";

                int empId = (int)requests.Employeeid;
                db.SaveChanges();

                return RedirectToAction("Welcome", "Employee", new { id = empId });
            }
        }

        public ActionResult Delivered(int id)
        {
            if (Session["EmpLog"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var db = new zero_hungerEntities();

                var requests = (from row in db.Requests
                                where row.id == id
                                select row).FirstOrDefault();

                requests.isDelivered = "Yes";

                int empId = (int)requests.Employeeid;
                db.SaveChanges();

                return RedirectToAction("Welcome", "Employee", new { id = empId });
            }
        }

    }
}