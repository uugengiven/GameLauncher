using LauncherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;

namespace LauncherClient.Owin.Controllers
{
    public class GamesController : ApiController
    {
        private GameCommand gc = new GameCommand();

        [HttpGet]
        public string StartGame(int id)
        {
            string baseURL = ConfigurationManager.AppSettings["BaseURL"];
            string computerKey = ConfigurationManager.AppSettings["ComputerKey"];

            string URL = $"{baseURL}/game/checkout/{id}";
            SteamGame game = gc.GetSteamLogin(id, URL, computerKey, "");
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

        [HttpGet]
        public string Checkin()
        {
            string baseURL = ConfigurationManager.AppSettings["BaseURL"];
            string computerKey = ConfigurationManager.AppSettings["ComputerKey"];

            string URL = $"{baseURL}/game/checkin";
            gc.CheckinUser(URL, computerKey);
            return "ok";
        }

        [HttpGet]
        public string Status()
        {
            return "running";
        }
    }
}
