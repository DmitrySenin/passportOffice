namespace PassportOffice.API.Controllers
{
    using System.Security.Claims;
    using System.Web.Http;

    using PassportOffice.API.DataAccess;
    using PassportOffice.API.Utility;

    /// <summary>
    /// Manages information about users.
    /// </summary>
    [RoutePrefix("users")]
    public class UsersController : ApiController
    { }
}
