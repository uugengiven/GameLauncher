﻿using LauncherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;
using System.Web.Http.Results;

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
            LauncherInfo.StopGame();
            return "ok";
        }

        [HttpGet]

        public ComputerStatus Status()
        {
            var status = new ComputerStatus();
            if (LauncherInfo.game == null)
            {
                status.status = "ready";
                status.message = "Hi there";
            }
            else
            {
                status.status = "running";
                status.message = "Currently running a game";
                status.game = new SteamGame();
                status.game.name = LauncherInfo.game.name;
                status.game.id = LauncherInfo.game.id;
            }
            
            return status;
        }
    }
}
