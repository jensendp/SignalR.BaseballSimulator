using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FantasyBaseball.Startup))]
namespace FantasyBaseball
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
