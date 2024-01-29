using Airtrafic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Airtrafic.Controllers
{
    public class UsersController : Controller
    {
        private readonly RoleManager<AirtraficUserRole> _roleManager;
        private readonly UserManager<AirtraficUser> _userManager;
        private readonly SignInManager<AirtraficUser> _signInManager;


        public UsersController(RoleManager<AirtraficUserRole> roleManager,UserManager<AirtraficUser> userManager,SignInManager<AirtraficUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var role = await _roleManager.FindByNameAsync(roles[0]);
                    user.UserRole = role;
                    user.UserRoleId = role.Id; 
                }
            }

            return View(users);
        }


        public async Task<IActionResult> Edit(string  id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("UserRoleId,Id")] AirtraficUser UserData)
        {
            var roles = _roleManager.Roles.ToList();
            var user = await _userManager.FindByIdAsync(UserData.Id);
            var role = roles.Where(r => r.Id == UserData.UserRoleId).FirstOrDefault();

            await _userManager.RemoveFromRolesAsync(user, roles.Select(r => r.Name));
            var result = await _userManager.AddToRoleAsync(user,role.Name);
            var claims = await _userManager.GetClaimsAsync(user);
            if(!claims.Where(c => c.Type == ClaimTypes.Role && c.Value == role.Name).Any())
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role.Name));
            } 
                var roleClaims = claims.Where(c => c.Type == ClaimTypes.Role && c.Value != role.Name).ToList();
                await _userManager.RemoveClaimsAsync(user,roleClaims);

            var claimsFromManager = await _userManager.GetClaimsAsync(user);
            var differentClaims = User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();


            if (!result.Errors.Any())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
    }
}
