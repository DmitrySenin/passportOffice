namespace PassportOffice.API.Controllers
{
    using System.Security.Claims;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using PassportOffice.API.DataAccess;
    using PassportOffice.API.Utility;

    /// <summary>
    /// Manages information about users.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        /// <summary>
        /// Help to communicate with data access layer to get information about users.
        /// </summary>
        UsersManager usersManager = new UsersManager();

        /// <summary>
        /// End-point to check current user for administrator role.
        /// </summary>
        /// <returns>Flag that identifies that current user is administrator.</returns>
        [Authorize]
        [HttpGet]
        [Route("isadmin")]
        public bool IsAdmin()
        {
            var identity = (ClaimsIdentity)this.User.Identity;
            string username = ClaimsUtils.FindUserName(identity.Claims);

            if (!string.IsNullOrEmpty(username))
            {
                return this.usersManager.IsAdmin(username);
            }
            else
            {
                return false;
            }
        }
    }
}
