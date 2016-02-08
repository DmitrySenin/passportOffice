namespace PassportOffice.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using PassportOffice.API.DataAccess;

    using PassportOffice.DataSource.Model;
    using PassportOffice.DataSource.Searching;

    /// <summary>
    /// Controller which contains API to access personal information.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("personalinfo")]
    public class PersonalInfoController : ApiController
    {
        /// <summary>
        /// Manager which used to access data source.
        /// </summary>
        PersonalInfoManager personalInfoManager = new PersonalInfoManager();

        /// <summary>
        /// Finds all data at data storage.
        /// </summary>
        /// <returns>All records of personal information from data source.</returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<PersonInfo> GetAll([FromUri] PersonalInfoSearchingOptions searchOptions)
        {
            return personalInfoManager.SearchAll(searchOptions);
        }
    }
}
