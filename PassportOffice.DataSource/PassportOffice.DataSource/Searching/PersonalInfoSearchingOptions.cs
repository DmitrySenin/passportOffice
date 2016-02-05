namespace PassportOffice.DataSource.Searching
{
    using System;

    /// <summary>
    /// Represents options of searching over personal data.
    /// </summary>
    public class PersonalInfoSearchingOptions
    {
        /// <summary>
        /// Restriction of person's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Restriction of person's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Restriction of person's middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Restriction of series of passport.
        /// </summary>
        public string PassportSeries { get; set; }

        /// <summary>
        /// Restriction of number of passport.
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Restriction of person's date of birth.
        /// </summary>
        public DateTime BirthdayDate { get; set; }
    }
}
