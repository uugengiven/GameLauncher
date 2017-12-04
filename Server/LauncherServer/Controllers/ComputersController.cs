using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LauncherServer.Models;
using LauncherServerClasses;
using System.Configuration;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace LauncherServer.Controllers
{
    public class ComputersController : Controller
    {
        private LauncherDbContext db = new LauncherDbContext();
        private Encryption encryption = new Encryption(ConfigurationManager.AppSettings["CryptKey"]);

        // GET: GetSecret
        public ActionResult GetSecret(string computer_key, string user, string pass)
        {
            var result = new StatusViewModel();
            var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var dbUser = userManager.FindByEmailAsync(user).Result;



            if (userManager.CheckPasswordAsync(dbUser, pass).Result)
            {
                // should check if it is an admin here too
                var computer = db.Computers.Where(c => c.key == computer_key).FirstOrDefault();
                if (computer != null)
                {
                    result.status = "ok";
                    result.message = computer.secret;
                }
                else
                {
                    result.status = "error";
                    result.message = "Key doesn't match computer";
                }
            }
            else
            {
                result.status = "error";
                result.message = "Invalid user/pass";
            }
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Computers
        public ActionResult Index()
        {
            return View(db.Computers.ToList());
        }

        // GET: Computers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // GET: Computers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,ip,authorized")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                computer.key = encryption.GetUniqueKey(16);
                computer.secret = encryption.getSalt(32);
                db.Computers.Add(computer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(computer);
        }

        // GET: Computers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,ip,key,authorized")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(computer);
        }

        // GET: Computers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Computer computer = db.Computers.Find(id);
            db.Computers.Remove(computer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
