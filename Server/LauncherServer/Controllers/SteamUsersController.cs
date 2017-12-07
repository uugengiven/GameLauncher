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

namespace LauncherServer.Controllers
{
    public class SteamUsersController : Controller
    {
        private LauncherDbContext db = new LauncherDbContext();
        private Encryption encryption = new Encryption(ConfigurationManager.AppSettings["CryptKey"]);

        // GET: SteamUsers
        public ActionResult Index()
        {
            return View(db.SteamUsers.ToList());
        }

        // GET: SteamUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SteamUser steamUser = db.SteamUsers.Find(id);
            if (steamUser == null)
            {
                return HttpNotFound();
            }
            return View(steamUser);
        }

        // GET: SteamUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SteamUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password")] SteamUser steamUser, string game_ids)
        {
            if (ModelState.IsValid)
            {
                string[] gameIds = game_ids.Split(',');
                steamUser.salt = encryption.getSalt(32);
                steamUser.password = encryption.Encrypt(steamUser.password, steamUser.salt);
                steamUser.password = encryption.Encrypt(steamUser.password);
                db.SteamUsers.Add(steamUser);
                db.SaveChanges();
                if (steamUser.games == null)
                {
                    steamUser.games = new List<Game>();
                }
                else
                {
                    steamUser.games.Clear();
                }
                foreach (string id in gameIds)
                {
                    steamUser.games.Add(db.Games.Find(Convert.ToInt32(id)));
                }
                db.SaveChanges();
            }
            return Redirect("Index");
        }

        // GET: SteamUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SteamUser steamUser = db.SteamUsers.Find(id);
            if (steamUser == null)
            {
                return HttpNotFound();
            }
            return View(steamUser);
        }

        // POST: SteamUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password")] SteamUser steamUser, string game_ids, bool update_password = false)
        {
            if (ModelState.IsValid)
            {
                SteamUser user = db.SteamUsers.Find(steamUser.id);
                string[] gameIds = game_ids.Split(',');
                user.username = steamUser.username;
                if (update_password)
                {
                    user.password = encryption.Encrypt(steamUser.password, user.salt);
                    user.password = encryption.Encrypt(user.password);
                }
                
                if (user.games == null)
                {
                    user.games = new List<Game>();
                }
                else
                { 
                user.games.Clear();
                    }
                foreach(string id in gameIds)
                {
                    user.games.Add(db.Games.Find(Convert.ToInt32(id)));
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(steamUser);
        }

        // GET: SteamUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SteamUser steamUser = db.SteamUsers.Find(id);
            if (steamUser == null)
            {
                return HttpNotFound();
            }
            return View(steamUser);
        }

        // POST: SteamUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SteamUser steamUser = db.SteamUsers.Find(id);
            db.SteamUsers.Remove(steamUser);
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
