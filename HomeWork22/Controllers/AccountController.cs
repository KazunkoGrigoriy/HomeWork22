using HomeWork22.AuthApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin()
            {
                returnUrl = returnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var LoginResult = await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, false, lockoutOnFailure: false);

                if (LoginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.returnUrl))
                    {
                        return Redirect(model.returnUrl);
                    }
                    return RedirectToAction("RecordBook", "Record");
                }
            }

            ModelState.AddModelError("", "Пользователь не найден");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.UserName };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("RecordBook", "Record");
                }
                else
                {
                    foreach (var item in createResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
    }
}
