using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminstrationSysytem_v1.Startup))]
namespace AdminstrationSysytem_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
