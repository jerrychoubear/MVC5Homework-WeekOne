using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5Homework_WeekOne.Startup))]
namespace MVC5Homework_WeekOne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
