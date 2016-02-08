namespace PassportOffice.DataSource.UnitOfWork.Repositories.PersonalInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PassportOffice.DataSource.Context;
    using PassportOffice.DataSource.Model;
    using Searching;

    /// <summary>
    /// Repository with personal data based on database context.
    /// </summary>
    public class PersonalInfoRepository : IPersonalInfoRepositiry
    {
        /// <summary>
        /// Current context of database.
        /// </summary>
        private PassportOfficeContext passportOfficeContext;

        /// <summary>
        /// Flag which identifies that object was already disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Create an instance of <see cref="PersonalInfoRepository"/> class.
        /// </summary>
        /// <param name="passportOfficeContext">Context of current database.</param>
        internal PersonalInfoRepository(PassportOfficeContext passportOfficeContext)
        {
            this.passportOfficeContext = passportOfficeContext;
        }

        /// <summary>
        /// Finds all data at database.
        /// </summary>
        /// <returns>All records from database.</returns>
        public IEnumerable<PersonInfo> GetAll()
        {
            List<PersonInfo> personalData = this.SortRecords(this.GetAllFromDB()).ToList();
            return personalData;
        }

        /// <summary>
        /// Searches all data considering criteria.
        /// </summary>
        /// <param name="searchOptions">Searching criteria.</param>
        /// <returns>All records from database which satisfy restrictions.</returns>
        public IEnumerable<PersonInfo> SearchAll(PersonalInfoSearchingOptions searchOptions)
        {
            IQueryable<PersonInfo> dbPersonalInfo = this.GetAllFromDB();
            dbPersonalInfo = this.SortRecords(this.SearchRecords(dbPersonalInfo, searchOptions));
            return dbPersonalInfo.ToList();
        }

        /// <summary>
        /// Finds record with concete unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of record at database.</param>
        /// <returns>Record with Id equals to passed parameter.</returns>
        public PersonInfo GetById(int id)
        {
            return this.passportOfficeContext.Persons.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// Save changes of repository.
        /// </summary>
        public void Save()
        {
            this.passportOfficeContext.SaveChanges();
        }

        /// <summary>
        /// Dispose current instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose current instance.
        /// </summary>
        /// <param name="disposing">Flag which identifies that disposing was invoked by user.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.passportOfficeContext.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Load data from database.
        /// </summary>
        /// <returns>All personal information from database.</returns>
        private IQueryable<PersonInfo> GetAllFromDB()
        {
            return this.passportOfficeContext.Persons.AsQueryable<PersonInfo>();
        }

        /// <summary>
        /// Sort personal information.
        /// It sorts data firstly by last name, then by first name,
        /// middle name, date of birth, series of passport, number of passport.
        /// </summary>
        /// <param name="personalInfo">Data that should be sorted.</param>
        /// <returns>Sorted collection of personal data.</returns>
        private IQueryable<PersonInfo> SortRecords(IQueryable<PersonInfo> personalInfo)
        {
            return personalInfo.OrderBy(p => p.LastName)
                                .ThenBy(p => p.FirstName)
                                .ThenBy(p => p.MiddleName)
                                .ThenBy(p => p.BirthdayDate)
                                .ThenBy(p => p.PassportSeries)
                                .ThenBy(p => p.PassportNumber);
        }

        /// <summary>
        /// Searches personal data using criteria.
        /// </summary>
        /// <param name="personalInfo">Collection of personal data.</param>
        /// <param name="searchingOptions">Searching criteria.</param>
        /// <returns>Selected records of personal data.</returns>
        private IQueryable<PersonInfo> SearchRecords(IQueryable<PersonInfo> personalInfo, PersonalInfoSearchingOptions searchingOptions)
        {
            // Add searching by first name if need
            if (searchingOptions.UseFirstName())
            {
                personalInfo = personalInfo.Where(p => p.FirstName.StartsWith(searchingOptions.FirstName));
            }

            // Add searching by last name if need
            if (searchingOptions.UseLastName())
            {
                personalInfo = personalInfo.Where(p => p.LastName.StartsWith(searchingOptions.LastName));
            }

            // Add searching by middle name if need
            if (searchingOptions.UseMiddleName())
            {
                personalInfo = personalInfo.Where(p => p.MiddleName.StartsWith(searchingOptions.MiddleName));
            }

            // Add searching by series of passport if need
            if (searchingOptions.UsePassportSeries())
            {
                personalInfo = personalInfo.Where(p => p.PassportSeries.StartsWith(searchingOptions.PassportSeries));
            }

            // Add searching by number of passport if need
            if (searchingOptions.UsePassportNumber())
            {
                personalInfo = personalInfo.Where(p => p.PassportNumber.StartsWith(searchingOptions.PassportNumber));
            }

            // Add searching by date of birthday if need
            if (searchingOptions.UseBirthdayDate())
            {
                personalInfo = personalInfo.Where(p => p.BirthdayDate.Year == searchingOptions.BirthdayDate.Value.Year
                                                        && p.BirthdayDate.Month == searchingOptions.BirthdayDate.Value.Month
                                                        && p.BirthdayDate.Day == searchingOptions.BirthdayDate.Value.Day);
            }

            return personalInfo;
        }
    }
}
