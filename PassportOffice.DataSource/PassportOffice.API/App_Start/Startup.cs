using Microsoft.Owin;

[assembly: OwinStartup(typeof(PassportOffice.API.App_Start.Startup))]
namespace PassportOffice.API.App_Start
{
    using System.Web.Http;

    using Owin;

    /// <summary>
    /// Fired when application starts.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Setups Owin configuration.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}