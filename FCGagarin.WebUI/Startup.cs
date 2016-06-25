using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FCGagarin.WebUI.Startup))]
namespace FCGagarin.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
