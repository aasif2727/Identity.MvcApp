using Identity.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.IRepository
{
    public interface IAccountRepository
    {
        public Task<List<IdentityRole>> GetRoles();
        public Task<IdentityRole> GetRoleById(string roleId);
        public Task<int> GetUserExistsByRoleId(string roleId);
        public Task<List<ApplicationUser>> GetUsers();
    }
}
