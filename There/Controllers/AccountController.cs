using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.ViewModels;

namespace There.Controllers
{
    public class AccountController:Controller
    {
        private readonly SignInManager<IdentityUser> _signinmanamer;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager
            ,UserManager<IdentityUser>  userManager )
        {
            _signinmanamer = signInManager;
            _userManager = userManager;
        }

        //登录 
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            else
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if(user!=null)
                {
                    var result = await _signinmanamer.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Register", "Account");
                    }
                   
                }
                ModelState.AddModelError("", "用户名/密码不正确");
                return View(loginViewModel);

            }
        }

        //注册
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVierModel registerVierModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName=registerVierModel.UserName
                };

                var result = await _userManager.CreateAsync(user, registerVierModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            return View(registerVierModel);
        }


        //登出
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signinmanamer.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
