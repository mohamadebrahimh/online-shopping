using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Businessdevweb.Startup))]
namespace Businessdevweb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
