using ASSIGNMENTMID.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASSIGNMENTMID.Controllers
{
    public class RestaurantController : Controller
    {

        // GET: Restaurant
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["ResLog"] != null)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                var db = new zero_hungerEntities();
                var restaurants = db.Restaurants.ToList();

                return View(restaurants);    
            }
        }

        [HttpPost]
        public ActionResult Index(int resId)
        {
            if (Session["ResLog"] != null)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                return RedirectToAction("Login","Restaurant", new { id = resId });
            }
        }



        public ActionResult Login(int id)
        {
            Session["ResLog"] = 1;

            return RedirectToAction("Welcome", "Restaurant", new { id = id });



        }

        [HttpGet]
        public ActionResult Welcome(int id)
        {
            if (Session["ResLog"] == null)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            else
            {
                var db = new zero_hungerEntities();
                var restaurants = (from row in db.Restaurants
                                   where row.id == id
                                   select row).SingleOrDefault();

                return View(restaurants);
            }
        }

        [HttpPost]
        public ActionResult Welcome(Request requests)
        {
            var db = new zero_hungerEntities();
            db.Requests.Add(requests);
            db.SaveChanges();
            return RedirectToAction("Welcome");



        }



    }
}