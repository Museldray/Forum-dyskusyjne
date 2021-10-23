using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Forum_dyskusyjne.Startup))]
namespace Forum_dyskusyjne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
