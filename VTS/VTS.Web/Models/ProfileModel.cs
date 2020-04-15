using System.ComponentModel.DataAnnotations;

namespace VTS.Web.Models
{
    /// <summary>
    /// Profile model.
    /// </summary>
    public class ProfileModel
    {
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        [Required]
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets user firstname.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user lastname.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
