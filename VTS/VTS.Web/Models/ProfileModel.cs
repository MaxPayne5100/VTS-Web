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
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets user firstname.
        /// </summary>
        [Required(ErrorMessage = "Ваше ім'я не було вказано")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user lastname.
        /// </summary>
        [Required(ErrorMessage = "Ваше прізвище не було вказано")]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user email.
        /// </summary>
        [Required(ErrorMessage = "Ваша електронна пошта не була вказана")]
        [EmailAddress(ErrorMessage = "Неправильна електронна пошта")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user role.
        /// </summary>
        public string Role { get; set; }
    }
}
