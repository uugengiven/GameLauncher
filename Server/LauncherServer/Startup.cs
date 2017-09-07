using LauncherServer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;

[assembly: OwinStartupAttribute(typeof(LauncherServer.Startup))]
namespace LauncherServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("admin"))
            {

                var role = new IdentityRole();
                role.Name = "admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "lange.john.m@gmail.com";
                user.Email = "lange.john.m@gmail.com";

                var chkUser = userManager.Create(user, "*Abcdef123*");

                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "admin");
                }
            }

            if (!roleManager.RoleExists("computer"))
            {
                var role = new IdentityRole();
                role.Name = "computer";
                roleManager.Create(role);
            }
        }
    }
}
