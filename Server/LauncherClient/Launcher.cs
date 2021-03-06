﻿using LauncherClasses;
using LauncherClient.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LauncherClient
{
    public partial class Launcher : Form
    {
        private ApiHost host;

        public Launcher()
        {
            InitializeComponent();

            host = new ApiHost();
            host.StartHost();

        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void game_start_timer_Tick(object sender, EventArgs e)
        {
            if(LauncherInfo.gameIsNew)
            {
                if (LauncherInfo.game.status == "ok")
                {
                    notifyIcon.BalloonTipTitle = "Game Started";
                    notifyIcon.BalloonTipText = LauncherInfo.game.name;
                    notifyIcon.ShowBalloonTip(1000);
                }
                LauncherInfo.gameIsNew = false;
            }
        }

        public void SetConfigValue(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.AppSettings.SectionInformation.ProtectSection(null);
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string computerKey = txtComputerKey.Text;
            string baseUrl = txtUrl.Text;
            SetConfigValue("ComputerKey", computerKey);
            SetConfigValue("BaseURL", baseUrl);
            // go get that secret key
            GameCommand gc = new GameCommand();

            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "computer_key", computerKey },
                { "current_time", DateTime.Now.ToString()}
            };

            dynamic obj = gc.GetWebResponse($"{baseUrl}/computers/getSecret", data);
            if (obj.status == "ok")
            {
                SetConfigValue("Secret", obj.secret);
            }
        }
    }
}
