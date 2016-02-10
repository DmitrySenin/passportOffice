namespace PersonalInfo.Test
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Ploeh.AutoFixture;
    using Moq;
    using NUnit.Framework;

    using PassportOffice.DataSource.Context;
    using PassportOffice.DataSource.Model;
    using PassportOffice.DataSource.UnitOfWork.Repositories.PersonalInfo;

    /// <summary>
    /// Testing repository of personal information.
    /// </summary>
    [TestFixture]
    public class PersonalInfoRepo
    {
        /// <summary>
        /// Amount of fake personal information records.
        /// </summary>
        private readonly int personalInfoCount = 100;

        /// <summary>
        /// Represents test data.
        /// </summary>
        private List<PersonInfo> personInfo;

        /// <summary>
        /// Repersents set of database contains personal data;
        /// </summary>
        private DbSet<PersonInfo> personalInfoSet;

        /// <summary>
        /// Represents context which is used in repository.
        /// </summary>
        PassportOfficeContext passportOfficeContext;

        /// <summary>
        /// Initializing of necessary data.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.personInfo = this.createPersonInfoContainer();
            this.personalInfoSet = this.createPersonInfoDBContext(this.personInfo);
            this.passportOfficeContext = this.createPassportOfficeContext(this.personalInfoSet);
        }

        /// <summary>
        /// Check that repository returns all data.
        /// </summary>
        [TestCase]
        public void Should_CollectAllData()
        {
            var repo = new PersonalInfoRepository(passportOfficeContext);
            var persons = repo.GetAll();

            // Check that all data was got from data source.
            CollectionAssert.AreEquivalent(personInfo, persons);
        }

        /// <summary>
        /// Check that returned data is ordered.
        /// </summary>
        [TestCase]
        public void Should_OrderData()
        {
            var repo = new PersonalInfoRepository(passportOfficeContext);
            var persons = repo.GetAll();

            var orderedPersonInfo = this.sortPersonalInfo(this.personInfo);

            // Check that all data was got from data source.
            CollectionAssert.AreEqual(orderedPersonInfo, persons);
        }

        /// <summary>
        /// Generate test data and set it in container.
        /// </summary>
        private List<PersonInfo> createPersonInfoContainer()
        {
            Fixture fixture = new Fixture();
            List<PersonInfo> personInfo = new List<PersonInfo>();
            for (int i = 0; i < this.personalInfoCount; i++)
            {
                personInfo.Add(fixture.Build<PersonInfo>().Create<PersonInfo>());
            }
            return personInfo;
        }

        /// <summary>
        /// Initializes database context using passed data.
        /// </summary>
        /// <param name="personInfo">Collection of personal data.</param>
        /// <returns>Created database context.</returns>
        private DbSet<PersonInfo> createPersonInfoDBContext(List<PersonInfo> personInfo)
        {
            var queryablePersonInfo = personInfo.AsQueryable();

            var personInfoSet = new Mock<DbSet<PersonInfo>>();
            personInfoSet.As<IQueryable<PersonInfo>>().Setup(m => m.Provider).Returns(queryablePersonInfo.Provider);
            personInfoSet.As<IQueryable<PersonInfo>>().Setup(m => m.Expression).Returns(queryablePersonInfo.Expression);
            personInfoSet.As<IQueryable<PersonInfo>>().Setup(m => m.ElementType).Returns(queryablePersonInfo.ElementType);
            personInfoSet.As<IQueryable<PersonInfo>>().Setup(m => m.GetEnumerator()).Returns(() => queryablePersonInfo.GetEnumerator());

            return personInfoSet.Object;
        }

        /// <summary>
        /// Creates context of passport office application using passed database set.
        /// </summary>
        /// <param name="personInfoSet">Database set which contains personal data.</param>
        /// <returns>Context to communicate with source of personal data.</returns>
        private PassportOfficeContext createPassportOfficeContext(DbSet<PersonInfo> personInfoSet)
        {
            var passportOfficeContext = new Mock<PassportOfficeContext>();
            passportOfficeContext.Setup(c => c.Persons).Returns(personInfoSet);
            return passportOfficeContext.Object;
        }

        /// <summary>
        /// Sort collection of personal information.
        /// </summary>
        /// <param name="personInfo">List of personal information records.</param>
        /// <returns>Sorted collection with personal data.</returns>
        private List<PersonInfo> sortPersonalInfo(List<PersonInfo> personInfo)
        {
            return personInfo
                        .OrderBy(p => p.LastName)
                        .ThenBy(p => p.FirstName)
                        .ThenBy(p => p.MiddleName)
                        .ThenBy(p => p.BirthdayDate)
                        .ThenBy(p => p.PassportSeries)
                        .ThenBy(p => p.PassportNumber).ToList();
        }
    }
}
