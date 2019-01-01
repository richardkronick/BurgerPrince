using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BurgerPrince.Startup))]
namespace BurgerPrince
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
