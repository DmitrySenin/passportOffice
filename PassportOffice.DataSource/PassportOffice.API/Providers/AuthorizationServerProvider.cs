namespace PassportOffice.API.Providers
{
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.Owin.Security.OAuth;

    using PassportOffice.API.DataAccess;
    using PassportOffice.API.Utility;

    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Validate client application.
        /// </summary>
        /// <param name="context">Client's context.</param>
        /// <returns>Task to enable asynchronous execution.</returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Check user credential for existing.
        /// </summary>
        /// <param name="context">Context of request </param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            UsersManager userManager = new UsersManager();

            if (userManager.DoesExist(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(ClaimsUtils.CreateUserNameClaim(context.UserName));

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
        }
    }
}