namespace PassportOffice.DataSource.UnitOfWork.Repositories.PersonalInfo
{
    using System;
    using System.Collections.Generic;

    using PassportOffice.DataSource.Model;
    
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
