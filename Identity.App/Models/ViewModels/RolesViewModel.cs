using Identity.Core.Entites;
using System.ComponentModel.DataAnnotations;

namespace Identity.App.Models.ViewModels
{
    public class RolesViewModel
    {
        public RolesViewModel()
        {
            RolesList = [];
        }
        public ApplicationUser User { get; set; }
        public List<RoleSelection> RolesList { get; set; }
    }


    public class RoleSelection
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
