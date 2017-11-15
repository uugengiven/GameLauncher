using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClasses
{
    public class GameCommand
    {
        public Process StartSteam(SteamGame game)
        {
            const string regKey = @"HKEY_CURRENT_USER\Software\Valve\Steam";
            string steamExe = Registry.GetValue(regKey, "SteamExe", "").ToString();
            return Process.Start(steamExe, $"-login {game.username} {game.password} -applaunch {game.id}");
        }

        public void StopSteam()
        {
            Process.Start("taskkill", "/F /IM steam.exe");
        }

        public SteamGame GetSteamLogin(int id, string URL, string computer_key, string secret)
        {
            SteamGame game = new SteamGame();
            game.id = id;
            
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
                {
                   { "computer_key", computer_key },
                   { "current_time", DateTime.Now.ToString()}
                };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync(URL, content).Result;



            var responseString = response.Content.ReadAsStringAsync().Result;
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(responseString);
            game.status = obj.status;
            if (game.status == "ok")
            {
                // do some look up for a user/pass
                game.username = obj.username;
                game.password = obj.password;
                game.exe = obj.exe;
                game.name = obj.name;

                // web request to server to return valid user/pass

                // Hit the server application at /api/checkout/{id}
                // Once user/pass is returned, plug them into the StartSteam call

                
            }

            return game;
        }
    }
}
