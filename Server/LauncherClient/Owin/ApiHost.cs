using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Owin
{
    public class ApiHost
    {
        public void StartHost()
        {
            string baseurl = "http://localhost:8099";
            WebApp.Start<Startup>(baseurl);
        }
    }
}
