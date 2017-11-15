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
            SteamGame game = gc.GetSteamLogin(id, URL, "SPGUWbS0csxjk6lk", "");
            if (game.status == "ok")
            {
                LauncherInfo.StartGame(game);

                gc.StartSteam(LauncherInfo.game);
            }
            return game.status;
        }

        [HttpGet]
        public string Test()
        {
            return "ok";
        }
    }
}
