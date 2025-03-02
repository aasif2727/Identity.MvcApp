using System.ComponentModel.DataAnnotations;

namespace Identity.App.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
