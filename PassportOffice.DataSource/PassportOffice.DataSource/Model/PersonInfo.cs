namespace PassportOffice.DataSource.Model
{
    using System;

    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents information about person.
    /// </summary>
    public class PersonInfo
    {
        /// <summary>
        /// Unique identifier of record in database.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// First name of person.
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of person.
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        public string LastName { get; set; }

        /// <summary>
        /// Middle name of person.
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Person's birthday date.
        /// </summary>
        public DateTime BirthdayDate { get; set; }

        /// <summary>
        /// Residence address of person.
        /// </summary>
        [StringLength(400, MinimumLength = 0)]
        public string Address { get; set; }

        /// <summary>
        /// Series of person's passport.
        /// </summary>
        [StringLength(15, MinimumLength = 1)]
        public string PassportSeries { get; set; }

        /// <summary>
        /// Number of person's passport.
        /// </summary>
        [StringLength(15, MinimumLength = 1)]
        public string PassportNumber { get; set; }

        /// <summary>
        /// Date of issue of person's passport.
        /// </summary>
        public DateTime PassportIssueDate { get; set; }
    }
}
