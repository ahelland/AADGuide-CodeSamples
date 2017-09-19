using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ADFSTodoSPA.Startup))]

namespace ADFSTodoSPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
