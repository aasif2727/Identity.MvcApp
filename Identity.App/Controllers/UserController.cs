using Identity.Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.App.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRepository _accountRepository;
        public UserController(ILogger<UserController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAccountRepository accountRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userList = _accountRepository.GetUsers().Result;

            foreach (var user in userList)
            {
                var user_role = await _userManager.GetRolesAsync(user) as List<string>;
                user.Role = String.Join(",", user_role);

                var user_claim = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult().Select(u => u.Type);
                user.UserClaim = String.Join(",", user_claim);
            }

            return View(userList);
        }
    }
}
