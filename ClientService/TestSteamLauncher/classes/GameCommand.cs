using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSteamLauncher.classes
{
    class GameCommand
    {
        public Process StartSteam(string user, string pass, int gameId)
        {
            const string regKey = @"HKEY_CURRENT_USER\Software\Valve\Steam";
            string steamExe = Registry.GetValue(regKey, "SteamExe", "").ToString();
            return Process.Start(steamExe, $"-login {user} {pass} -applaunch {gameId}");
        }
    }
}
