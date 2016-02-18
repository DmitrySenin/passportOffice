namespace PassportOffice.DataSource.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represent user's credentials.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's login name.
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
