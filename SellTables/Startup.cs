using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SellTables.Startup))]
namespace SellTables
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
