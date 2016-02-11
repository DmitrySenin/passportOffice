namespace PassportOffice.DataSource.DataAccess
{
    using System;

    using PassportOffice.DataSource.Context;
    using PassportOffice.DataSource.DataAccess.Repositories.PersonalInfo;
    using PassportOffice.DataSource.DataAccess.Repositories.Users;

    /// <summary>
    /// Container of repositories working with database.
    /// It guarantees that all repositories will work with one context of database.
    /// </summary>
    public class RepositoriesUnit : IDisposable
    {
        /// <summary>
        /// Flag which identifies that object was already disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Context of database.
        /// </summary>
        private PassportOfficeContext passportOfficeContext = new PassportOfficeContext();

        /// <summary>
        /// Repository with personal information.
        /// </summary>
        private IPersonalInfoRepositiry personalInfoRepo;

        /// <summary>
        /// Repository with information about users.
        /// </summary>
        private IUsersRepository usersRepository;

        /// <summary>
        /// Gets/sets repository with personal data.
        /// </summary>
        public IPersonalInfoRepositiry PersonalInfoRepository
        {
            get
            {
                if (this.personalInfoRepo == null)
                {
                    this.personalInfoRepo = new PersonalInfoRepository(this.passportOfficeContext);
                }

                return this.personalInfoRepo;
            }
        }

        /// <summary>
        /// Gets/sets respoistory with users information.
        /// </summary>
        public IUsersRepository UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new UsersRepository(this.passportOfficeContext);
                }

                return this.usersRepository;
            }
        }

        /// <summary>
        /// Dispose current object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose current object.
        /// </summary>
        /// <param name="disposing">Flag which identifies that process of disposing was invoked by user.</param>
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
    }
}
