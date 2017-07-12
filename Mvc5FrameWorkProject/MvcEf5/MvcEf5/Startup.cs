using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcEf5.Startup))]
namespace MvcEf5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
