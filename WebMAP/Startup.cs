using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMAP.Startup))]
namespace WebMAP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();

        }
    }
}
