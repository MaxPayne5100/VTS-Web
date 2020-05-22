using System.ComponentModel.DataAnnotations;

namespace VTS.Web.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets First Name.
        /// </summary>
        [Required(ErrorMessage = "Ваше ім'я не було вказано")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Last Name.
        /// </summary>
        [Required(ErrorMessage = "Ваше прізвище не було вказано")]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        [Required(ErrorMessage = "Ваша електронна пошта не була вказана")]
        [EmailAddress(ErrorMessage = "Неправильна електронна пошта")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Role.
        /// </summary>
        [Required(ErrorMessage = "Ваша роль не була вказана")]
        [MaxLength(15)]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets Employee Manager.
        /// </summary>
        public string ManagerId { get; set; }
    }
}