namespace PersonalInfo.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Moq;
    using NUnit.Framework;

    using PassportOffice.DataSource.Context;
    using PassportOffice.DataSource.Model;
    using PassportOffice.DataSource.Searching;
    using PassportOffice.DataSource.DataAccess.Repositories.PersonalInfo;

    /// <summary>
    /// Testing repository of personal information.
    /// </summary>
    [TestFixture]
    public class PersonalInfoRepo
    {
        /// <summary>
        /// Check that repository returns all data.
        /// </summary>
        [TestCase]
        public void Should_CollectAllData()
        {
            // Arrange
            var personalData = new List<PersonInfo>()
            {
                new PersonInfo { ID = 1, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(3, 1, 1), PassportSeries = "3333", PassportNumber = "333333", PassportIssueDate = new DateTime(21, 1, 1), Address = "C street" },
                new PersonInfo { ID = 4, LastName = "A", FirstName = "A", MiddleName = "A", BirthdayDate = new DateTime(1, 1, 1), PassportSeries = "1111", PassportNumber = "111111", PassportIssueDate = new DateTime(19, 1, 1), Address = "A street" },
                new PersonInfo { ID = 2, LastName = "B", FirstName = "B", MiddleName = "B", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222222", PassportIssueDate = new DateTime(20, 1, 1), Address = "B street" },
                new PersonInfo { ID = 3, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222221", PassportIssueDate = new DateTime(20, 1, 1), Address = "C street" },
            };
            var repo = this.createPersonaInfoRepo(personalData);

            // Act
            // Get all data from repo.
            var persons = repo.GetAll();

            // Assert
            // Check that all data was got from data source.
            CollectionAssert.AreEquivalent(personalData, persons);
        }

        /// <summary>
        /// Check that returned all data from repository and it is ordered.
        /// </summary>
        [TestCase]
        public void Should_OrderData()
        {
            // Arrange
            var personalData = new List<PersonInfo>()
            {
                new PersonInfo { ID = 1, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(3, 1, 1), PassportSeries = "3333", PassportNumber = "333333", PassportIssueDate = new DateTime(21, 1, 1), Address = "C street" },
                new PersonInfo { ID = 4, LastName = "A", FirstName = "A", MiddleName = "A", BirthdayDate = new DateTime(1, 1, 1), PassportSeries = "1111", PassportNumber = "111111", PassportIssueDate = new DateTime(19, 1, 1), Address = "A street" },
                new PersonInfo { ID = 2, LastName = "B", FirstName = "B", MiddleName = "B", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222222", PassportIssueDate = new DateTime(20, 1, 1), Address = "B street" },
                new PersonInfo { ID = 3, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222221", PassportIssueDate = new DateTime(20, 1, 1), Address = "C street" },
            };
            // Collection that should be returned.
            var expectedCollection = new List<PersonInfo>()
            {
                personalData[1],
                personalData[2],
                personalData[3],
                personalData[0],
            };
            var repo = this.createPersonaInfoRepo(personalData);

            // Act
            var persons = repo.GetAll(true);

            // Assert
            // Check that all data was got from data source in expected order.
            CollectionAssert.AreEqual(expectedCollection, persons);
        }

        /// <summary>
        /// Check that collection will be empty if searching options contain non existing characteristic.
        /// </summary>
        [TestCase]
        public void Should_SearchedCollectionIsEmpty()
        {
            // Arrange
            var personalData = new List<PersonInfo>()
            {
                new PersonInfo { ID = 1, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(3, 1, 1), PassportSeries = "3333", PassportNumber = "333333", PassportIssueDate = new DateTime(21, 1, 1), Address = "C street" },
                new PersonInfo { ID = 4, LastName = "A", FirstName = "A", MiddleName = "A", BirthdayDate = new DateTime(1, 1, 1), PassportSeries = "1111", PassportNumber = "111111", PassportIssueDate = new DateTime(19, 1, 1), Address = "A street" },
                new PersonInfo { ID = 2, LastName = "B", FirstName = "B", MiddleName = "B", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222222", PassportIssueDate = new DateTime(20, 1, 1), Address = "B street" },
                new PersonInfo { ID = 3, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222221", PassportIssueDate = new DateTime(20, 1, 1), Address = "C street" },
            };
            var repo = this.createPersonaInfoRepo(personalData);
            
            // There are no records at repository which satisfy created searching options.
            var searchingOptions = new PersonalInfoSearchingOptions();
            searchingOptions.FirstName = "Mike";
            searchingOptions.LastName = "Shinoda";

            var persons = repo.SearchAll(searchingOptions);

            // Check that collection is empty.
            Assert.AreEqual(persons.Count(), 0);
        }

        /// <summary>
        /// Check that each elements of selected collection satisfies searching options and ordered by discussed fields.
        /// </summary>
        [TestCase]
        public void Should_SatisfySearchingCriteria_And_Ordered()
        {
            // Arrange
            var personalData = new List<PersonInfo>()
            {
                new PersonInfo { ID = 1, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(3, 1, 1), PassportSeries = "3333", PassportNumber = "333333", PassportIssueDate = new DateTime(21, 1, 1), Address = "C street" },
                new PersonInfo { ID = 4, LastName = "A", FirstName = "A", MiddleName = "A", BirthdayDate = new DateTime(1, 1, 1), PassportSeries = "1111", PassportNumber = "111111", PassportIssueDate = new DateTime(19, 1, 1), Address = "A street" },
                new PersonInfo { ID = 2, LastName = "B", FirstName = "B", MiddleName = "B", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222222", PassportIssueDate = new DateTime(20, 1, 1), Address = "B street" },
                new PersonInfo { ID = 3, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222221", PassportIssueDate = new DateTime(20, 1, 1), Address = "C street" },
            };

            var repo = this.createPersonaInfoRepo(personalData);

            var searchingOptions = new PersonalInfoSearchingOptions();
            searchingOptions.FirstName = "C";
            searchingOptions.LastName = "C";

            var excepectedCollection = new List<PersonInfo>()
            {
                personalData[3],
                personalData[0]
            };

            // Act
            var persons = repo.SearchAll(searchingOptions, true);

            // Assert
            // Check that collection consists of same element in same order.
            CollectionAssert.AreEqual(excepectedCollection, persons);
        }

        /// <summary>
        /// Check that each element of collection satisfies searching options, ordered and amount of elements is not more than requested.
        /// </summary>
        [TestCase]
        public void Should_SatisfySearchingCriteriaAndOrderedAndPaged()
        {
            // Arrange
            int pageSize = 1;
            int pageNumber = 2;

            var personalData = new List<PersonInfo>()
            {
                new PersonInfo { ID = 1, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(3, 1, 1), PassportSeries = "3333", PassportNumber = "333333", PassportIssueDate = new DateTime(21, 1, 1), Address = "C street" },
                new PersonInfo { ID = 4, LastName = "A", FirstName = "A", MiddleName = "A", BirthdayDate = new DateTime(1, 1, 1), PassportSeries = "1111", PassportNumber = "111111", PassportIssueDate = new DateTime(19, 1, 1), Address = "A street" },
                new PersonInfo { ID = 2, LastName = "B", FirstName = "B", MiddleName = "B", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222222", PassportIssueDate = new DateTime(20, 1, 1), Address = "B street" },
                new PersonInfo { ID = 3, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222221", PassportIssueDate = new DateTime(20, 1, 1), Address = "C street" },
            };

            var repo = this.createPersonaInfoRepo(personalData);

            var searchingOptions = new PersonalInfoSearchingOptions();
            searchingOptions.FirstName = "C";
            searchingOptions.LastName = "C";

            var excepectedCollection = new List<PersonInfo>()
            {
                personalData[0]
            };
            
            // Act
            var persons = repo.GetPage(pageSize, pageNumber, searchingOptions, true);

            // Assertions
            // Amount of records is correct.
            Assert.LessOrEqual(persons.Count(), pageSize);
            
            // Check that collection consist of same element in same order.
            CollectionAssert.AreEqual(excepectedCollection, persons);
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
        /// Creates repository based on collection with personal data.
        /// </summary>
        /// <param name="personInfo">Collection of personal information.</param>
        /// <returns>Repository which manipulates personal data and contains passed data.</returns>
        private PersonalInfoRepository createPersonaInfoRepo(List<PersonInfo> personInfo)
        {
            var personInfoSet = this.createPersonInfoDBContext(personInfo);
            var passportOfficeContext = new Mock<PassportOfficeContext>();
            passportOfficeContext.Setup(c => c.Persons).Returns(personInfoSet);
            var personalInfoRepo = new PersonalInfoRepository(passportOfficeContext.Object);
            return personalInfoRepo;
        }
    }
}
