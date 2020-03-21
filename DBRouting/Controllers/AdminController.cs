
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DBRouting.DBOpertions;
using DBRouting.Models;

namespace DBRouting.Controllers
{
    public class AdminController : Controller
    {
        private DBRouteEntities db = new DBRouteEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var roles = db.User_Roles.Where(m => m.UserID == user.ID).Select(n => n.RoleID).ToList();
            StringBuilder roleBuilder = new StringBuilder();
            var routesList = new List<int>();
            if (roleBuilder != null)
            {
                List<int> route=null;
                int roleNumber = 1;
                foreach (var role in roles)
                {
                    roleBuilder.Append(roleNumber.ToString()+". "+db.Master_Roles.Where(m => m.RoleID == role).Select(n => n.RoleName)
                                           .FirstOrDefault() + " ");
                    route = db.Roles_Controller.Where(m => m.RoleID == role).Select(n => n.ControllerID).ToList();
                    routesList.AddRange(route);
                    roleNumber++;
                }
                StringBuilder routeBuilder=new StringBuilder();
                if (routesList != null)
                {
                    int routeNumber=1;
                    foreach (var controller in routesList)
                    {
                        
                        routeBuilder.Append(routeNumber.ToString()+". "+db.RouteTables.Where(m => m.RouteID == controller).Select(m => m.Route)
                                                .FirstOrDefault() + "\n\n");
                        routeNumber++;
                    }
                }
                routesList.Count();
                ViewBag.RouteString = routeBuilder.ToString();

                ViewBag.Roles = roleBuilder.ToString();
            }


            return View(user);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,EmailID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult AddRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserRole userRole=new UserRole();
            userRole.Id = user.ID;
            userRole.EmailId = user.EmailID;

            ViewBag.Roles = db.Master_Roles.ToList();
            return View(userRole);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,EmailID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult AddRole([Bind(Include = "Id,EmailId,Role")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                AddUserRole.AssignRole(userRole);
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Roles()
        {
            ControllerList controllerList=new ControllerList();
            ViewBag.Controller = db.RouteTables.ToList();
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
