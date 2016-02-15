namespace PassportOffice.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonalInfoRestrictions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonInfo", "FirstName", c => c.String(maxLength: 200));
            AlterColumn("dbo.PersonInfo", "LastName", c => c.String(maxLength: 200));
            AlterColumn("dbo.PersonInfo", "MiddleName", c => c.String(maxLength: 200));
            AlterColumn("dbo.PersonInfo", "Address", c => c.String(maxLength: 400));
            AlterColumn("dbo.PersonInfo", "PassportSeries", c => c.String(maxLength: 15));
            AlterColumn("dbo.PersonInfo", "PassportNumber", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonInfo", "PassportNumber", c => c.String());
            AlterColumn("dbo.PersonInfo", "PassportSeries", c => c.String());
            AlterColumn("dbo.PersonInfo", "Address", c => c.String());
            AlterColumn("dbo.PersonInfo", "MiddleName", c => c.String());
            AlterColumn("dbo.PersonInfo", "LastName", c => c.String());
            AlterColumn("dbo.PersonInfo", "FirstName", c => c.String());
        }
    }
}
