namespace PassportOffice.DataSource.Context
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;

    using PassportOffice.DataSource.Model;

    /// <summary>
    /// Context of connection to database. 
    /// </summary>
    class PassportOfficeContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Create instance of <see cref="PassportOfficeContext"/> class.
        /// </summary>
        public PassportOfficeContext() : base("PassportOfficeContext")
        {
            // Set 4 minutes timeout of getting information from database.
            this.Database.CommandTimeout = 240;
        }

        /// <summary>
        /// All personal information in database.
        /// </summary>
        public virtual DbSet<PersonInfo> Persons { get; set; }

        /// <summary>
        /// Trigger of initializing of the context's model.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
