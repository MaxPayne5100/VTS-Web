using System.ComponentModel.DataAnnotations;

namespace VTS.Web.Models
{
    public class LogInModel
    {
        [Required]
        public uint Id { get; set; }

        [Required(ErrorMessage = "No email specified")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
    }
}