using LauncherServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jose;
using System.Security.Cryptography;
using LauncherServerClasses;
using System.Configuration;
using Newtonsoft.Json;

namespace LauncherServer.Controllers
{
    public class GameController : Controller
    {
        LauncherDbContext db = new LauncherDbContext();
        Encryption encryption = new Encryption(ConfigurationManager.AppSettings["CryptKey"]); 

        // GET: Game
        public ActionResult Index()
        {
            var gameList = db.Games.ToList();
            List<GameViewModel> finalList = new List<GameViewModel>();

            foreach(var game in gameList)
            {
                GameViewModel temp = new GameViewModel();
                temp.exe = game.exe;
                temp.name = game.name;
                temp.steamId = game.steamId;
                temp.id = game.id;
                temp.genre = game.genre;
                finalList.Add(temp);
            }
           // var noUserInfoGameList = db.gameList.ToList(); 
                // var finalGameList = some loop over gamelist and fill in the finalGameList

            //SerializeObject allows us to convert something into Json (Derulo).
            string result = JsonConvert.SerializeObject(finalList, 
              new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            return Content(result, "application/json");
         }

        public JsonResult checkout( string computer_key, string current_time,  int security_code = 0, int id = 0)
        {
            // List of checks to verify that a user can be checked out
            // computer key exists - done
            // computer key is enabled - done
            // computer key does not have another game checked out - done
            // time is within given parameters
            // game exists
            // user with game is available

            if (id > 0)
            {
                var computer = db.Computers.Where(x => x.key == computer_key).FirstOrDefault();

                if (computer != null) // computer key exists check
                {
                    if (computer.authorized == false) // computer authorized check
                    {
                        var output = new StatusViewModel();
                        output.status = "failed";
                        output.message = "computer not authorized. please contact administrator";
                        return new JsonResult() { Data = output, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    if (db.SteamUsers.Where(x=> x.inUseBy.id == computer.id).Count() > 0) // no users already checked out check
                    {
                        var output = new StatusViewModel();
                        output.status = "failed";
                        output.message = "This computer already has a user checked out. please contact administrator";
                        return new JsonResult() { Data = output, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    var time = Convert.ToDateTime(current_time);

                    if (CheckTime(current_time))
                    {
                        var game = db.Games.Where(x => x.steamId == id).First();
                        var user = db.SteamUsers.Where(x => x.games.Any(g => g.id == game.id) && x.inUse == false).FirstOrDefault();

                        if (user != null)
                        {
                            var output = new GameStartViewModel();
                            output.exe = game.exe;
                            output.steamId = game.steamId;
                            output.name = game.name;
                            output.username = user.username;
                            output.password = encryption.Decrypt(user.password);
                            output.password = encryption.Decrypt(output.password, user.salt);
                            output.status = "ok";
                            user.inUse = true;
                            user.inUseBy = db.Computers.Where(x => x.key == computer_key).FirstOrDefault();
                            db.SaveChanges();
                            return new JsonResult() { Data = output, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {
                            var output = new StatusViewModel();
                            output.status = "failed";
                            output.message = "no users available";
                            return new JsonResult() { Data = output, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                    }
                    else
                    {
                        var output = new StatusViewModel();
                        output.status = "failed";
                        output.message = "request timing is off";
                        return new JsonResult() { Data = output, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
                else
                {
                    var output = new StatusViewModel();
                    output.status = "failed";
                    output.message = "computer not authorized. please contact administrator";
                    return new JsonResult() { Data = output, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

            }
            else
            {
                return new JsonResult() { Data = "invalid game request", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult checkin(string computer_key, string current_time)
        {
            

            if (CheckTime(current_time))
            {
                var computer = db.Computers.Where(x => x.key == computer_key).FirstOrDefault();
                var users = db.SteamUsers.Where(x => x.inUseBy.id == computer.id).ToList();

                foreach (var i in users)
                {
                    i.inUse = false;
                    i.inUseBy = null;
                }
                db.SaveChanges();
                var output = new StatusViewModel();
                output.status = "success";
                output.message = "checked in" + " " + users.Count.ToString()+ " " + "users";
                return new JsonResult() { Data = output };
            }
            else
            {
                var output = new StatusViewModel();
                output.status = "failed";
                output.message = "request timing is off";
                return new JsonResult() { Data = output };
            }
        }
      //Remember to Add in List of Users to include
        [HttpPost]
        public string CreateGame ([Bind(Include = "steamId,name,exe")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return "game added";
            }

            return "could not add game";
        }

        public string DeleteGame (int id)
        {
            Game game = db.Games.Find(id);
            if (game != null)
            {
                db.Games.Remove(game);
                db.SaveChanges();

                return "game deleted";
            }

            return "error: game not found";
        }

        public string CreateSteamUser ([Bind(Include = "username,password")] SteamUser steamuser)
        {
            if (ModelState.IsValid)
            {
                db.SteamUsers.Add(steamuser);
                db.SaveChanges();
                return "User added";
            }

            return "could not add User";
        }

        public string DeleteSteamUser (int id)
        {
            SteamUser steamuser = db.SteamUsers.Find(id);
            if (steamuser != null)
            {
                db.SteamUsers.Remove(steamuser);
                db.SaveChanges();

                return "User added";
            }
            return "error: User not found";
        }


        public string CreateComputer([Bind(Include = "name,ip,key,authorized")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                db.Computers.Add(computer);
                db.SaveChanges();
                return "Computer added";
            }

            return "could not add Computer";
        }

        public string DeleteComputer(int id)
        {
            Computer computer = db.Computers.Find(id);
            if (computer != null)
            {
                db.Computers.Remove(computer);
                db.SaveChanges();

                return "Computer added";
            }
            return "error: Computer not found";
        }
        // GET:
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Setup()
        {

            //var game = new Game();
            //game.exe = "dota2.exe";
            //game.name = "dota 2";
            //game.steamId = 570;
            //game.users = new List<SteamUser>();
            //db.Games.Add(game);
            //db.SaveChanges();

            //var su = new SteamUser();
            //su.username = "somedota";
            //su.password = encryption.Encrypt("dota2");
            //su.games = new List<Game>();
            //su.games.Add(game);
            //db.SteamUsers.Add(su);

            //var su1 = new SteamUser();
            //su1.username = "lfgdeadbydaylight02";
            //su1.password = Encrypt("asdf");
            //su1.games = new List<Game>();
            //db.SteamUsers.Add(su1);


            //var game = db.Games.Find(3);
            //game.name = "Dead by Daylight";
            //game.steamId = 381210;
            //game.exe = "deadbydaylight.exe";
            //game.users = new List<SteamUser>();
            //db.Games.Add(game);
            //db.SaveChanges();

            //su.games.Add(game);
            //su1.games.Add(game);
            //db.SaveChanges();



            //var user = db.SteamUsers.Find(1);
            //user.password = encryption.Encrypt("mwk318");

            foreach (var u in db.SteamUsers)
            {
                u.inUse = false;
                u.inUseBy = null;
            }
            db.SaveChanges();

            return "ok";
        }

        private bool CheckTime(string string_time)
        {
            var time = Convert.ToDateTime(string_time);
            return DateTime.Now <= (time.AddSeconds(10000)) && DateTime.Now >= (time.AddSeconds(-10000));
        }


    }
}