using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SignalR_app.Models;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalR_app.Models
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // manage context and server
            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            app.MapSignalR();
        }
    }
}