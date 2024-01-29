using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Airtrafic.Models;
using Microsoft.AspNetCore.Identity;

namespace Airtrafic.Controllers
{
    public class AirtraficUserRolesController : Controller
    {
        private readonly RoleManager<AirtraficUserRole> _roleManager;
        private readonly UserManager<AirtraficUser> _userManager;

        public AirtraficUserRolesController(RoleManager<AirtraficUserRole> roleManager, UserManager<AirtraficUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: AirtraficUserRoles
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: AirtraficUserRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airtraficUserRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airtraficUserRole == null)
            {
                return NotFound();
            }

            return View(airtraficUserRole);
        }

        // GET: AirtraficUserRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,NormalizedName")] AirtraficUserRole airtraficUserRole)
        {
            try
            {
                if (!string.IsNullOrEmpty(airtraficUserRole.Name))
                {
                    if (!(await _roleManager.RoleExistsAsync(airtraficUserRole.Name)))
                    {
                        var result =await _roleManager.CreateAsync(airtraficUserRole);
                        if(result.Errors.Any())
                        {
                            return View();
                        }
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AirtraficUserRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airtraficUserRole = await _roleManager.FindByIdAsync(id);
            if (airtraficUserRole == null)
            {
                return NotFound();
            }
            return View(airtraficUserRole);
        }

        // POST: AirtraficUserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] AirtraficUserRole airtraficUserRole)
        {
            if (id != airtraficUserRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(airtraficUserRole);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _roleManager.RoleExistsAsync(airtraficUserRole.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(airtraficUserRole);
        }

        // GET: AirtraficUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airtraficUserRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airtraficUserRole == null)
            {
                return NotFound();
            }

            return View(airtraficUserRole);
        }

        // POST: AirtraficUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var airtraficUserRole = await _roleManager.FindByIdAsync(id);
            if (airtraficUserRole != null)
            {
                await _roleManager.DeleteAsync(airtraficUserRole);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
