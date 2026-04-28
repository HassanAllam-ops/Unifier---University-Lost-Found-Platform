using System.ComponentModel.DataAnnotations;
namespace Unifier___University_Lost___Found_Platform.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email مطلوب")]
        [EmailAddress(ErrorMessage = "Email غير صحيح")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password مطلوب")]
        public string Password { get; set; } = "";
    }
}
