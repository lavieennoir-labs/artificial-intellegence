using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Questioning.Startup))]
namespace Questioning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
