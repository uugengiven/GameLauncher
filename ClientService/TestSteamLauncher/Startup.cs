using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web;

[assembly: OwinStartup(typeof(TestSteamLauncher.Startup))]

namespace TestSteamLauncher
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional }
                );
            app.UseWebApi(config);
        }
    }
}
