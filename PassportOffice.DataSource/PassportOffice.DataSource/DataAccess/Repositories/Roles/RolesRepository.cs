namespace PassportOffice.DataSource.DataAccess.Repositories.Roles
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PassportOffice.DataSource.Context;
    using PassportOffice.DataSource.Model;

    /// <summary>
    /// Contains method to communicate with data base to manage roles.
    /// </summary>
    public class RolesRepository : IRolesRepository
    {
        /// <summary>
        /// Context of application's database.
        /// </summary>
        private PassportOfficeContext passportOfficeContext;

        /// <summary>
        /// Provides main operation with roles.
        /// </summary>
        private RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Represent unique identifier of administrator role.
        /// </summary>
        public static readonly string AdminRoleId = "admin";

        /// <summary>
        /// Gets/sets information about administrator role.
        /// </summary>
        public Role AdminRole
        {
            get
            {
                IdentityRole adminRole = this.roleManager.FindById(RolesRepository.AdminRoleId);
                return new Role { ID = adminRole.Id, Name = adminRole.Name };
            }
        }

        /// <summary>
        /// Create an instance of <see cref="RolesRepository"/> class.
        /// </summary>
        /// <param name="passportOfficeContext">Context of database.</param>
        internal RolesRepository(PassportOfficeContext passportOfficeContext)
        {
            this.passportOfficeContext = passportOfficeContext;
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(passportOfficeContext));
        }
    }
}
