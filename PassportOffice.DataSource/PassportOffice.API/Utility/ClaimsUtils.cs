namespace PassportOffice.API.Utility
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// Helps to manage claims at this layer of application.
    /// </summary>
    public class ClaimsUtils
    {
        /// <summary>
        /// Represents type of user name claim.
        /// </summary>
        private static readonly string ClaimTypeUserName = "username";

        /// <summary>
        /// Create claim for passed name of user.
        /// </summary>
        /// <param name="username">Name of user.</param>
        /// <returns>Claim which specified uniform type for names of users.</returns>
        public static Claim CreateUserNameClaim(string username)
        {
            return new Claim(ClaimsUtils.ClaimTypeUserName, username);
        }

        /// <summary>
        /// Finds claim of user name among collection of claims.
        /// </summary>
        /// <param name="claims">Collection to find among.</param>
        /// <returns>Name of user which contained in collection or null if there is no claim of such type.</returns>
        public static string FindUserName(IEnumerable<Claim> claims)
        {
            foreach (var c in claims)
            {
                if (ClaimsUtils.IsUserNameClaim(c))
                {
                    return c.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks claim that its typed as name of user.
        /// </summary>
        /// <param name="claim">Verified claim.</param>
        /// <returns>Flag which identifies that type of claim is for name of users.</returns>
        private static bool IsUserNameClaim(Claim claim)
        {
            return claim.Type.Equals(ClaimsUtils.ClaimTypeUserName);
        }
    }
}