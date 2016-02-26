namespace PassportOffice.DataSource.DataAccess.Repositories
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using PassportOffice.DataSource.Model;

    /// <summary>
    /// Foundation to implement roles management system.
    /// </summary>
    public interface IRolesRepository
    {
        /// <summary>
        /// Gets admin role's description.
        /// </summary>
        Role AdminRole { get; }
    }
}
