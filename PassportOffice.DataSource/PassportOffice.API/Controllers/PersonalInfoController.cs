namespace PassportOffice.API.Controllers
{
    using System.Web.Http;

    using PassportOffice.DataSource.UnitOfWork;

    /// <summary>
    /// Controller which contains API to access personal information.
    /// </summary>
    [RoutePrefix("personalinfo")]
    public class PersonalInfoController : ApiController
    {
        /// <summary>
        /// Collection of repositories contain information from database.
        /// </summary>
        private UnitOfWork databaseRepos = new UnitOfWork();
    }
}
