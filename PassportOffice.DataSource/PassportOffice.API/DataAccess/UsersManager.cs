namespace PassportOffice.API.DataAccess
{
    using PassportOffice.DataSource.DataAccess;
    using PassportOffice.DataSource.Model;

    /// <summary>
    /// Communicates with database to get information about users.
    /// </summary>
    public class UsersManager
    {
        /// <summary>
        /// Collection of repositories contains information from database.
        /// </summary>
        private RepositoriesUnit reposCollection;

        /// <summary>
        /// Create instance of <see cref="UsersManager"/> class.
        /// </summary>
        public UsersManager()
        {
            this.reposCollection = new RepositoriesUnit();
        }

        /// <summary>
        /// Check exsitnig of combination of credentials..
        /// </summary>
        /// <param name="username">User's login name.</param>
        /// <param name="password">User's password.</param>
        /// <returns>Flag which identifies that passed combination of username and password exests.</returns>
        public bool DoesExist(string username, string password)
        {
            return this.reposCollection.UsersRepository.DoesExist(new User { UserName = username, Password = password});
        }

        /// <summary>
        /// Recognizes administrator by login.
        /// </summary>
        /// <param name="userName">Registered name of user.</param>
        /// <returns>Flag which identifies that user is administrator.</returns>
        public bool IsAdmin(string userName)
        {
            return this.reposCollection.UsersRepository.IsAdmin(userName);
        }
    }
}