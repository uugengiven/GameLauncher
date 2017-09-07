using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TestSteamLauncher.classes;


namespace TestSteamLauncher
{
    public class GamesController : ApiController
    {
        private GameCommand gc = new GameCommand();

        [HttpGet]
        public string StartGame(int id)
        {
            // do some look up for a user/pass
            gc.StartSteam("LFGDeadbydaylight02", "924Brookline", 381210);
            return "Started!";
        }
    }
}
