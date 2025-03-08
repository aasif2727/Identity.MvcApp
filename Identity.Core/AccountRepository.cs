using Identity.Core.Entites;
using Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    public class AccountRepository: IAccountRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AccountRepository> _logger;
        public AccountRepository(ApplicationDbContext db, ILogger<AccountRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            try
            {
                return await _db.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message.ToString()}");
                throw ex;
            }
        }

        public async Task<IdentityRole> GetRoleById(string roleId)
        {
             return await _db.Roles.FirstOrDefaultAsync(u => u.Id == roleId);
        }

        public async Task<int> GetUserExistsByRoleId(string roleId)
        {
             return await Task.FromResult<int>(_db.UserRoles.Where(u => u.RoleId == roleId).Count());
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _db.ApplicationUser.ToListAsync();
        }
    }
}
