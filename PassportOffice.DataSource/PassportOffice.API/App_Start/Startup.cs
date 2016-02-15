using Microsoft.Owin;

[assembly: OwinStartup(typeof(PassportOffice.API.App_Start.Startup))]
namespace PassportOffice.API.App_Start
{
    using System;
    using System.Web.Http;

    using Microsoft.Owin.Security.OAuth;
    using Owin;

    using PassportOffice.API.Providers;

    /// <summary>
    /// Fired when application starts.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Setups Owin configuration.
        /// </summary>
        /// <param name="app">Application configuration for Owin server.</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            this.ConfigureOAuth(app);

            app.UseWebApi(config);
        }

        /// <summary>
        /// Configure OAuth authorizations server.
        /// </summary>
        /// <param name="app">Collection of application's parameters.</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            // Setup server's options.
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
            };

            // Generate token.
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}