using System.ComponentModel.DataAnnotations;

namespace Clinic.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not confirmed")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
