using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(B2CVideoPortal.Startup))]

namespace B2CVideoPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
