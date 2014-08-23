using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalRDbUpdates.Startup))]
namespace SignalRDbUpdates
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
