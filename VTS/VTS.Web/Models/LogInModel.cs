using System.ComponentModel.DataAnnotations;

namespace VTS.Web.Models
{
    /// <summary>
    /// Login model.
    /// </summary>
    public class LogInModel
    {
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        [Required]
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets user email address.
        /// </summary>
        [Required(ErrorMessage = "No email specified")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
    }
}