using AutoMapper;
using DAL.Models;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_sAppointment.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AccountUser> userManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<AccountUser> _userManager, IMapper _mapper)
        {
            userManager = _userManager;
            mapper = _mapper;
        }
        public async Task<IActionResult> Index(string Search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Search))
                {
                    var userVM = userManager.Users.Select(U => new UsersVM()
                    {
                        Id = U.Id,
                        FullName = U.FullName,
                        Address = U.Address,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber,
                        Agree = U.Agree,
                        Roles = userManager.GetRolesAsync(U).Result
                    }).ToList();
                    return View(userVM);
                }
                else
                {
                    var user = await userManager.FindByNameAsync(Search);
                    if (user != null)
                    {
                        var userVM = new UsersVM()
                        {
                            Id = user.Id,
                            FullName = user.FullName,
                            Address = user.Address,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            Agree = user.Agree,
                            Roles = userManager.GetRolesAsync(user).Result
                        };
                        return View(new List<UsersVM> { userVM });
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Not Found");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View();
        }
        public async Task<IActionResult> Edit([FromRoute] string id, string ViewName = "Edit")
        {
            if (id is null)
                return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var Roles =await userManager.GetRolesAsync(user);
            user.oldRole = Roles.FirstOrDefault();
            var userVM = mapper.Map<AccountUser, UsersVM>(user);
            return View(ViewName, userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersVM usersVM , [FromRoute] string id)
        {
            try
            {
                if(id != usersVM.Id)
                    return BadRequest();
                if(!ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(id);
                    if(user == null)
                        return NotFound();
                    user.PhoneNumber = usersVM.PhoneNumber;
                    user.Address = usersVM.Address;
                    user.FullName = usersVM.FullName;
                    await userManager.RemoveFromRoleAsync(user, usersVM.OldRole);
                    await userManager.AddToRoleAsync(user, usersVM.Role);
                    await userManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(usersVM);
        }
        public async Task<IActionResult> Delete([FromRoute] string id) => await Edit(id , "Delete");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete( string id)
        {
            try
            {
                var result = await userManager.FindByIdAsync(id);
                if (result == null)
                    return NotFound();
                await userManager.DeleteAsync(result);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View("Error" , "Home");
        }
        public async Task<IActionResult> Details([FromRoute] string id) => await Edit(id, "Details");
    }
}
