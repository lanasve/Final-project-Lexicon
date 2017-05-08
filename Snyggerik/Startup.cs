using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Snyggerik.Startup))]
namespace Snyggerik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
