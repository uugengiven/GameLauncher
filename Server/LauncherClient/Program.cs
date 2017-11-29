using LauncherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LauncherClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Launcher());
        }
    }

    static class LauncherInfo
    {
        static public SteamGame game;
        static public bool gameIsNew = false;

        static public void StartGame(SteamGame game)
        {
            LauncherInfo.game = game;
            gameIsNew = true;
        }

        static public void StopGame()
        {
            LauncherInfo.game = null;
            gameIsNew = false;
        }
    }
}
