using System.Collections.Generic;

namespace VTS.Web.Models
{
    /// <summary>
    /// Model for edit users on the clerk page.
    /// </summary>
    public class EditUsersModel
    {
        /// <summary>
        /// Gets or sets user's role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets list of users.
        /// </summary>
        public IEnumerable<Core.DTO.User> Users { get; set; }
    }
}