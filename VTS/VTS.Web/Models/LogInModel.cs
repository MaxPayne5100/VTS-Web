using System.ComponentModel.DataAnnotations;
using VTS.Core.Attributes;

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
        [Required(ErrorMessage = "Ваше ID не було вказано")]
        [PosNumberNoZero(ErrorMessage = "ID повинно бути додатнім числом")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets user email address.
        /// </summary>
        [Required(ErrorMessage = "Ваша електронна пошта не була вказана")]
        [EmailAddress(ErrorMessage = "Неправильна електронна пошта")]
        public string Email { get; set; }
    }
}