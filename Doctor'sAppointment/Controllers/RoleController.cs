using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_sAppointment.Controllers
{
    [Authorize ]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        public async Task<IActionResult> Index(string Search)
        {
            try
            {
                if (Search == null)
                {
                    var roleVM = await roleManager.Roles.Select(R => new RoleVM()
                    {
                        Id = R.Id,
                        Name = R.Name
                    }).ToListAsync();
                    return View(roleVM);
                }
                else
                {
                    var role = await roleManager.FindByNameAsync(Search);
                    var roleVM = new RoleVM() { Id = role.Id, Name = role.Name };
                    return View(new List<RoleVM> { roleVM });
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View();
        }
        public async Task<IActionResult> Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleVM roleVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole() { Name = roleVM.Name };
                    await roleManager.CreateAsync(role);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(roleVM);
        }
        public async Task<IActionResult> Edit([FromRoute] string id, string ViewName = "Edit")
        {
            if (id == null)
                return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var roleVM = new RoleVM() { Id = role.Id, Name = role.Name };
            return View(ViewName, roleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleVM roleVM, [FromRoute] string id)
        {
            try
            {
                if (id != roleVM.Id)
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    var role = await roleManager.FindByIdAsync(id);
                    if (role == null)
                        return NotFound();
                    role.Id = roleVM.Id;
                    role.Name = roleVM.Name;
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(roleVM);
        }
        public async Task<IActionResult> Delete([FromRoute] string id) => await Edit(id, "Delete");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmeDelete(string id)
        {
            try
            {
                var Role = await roleManager.FindByIdAsync(id);
                if (Role == null)
                    return NotFound();
                await roleManager.DeleteAsync(Role);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return RedirectToAction("Error", "Home");
        }
        public async Task<IActionResult> Details([FromRoute] string id) => await Edit(id, "Details");
    }
}

