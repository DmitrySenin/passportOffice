namespace PersonalInfo.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Ploeh.AutoFixture;
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
        /// Check that each elemnent of collection satisfies searching options.
        /// </summary>
        [TestCase]
        public void Should_SatisfySearchingCriteria_And_Ordered()
        {
            var repo = new PersonalInfoRepository(passportOfficeContext);
            
            var searchingOptions = this.createSearchOptions(this.personInfo);
            var persons = repo.SearchAll(searchingOptions, true);

            var searchAndOrderPersonalData = this.sortPersonalInfo(this.personInfo);
            searchAndOrderPersonalData = this.searchPersonalData(searchAndOrderPersonalData, searchingOptions).ToList();

            // Check that collection consist of same element in same order.
            CollectionAssert.AreEqual(searchAndOrderPersonalData, persons);
        }

        /// <summary>
        /// Check that each elemnent of collection satisfies searching options.
        /// </summary>
        [TestCase]
        public void Should_SatisfySearchingCriteriaAndOrderedAndPaged()
        {
            int pageSize = 10;
            int pageNumber = 1;

            var repo = new PersonalInfoRepository(passportOfficeContext);

            var searchingOptions = this.createSearchOptions(this.personInfo);
            var persons = repo.GetPage(pageSize, pageNumber, searchingOptions, true);

            var testPersonalData = this.sortPersonalInfo(this.personInfo);
            testPersonalData = this.searchPersonalData(testPersonalData, searchingOptions).ToList();
            testPersonalData = this.getPageOfPersonalData(testPersonalData, pageSize, pageNumber).ToList();

            // Amount of records is correct.
            Assert.LessOrEqual(persons.Count(), pageSize);
            
            // Check that collection consist of same element in same order.
            CollectionAssert.AreEqual(testPersonalData, persons);
        }

        /// <summary>
        /// Generate test data and set it in container.
        /// </summary>
        private List<PersonInfo> createPersonInfoContainer()
        {
            Fixture fixture = new Fixture();
            List<PersonInfo> personInfo = new List<PersonInfo>()
            {
                new PersonInfo { ID = 1, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(3, 1, 1), PassportSeries = "3333", PassportNumber = "333333", PassportIssueDate = new DateTime(21, 1, 1), Address = "C street" },
                new PersonInfo { ID = 4, LastName = "A", FirstName = "A", MiddleName = "A", BirthdayDate = new DateTime(1, 1, 1), PassportSeries = "1111", PassportNumber = "111111", PassportIssueDate = new DateTime(19, 1, 1), Address = "A street" },
                new PersonInfo { ID = 2, LastName = "B", FirstName = "B", MiddleName = "B", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222222", PassportIssueDate = new DateTime(20, 1, 1), Address = "B street" },
                new PersonInfo { ID = 3, LastName = "C", FirstName = "C", MiddleName = "C", BirthdayDate = new DateTime(2, 1, 1), PassportSeries = "2222", PassportNumber = "222221", PassportIssueDate = new DateTime(20, 1, 1), Address = "C street" },
            };

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

        /// <summary>
        /// Create random searching options based on passed collection of personal data.
        /// </summary>
        /// <param name="personInfo">Personal data which is used to generate options.</param>
        /// <returns>Searching options which can be used to find at least one record from passed collection.</returns>
        private PersonalInfoSearchingOptions createSearchOptions(IEnumerable<PersonInfo> personInfo)
        {
            Random rnd = new Random();
            int index = rnd.Next(personInfo.Count());

            string rndLastName = personInfo.ElementAt(index).LastName;

            PersonalInfoSearchingOptions searchingOption = new PersonalInfoSearchingOptions();

            // get random substring of random last name.
            searchingOption.LastName = rndLastName.Substring(0, rnd.Next(rndLastName.Length));

            return searchingOption;
        }

        /// <summary>
        /// Execute seacrhing over collection of personal data using passed parameters.
        /// </summary>
        /// <param name="personalInfo">Collection to search over.</param>
        /// <param name="searchingOptions">Searching criteria.</param>
        /// <returns>Collection consists of elements which satisfy searching criteria.</returns>
        private IEnumerable<PersonInfo> searchPersonalData(IEnumerable<PersonInfo> personalInfo, PersonalInfoSearchingOptions searchingOptions)
        {
            // Create temorary storage.
            IEnumerable<PersonInfo> searchedPersonalData = personalInfo;

            if (PersonalInfoSearchingOptions.CheckSearchingOptions(searchingOptions))
            {
                // Add searching by first name if need
                if (searchingOptions.UseFirstName())
                {
                    searchedPersonalData = personalInfo.Where(p => p.FirstName.StartsWith(searchingOptions.FirstName));
                }

                // Add searching by last name if need
                if (searchingOptions.UseLastName())
                {
                    searchedPersonalData = personalInfo.Where(p => p.LastName.StartsWith(searchingOptions.LastName));
                }

                // Add searching by middle name if need
                if (searchingOptions.UseMiddleName())
                {
                    searchedPersonalData = personalInfo.Where(p => p.MiddleName.StartsWith(searchingOptions.MiddleName));
                }

                // Add searching by series of passport if need
                if (searchingOptions.UsePassportSeries())
                {
                    searchedPersonalData = personalInfo.Where(p => p.PassportSeries.StartsWith(searchingOptions.PassportSeries));
                }

                // Add searching by number of passport if need
                if (searchingOptions.UsePassportNumber())
                {
                    searchedPersonalData = personalInfo.Where(p => p.PassportNumber.StartsWith(searchingOptions.PassportNumber));
                }

                // Add searching by date of birthday if need
                if (searchingOptions.UseBirthdayDate())
                {
                    searchedPersonalData = personalInfo.Where(p => p.BirthdayDate.Year == searchingOptions.BirthdayDate.Value.Year
                                                            && p.BirthdayDate.Month == searchingOptions.BirthdayDate.Value.Month
                                                            && p.BirthdayDate.Day == searchingOptions.BirthdayDate.Value.Day);
                }
            }

            return searchedPersonalData.ToList();
        }

        /// <summary>
        /// Paged collection of personal data.
        /// </summary>
        /// <param name="personInfo">Collection of persoanal data.</param>
        /// <param name="pageSize">Amount records on one page.</param>
        /// <param name="pageNumber">Number of requested page.</param>
        /// <returns>Collection of personal information which placed on requested page.</returns>
        private IEnumerable<PersonInfo> getPageOfPersonalData(IEnumerable<PersonInfo> personInfo, int pageSize, int pageNumber)
        {
            if (pageSize > 0 && pageNumber > 0)
            {
                return personInfo.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return personInfo;
            }
        }
    }
}
