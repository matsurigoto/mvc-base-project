using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseProject.Admin.Startup))]
namespace BaseProject.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
