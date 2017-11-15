using LauncherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LauncherClient.Owin.Controllers
{
    public class GamesController : ApiController
    {
        private GameCommand gc = new GameCommand();

        [HttpGet]
        public string StartGame(int id)
        {

            string URL = $"http://localhost:61016/game/checkout/{id}";
            SteamGame game = gc.GetSteamLogin(id, URL, "12345", "");

            gc.StartSteam(game);
            return "ok";
        }

        [HttpGet]
        public string Test()
        {
            return "ok";
        }
    }
}
