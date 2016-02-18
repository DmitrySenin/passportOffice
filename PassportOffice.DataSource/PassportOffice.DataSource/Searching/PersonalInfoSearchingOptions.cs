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
        public DateTime? BirthdayDate { get; set; }

        /// <summary>
        /// Checks that first name should be used for searching.
        /// </summary>
        /// <returns>Flag which identifies that option should be used.</returns>
        public bool UseFirstName()
        {
            return !string.IsNullOrEmpty(this.FirstName);
        }

        /// <summary>
        /// Checks that last name should be used for searching.
        /// </summary>
        /// <returns>Flag which identifies that option should be used.</returns>
        public bool UseLastName()
        {
            return !string.IsNullOrEmpty(this.LastName);
        }

        /// <summary>
        /// Checks that middle name should be used for searching.
        /// </summary>
        /// <returns>Flag which identifies that option should be used.</returns>
        public bool UseMiddleName()
        {
            return !string.IsNullOrEmpty(this.MiddleName);
        }

        /// <summary>
        /// Checks that series of passport should be used for searching.
        /// </summary>
        /// <returns>Flag which identifies that option should be used.</returns>
        public bool UsePassportSeries()
        {
            return !string.IsNullOrEmpty(this.PassportSeries);
        }

        /// <summary>
        /// Checks that number of passport should be used for searching.
        /// </summary>
        /// <returns>Flag which identifies that option should be used.</returns>
        public bool UsePassportNumber()
        {
            return !string.IsNullOrEmpty(this.PassportNumber);
        }

        /// <summary>
        /// Checks that date of birthday should be used for searching.
        /// </summary>
        /// <returns>Flag which identifies that option should be used.</returns>
        public bool UseBirthdayDate()
        {
            return this.BirthdayDate != null;
        }

        /// <summary>
        /// Check the correctness of passed searching options.
        /// </summary>
        /// <param name="searchingOptions">Searching options to be checked.</param>
        /// <returns>Flag which identifies correctness of searching options.</returns>
        public static bool CheckSearchingOptions(PersonalInfoSearchingOptions searchingOptions)
        {
            bool checkResult = true;

            if (searchingOptions == null)
            {
                checkResult = false;
            }

            return checkResult;
        }
    }
}
