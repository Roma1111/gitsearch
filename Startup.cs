using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GitSearch.Startup))]
namespace GitSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
