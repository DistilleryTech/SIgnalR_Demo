namespace SignalRKit.Web.Helpers
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Extension methods for IPrincipal
    /// </summary>
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Gets the user name
        /// </summary>
        /// <param name="user">IPrincipal user</param>
        /// <returns>The user name</returns>
        public static String GetUserName(this IPrincipal user)
        {
            return user.GetClaimValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Gets the user identificator
        /// </summary>
        /// <param name="user">IPrincipal user</param>
        /// <returns>The user identificator</returns>
        public static Int64? GetUserId(this IPrincipal user)
        {
            var claimValue = user.GetClaimValue(ClaimTypes.NameIdentifier);

            Int64 userId;
            if (Int64.TryParse(claimValue, out userId))
                return userId;

            return null;
        }

        /// <summary>
        /// Gets the user identificator.
        /// This method works with authorized requests only!!!
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The user identificator</returns>
        public static Int64 GetUserIdValue(this IPrincipal user)
        {
            return user.GetUserId().Value;
        }

        /// <summary>
        /// Gets the claim value
        /// </summary>
        /// <param name="user">IPrincipal user</param>
        /// <param name="claimType">Type of the claim</param>
        /// <returns>The claim value</returns>
        private static String GetClaimValue(this IPrincipal user, String claimType)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(claimType);
            return claim == null ? null : claim.Value;
        }
    }
}