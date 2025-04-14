using AutoMapper;
using BLL.Interfaces;
using BLL.Repos;
using DAL.Models;
using Doctor_sAppointment.Helpers;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Doctor_sAppointment.Controllers
{
    [AllowAnonymous]
    public class AccountUserController : Controller
    {
        private readonly UserManager<AccountUser> userManager;
        private readonly SignInManager<AccountUser> signInManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountUserController(UserManager<AccountUser> _userManager, SignInManager<AccountUser> _signInManager , IUnitOfWork _unitOfWork)
        {
            signInManager = _signInManager;
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }
        
        public IActionResult Register() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountUserVM userVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await userManager.FindByEmailAsync(userVM.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError(string.Empty, "Email already registered.");
                        return View(userVM);
                    }
                    var user = new AccountUser()
                    {
                        FullName = userVM.FullName,
                        UserName = userVM.Email.Split("@")[0],
                        Email = userVM.Email,
                        PhoneNumber = userVM.PhoneNumber,
                        Address = userVM.Address,
                        Agree = userVM.Agree,
                        Password = userVM.Password,
                        PasswordCheck = userVM.PasswordCheck
                    };
                    var flag = await userManager.CreateAsync(user, userVM.Password);
                    if (flag.Succeeded)
                    {
                        var patient = new Patient
                        {
                            Id = user.Id,
                            AccountUserId = user.Id,
                            FullName = userVM.FullName,
                            Address = userVM.Address,
                            Email = userVM.Email,
                            PhoneNumber = userVM.PhoneNumber,
                        };
                        await unitOfWork.PatientRepo.Add(patient);
                        await unitOfWork.Compelete();
                        await userManager.AddToRoleAsync(user, "Patients");
                        return RedirectToAction("Login");
                    }
                    else
                        foreach (var error in flag.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(userVM);
        }
        public IActionResult Login() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(loginVM.Email);
                    if (user != null)
                    {
                        var result = await userManager.CheckPasswordAsync(user, loginVM.Password);
                        if(result)
                        {
                            var flag = await signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
                            if(flag.Succeeded)
                                return RedirectToAction("Index", "Home");
                        }
                        else { ModelState.AddModelError(string.Empty, "Invalid Password"); }
                    }
                    else { ModelState.AddModelError(string.Empty, "Invalid UserName"); }
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(loginVM);
        }
        public async Task<IActionResult> Signout ()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ForgetPassword() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(ForgetPassVM passwordVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await userManager.FindByNameAsync(passwordVM.UserName);
                    if (result != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(result);
                        var ResetPasswordLink = Url.Action("ResetPassword", "AccountUser", new { username = result.UserName, Token = token }, Request.Scheme);
                        string SendToEmail = result.UserName.Contains("@") ? result.UserName : $"{result.UserName}@gmail.com";
                        var email = new Email()
                        {
                            Subject = "Reset Password",
                            To = SendToEmail,
                            Body = ResetPasswordLink
                        };
                        EmailSitting.SendEmail(email);
                        return RedirectToAction("CheckYourInbox");
                    }
                    else { ModelState.AddModelError(string.Empty, "Email Not Found"); }
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View("ForgetPassword", passwordVM);
        }
        public IActionResult CheckYourInbox() => View();
        public IActionResult ResetPassword(string username, string token)
        {
            ViewBag.email = username;
            ViewBag.token = token;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string username, string token, ResetPassVM resetPasswordVM)
        {
            var User = await userManager.FindByNameAsync(username);
            var Result = await userManager.ResetPasswordAsync(User, token, resetPasswordVM.Password);
            if (Result.Succeeded)
                return RedirectToAction("LogIn");
            else
                foreach (var item in Result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
            return View(resetPasswordVM);
        }
    }
}
