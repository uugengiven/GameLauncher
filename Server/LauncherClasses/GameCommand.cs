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

        public bool isGameRunning(string exe)
        {
            var bleh = System.Diagnostics.Process.GetProcesses().ToList();
            return (System.Diagnostics.Process.GetProcesses()
                             .Where(x => x.ProcessName.ToLower()
                                          .Contains(exe))
                             .ToList().Count > 0);
        }

        public void StopSteam()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "taskkill";
            startInfo.Arguments = "/F /IM steam.exe";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process procTemp = new Process();
            procTemp.StartInfo = startInfo;
            procTemp.EnableRaisingEvents = true;
            procTemp.Start();

            //Process.Start("taskkill", "/F /IM steam.exe");
        }

        public SteamGame GetSteamLogin(int id, string URL, string computer_key, string secret)
        {
            SteamGame game = new SteamGame();
            game.id = id;
            
            var values = new Dictionary<string, string>
                {
                   { "computer_key", computer_key },
                   { "current_time", DateTime.Now.ToString()}
                };

            dynamic obj = GetWebResponse(URL, values);
            game.status = obj.status;
            game.message = obj.message; 

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

        public bool CheckinUser(string URL, string computer_key)
        {
            var values = new Dictionary<string, string>
                {
                   { "computer_key", computer_key },
                   { "current_time", DateTime.Now.ToString()}
                };

            dynamic obj = GetWebResponse(URL, values);
            return true;
        }

        public dynamic GetWebResponse(string URL, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(data);

            var response = client.PostAsync(URL, content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<dynamic>(responseString);
        }
    }
}
