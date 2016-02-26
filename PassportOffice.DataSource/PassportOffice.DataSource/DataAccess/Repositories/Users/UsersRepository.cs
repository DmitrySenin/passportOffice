namespace PassportOffice.DataSource.DataAccess.Repositories.Users
{
    using System;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PassportOffice.DataSource.Context;
    using PassportOffice.DataSource.DataAccess.Repositories.Roles;
    using PassportOffice.DataSource.Model;

    /// <summary>
    /// Communicates with database to get user information.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        /// <summary>
        /// Authentication conetext of database.
        /// </summary>
        private PassportOfficeContext passportOfficeContext;

        /// <summary>
        /// Provides main operations with users.
        /// </summary>
        private UserManager<IdentityUser> userManager;

        /// <summary>
        /// Flag which identifies that an instance was already disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Create instance of <see cref="UsersRepository"/> class.
        /// </summary>
        /// <param name="authContext">Context of database.</param>
        internal UsersRepository(PassportOfficeContext passportOfficeContext)
        {
            this.passportOfficeContext = passportOfficeContext;

            // Initialize manager to work with database.
            this.userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this.passportOfficeContext));
        }

        /// <summary>
        /// Check existing of user with passed credentials.
        /// </summary>
        /// <param name="user">User's credentials.</param>
        /// <returns>Flag which identifies that user exists.</returns>
        public bool DoesExist(User user)
        {
            // If user doesn't exist than Find return null.
            return this.FindUser(user) != null;
        }

        /// <summary>
        /// Find information about user in database.
        /// </summary>
        /// <param name="user">User's credentials.</param>
        /// <returns>Full information abot user with passed credentials or null if user with credentials does not exist.</returns>
        public IdentityUser FindUser(User user)
        {
            return this.userManager.Find(user.UserName, user.Password);
        }

        /// <summary>
        /// Verifies that user was attached to admin role.
        /// </summary>
        /// <param name="userName">Login name of user.</param>
        /// <returns>Flag which identifies that user is administrator.</returns>
        public bool IsAdmin(string userName)
        {
            IdentityUser user = this.userManager.FindByName(userName);

            // User doesn't exist.
            if (user == null)
            {
                return false;
            }

            return this.userManager.IsInRole(user.Id, RolesRepository.AdminRoleId);
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
    }
}
