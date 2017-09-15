using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            string URL = $"http://localhost:61016/game/checkout/{id}";
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
                {
                   { "computer_key", "12345" },
                   { "current_time", DateTime.Now.ToString() }
                };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync(URL, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(responseString);


            // do some look up for a user/pass
            string user = obj.username;
            string pass = obj.password;

            // web request to server to return valid user/pass

            // Hit the server application at /api/checkout/{id}
            // Once user/pass is returned, plug them into the StartSteam call
            
            gc.StartSteam(user, pass, id);
            return "Started!";
        }
    }
}
