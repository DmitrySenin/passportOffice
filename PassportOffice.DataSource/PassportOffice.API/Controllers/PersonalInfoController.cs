namespace PassportOffice.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using PassportOffice.DataSource.Model;
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
