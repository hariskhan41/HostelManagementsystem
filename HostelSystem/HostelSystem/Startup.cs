using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HostelSystem.Startup))]
namespace HostelSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
