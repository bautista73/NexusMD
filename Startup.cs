using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NexusMD.MVC.Startup))]
namespace NexusMD.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
