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
        /// <param name="pageSize">Amount of records on one page.</param>
        /// <param name="pageNumber">Number of page with personal data.</param>
        /// <param name="fullSort">Flag to sorting data by all fields.</param>
        /// <param name="searchOptions">Criteria to select data.</param>
        /// <returns>All records of personal information from data source.</returns>
        [Route("{pageSize:int}/{pageNumber:int}/{fullSort:bool}")]
        [HttpGet]
        public IEnumerable<PersonInfo> GetAll(int pageSize, int pageNumber, bool fullSort, [FromUri] PersonalInfoSearchingOptions searchOptions)
        {
            return personalInfoManager.GetPagedList(pageSize, pageNumber, searchOptions, fullSort);
        }

        /// <summary>
        /// Remove all personal data.
        /// </summary>
        /// <returns>Result of execution of removing.</returns>
        [Authorize]
        [Route("")]
        [HttpDelete]
        public IHttpActionResult RemoveAll()
        {
            this.personalInfoManager.RemoveAll();
            return this.Ok();
        }
    }
}
