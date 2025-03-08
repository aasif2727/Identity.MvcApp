using Identity.App.Models.ViewModels;
using Identity.Core.Entites;
using Identity.Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.App.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRepository _accountRepository;

        public RoleController(ILogger<RoleController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_accountRepository.GetRoles().Result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: {ex.Message}");
                return null;
            }
        }

        [HttpGet]
        public IActionResult Upsert(string roleId)
        {
            if (String.IsNullOrEmpty(roleId))
            {
                //create
                return View();
            }
            else
            {
                //update
                var objFromDb = _accountRepository.GetRoleById(roleId).GetAwaiter().GetResult();
                return View(objFromDb);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            if (await _roleManager.RoleExistsAsync(roleObj.Name))
            {
                TempData["error"] = "Role exists!!";
            }
            if (String.IsNullOrEmpty(roleObj.NormalizedName))
            {
                //create
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                TempData["success"] = "Role created successfully";
            }
            else
            {
                //update
                var objFromDb = _accountRepository.GetRoleById(roleObj.Id).GetAwaiter().GetResult();
                objFromDb.Name = roleObj.Name;
                objFromDb.NormalizedName = roleObj.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(objFromDb);
                TempData["success"] = "Role updated successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "OnlySuperAdminChecker")]
        public async Task<IActionResult> Delete(string roleId)
        {

            var objFromDb = _accountRepository.GetRoleById(roleId).GetAwaiter().GetResult();
            if (objFromDb != null)
            {

                var userRolesForThisRole = _accountRepository.GetUserExistsByRoleId(roleId).Result;
                if (userRolesForThisRole > 0)
                {
                    TempData["error"] = "Cannot delete this role, since there are users assigned to this role.";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _roleManager.DeleteAsync(objFromDb);
                TempData["success"] = "Role deleted successfully";
            }
            else
            {
                TempData["error"] = "Role not found.";
            }
            return RedirectToAction(nameof(Index));
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
