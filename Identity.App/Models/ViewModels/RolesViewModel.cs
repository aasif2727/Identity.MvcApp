using System.ComponentModel.DataAnnotations;

namespace Identity.App.Models.ViewModels
{
    public class RolesViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NormalizedName { get; set; }
    }
}
