namespace PassportOffice.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using PassportOffice.DataSource.Model;
    using PassportOffice.DataSource.UnitOfWork;

    /// <summary>
    /// Controller which contains API to access personal information.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("personalinfo")]
    public class PersonalInfoController : ApiController
    {
        /// <summary>
        /// Collection of repositories contain information from database.
        /// </summary>
        private UnitOfWork databaseRepos = new UnitOfWork();

        /// <summary>
        /// Finds all data at data storage.
        /// </summary>
        /// <returns>All records of personal information from data source.</returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<PersonInfo> GetAll()
        {
            return databaseRepos.PersonalInfoRepository.GetAll();
        }
    }
}
