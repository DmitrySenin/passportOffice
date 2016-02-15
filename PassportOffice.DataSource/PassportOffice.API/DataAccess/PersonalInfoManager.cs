namespace PassportOffice.API.DataAccess
{
    using System.Collections.Generic;

    using PassportOffice.DataSource.Model;
    using PassportOffice.DataSource.DataAccess;
    using PassportOffice.DataSource.Searching;

    /// <summary>
    /// Communicates with data source classes to work with passport information.
    /// </summary>
    public class PersonalInfoManager
    {
        /// <summary>
        /// Collection of repositories contains information from database.
        /// </summary>
        private RepositoriesUnit reposCollection;

        /// <summary>
        /// Create instance of <see cref="PersonalInfoManager"/> class.
        /// </summary>
        public PersonalInfoManager()
        {
            reposCollection = new RepositoriesUnit();
        }

        /// <summary>
        /// Finds all records of personal information.
        /// </summary>
        /// <param name="fullSort">Flag which identifies what fields should be used to sord data.</param>
        /// <returns>All passport data.</returns>
        public IEnumerable<PersonInfo> GetAll(bool fullSort = false)
        {
            return this.reposCollection.PersonalInfoRepository.GetAll(fullSort);
        }

        /// <summary>
        /// Finds all records of personal data using for selection searching criteria.
        /// </summary>
        /// <param name="searchingOptions">Restriction which used to select data.</param>
        /// <param name="fullSort">Flag which identifies what fields should be used to sord data.</param>
        /// <returns>All passport data satisfy to searching criteria.</returns>
        public IEnumerable<PersonInfo> SearchAll(PersonalInfoSearchingOptions searchingOptions, bool fullSort = false)
        {
            return this.reposCollection.PersonalInfoRepository.SearchAll(searchingOptions, fullSort);
        }

        /// <summary>
        /// Finds all selected records on requested page.
        /// </summary>
        /// <param name="pageSize">Count of records on one page.</param>
        /// <param name="pageNumber">Number of requested page.</param>
        /// <param name="searchingOptions">Criteria for selection of records.</param>
        /// <param name="fullSort">Flag which identifies what fields should be used to sord data.</param>
        /// <returns>Records located on requested page.</returns>
        public IEnumerable<PersonInfo> GetPagedList(int pageSize, int pageNumber, PersonalInfoSearchingOptions searchingOptions, bool fullSort = false)
        {
            return this.reposCollection.PersonalInfoRepository.GetPage(pageSize, pageNumber, searchingOptions, fullSort);
        }
    }
}