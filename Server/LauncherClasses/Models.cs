using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClasses
{
    public class SteamGame
    {
        public int id { get; set; }
        public int steamId { get { return id; } }
        public string username { get; set; }
        public string password { get; set; }
        public string exe { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ComputerStatus
    {
        public string status { get; set; }
        public string message { get; set; }
        public SteamGame game { get; set; }
    }
}
