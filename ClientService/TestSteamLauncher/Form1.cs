using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using TestSteamLauncher.classes;
using Microsoft.Owin.Hosting;
using Microsoft.Win32;

namespace TestSteamLauncher
{
    public partial class Form1 : Form
    {
        Process CurrentProcess = null;
        GameCommand gc = new GameCommand();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentProcess = gc.StartSteam("LFGDeadbydaylight", "924Brookline", 381210);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string regKey = @"HKEY_CURRENT_USER\Software\Valve\Steam";
            lblSteamLoc.Text = Registry.GetValue(regKey, "SteamExe", "").ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("taskkill", "/F /IM steam.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string baseurl = "http://localhost:8099";
            WebApp.Start<Startup>(baseurl);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetProcesses()
                         .Where(x => x.ProcessName.ToLower()
                                      .Contains("dead"))
                         .ToList().Count > 0)
            {
                lblGame.Text = "Dead by Daylight!";
            }
            else
            {
                lblGame.Text = "Not Dead by Daylight";
            }

        }
    }
}
