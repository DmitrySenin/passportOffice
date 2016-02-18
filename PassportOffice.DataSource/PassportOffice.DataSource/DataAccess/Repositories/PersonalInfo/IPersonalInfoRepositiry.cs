namespace PassportOffice.DataSource.DataAccess.Repositories.PersonalInfo
{
    using System;
    using System.Collections.Generic;

    using PassportOffice.DataSource.Model;
    using PassportOffice.DataSource.Searching;

    /// <summary>
    /// Frame for implementing repository of personal data.
    /// </summary>
    public interface IPersonalInfoRepositiry : IDisposable
    {
        /// <summary>
        /// Finds all data in source of data.
        /// </summary>
        /// <returns>All records of personal information.</returns>
        IEnumerable<PersonInfo> GetAll();

        /// <summary>
        /// Finds all data in source of data using searching criteria for selection.
        /// </summary>
        /// <param name="searchOptions">Searching criteria.</param>
        /// <returns>All records which satisfy searching options.</returns>
        IEnumerable<PersonInfo> SearchAll(PersonalInfoSearchingOptions searchOptions);

        /// <summary>
        /// Selecting portion of personal date based on passed criteria.
        /// </summary>
        /// <param name="pageSize">Size of returned portion.</param>
        /// <param name="pageNumber">Number of returned portion.</param>
        /// <param name="searchOptions">Criteria of selecting records.</param>
        /// <returns>Records of personal information which satisfy criteria.</returns>
        IEnumerable<PersonInfo> GetPage(int pageSize, int pageNumber, PersonalInfoSearchingOptions searchOptions);

        /// <summary>
        /// Finds record with concrete unique identifier.
        /// </summary>
        /// <param name="ID">Unique identifier of record of personal data.</param>
        /// <returns>Personal information with Id equals to passed parameter.</returns>
        PersonInfo GetById(int ID);

        /// <summary>
        /// Saves changes of repository.
        /// </summary>
        void Save();
    }
}
