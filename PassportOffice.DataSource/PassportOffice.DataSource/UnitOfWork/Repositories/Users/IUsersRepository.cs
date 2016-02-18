namespace PassportOffice.DataSource.UnitOfWork.Repositories.Users
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    using PassportOffice.DataSource.Model;

    /// <summary>
    /// Frame to create repository managing information about users.
    /// </summary>
    public interface IUsersRepository : IDisposable
    {
        /// <summary>
        /// Find uset in data source.
        /// </summary>
        /// <param name="user">User's credentials.</param>
        /// <returns>Full information about user.</returns>
        IdentityUser FindUser(User user);

        /// <summary>
        /// Check that user was added to data source.
        /// </summary>
        /// <param name="user">User's credentials.</param>
        /// <returns>Flag which identifies that user exists.</returns>
        bool DoesExist(User user);
    }
}
