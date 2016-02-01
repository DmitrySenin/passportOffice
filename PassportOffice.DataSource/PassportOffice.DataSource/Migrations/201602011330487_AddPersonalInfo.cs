namespace PassportOffice.DataSource.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonalInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        BirthdayDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        PassportSeries = c.String(),
                        PassportNumber = c.String(),
                        PassportIssueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonInfo");
        }
    }
}
