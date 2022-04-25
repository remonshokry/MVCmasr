using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCmasr.Models;
using MVCmasr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCmasr.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly INotyfService notyfService;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            INotyfService notyfService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.notyfService = notyfService;
        }

        #region Registeration
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Role"] = "user";
            ViewData["Header"] = "Register User";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (ModelState.IsValid == false)
            {
                notyfService.Error("There is an Error in User Data");
                return View(registerUser);
            }
            try
            {
                #region Get Picture File
                var files = Request.Form.Files;

                if (!files.Any())
                {
                    ModelState.AddModelError("Picture", "Please Select a Picture!");
                    return View(registerUser);
                }

                var picture = files.FirstOrDefault();
                var allowedExtensions = new List<string> { ".jpg", ".png" };

                if (!allowedExtensions.Contains(Path.GetExtension(picture.FileName).ToLower()))
                {
                    ModelState.AddModelError("Picture", "Only .jpg and .png images are allowed!");
                    return View(registerUser);
                }

                if (picture.Length > 1048576)
                {
                    ModelState.AddModelError("Picture", "poster cant be more then 1 MB!");
                    return View(registerUser);
                }

                using var dataStream = new MemoryStream();

                await picture.CopyToAsync(dataStream);
                #endregion

                #region Register User
                ApplicationUser userModel = new ApplicationUser
                {
                    Name = registerUser.UserName,
                    UserName = Guid.NewGuid().ToString(),
                    Email = registerUser.Email,
                    Age = registerUser.Age,
                    PhoneNumber = registerUser.Phone,
                    Address = registerUser.Address,
                    Picture = dataStream.ToArray()
                };

                IdentityResult creationResult =
                    await userManager.CreateAsync(userModel, registerUser.Password);

                if (!creationResult.Succeeded)
                {
                    foreach (var error in creationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                #endregion

                #region Add User Claims
                var creatingClaimsResult = await userManager.AddClaimsAsync(userModel, new List<Claim>
                {
                    //new Claim (ClaimTypes.NameIdentifier, userModel.Name), 
                    new Claim ("Name", userModel.Name),
                    new Claim ("Id", userModel.Id),
                    new Claim (ClaimTypes.Email, userModel.Email),
                    new Claim (ClaimTypes.Role, registerUser.Role),
                    new Claim("Age", userModel.Age.ToString())
                });

                if (!creatingClaimsResult.Succeeded)
                {
                    await userManager.DeleteAsync(userModel);
                    foreach (var error in creatingClaimsResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                #endregion

                #region Redirect on Success register
                if (registerUser.Role == "user")
                {
                    notyfService.Success("User Successfully Created");
                    return RedirectToAction("Login");
                }
                else
                {
                    notyfService.Success("Admin Successfully Created");
                    return RedirectToAction("Index", "Home");
                }
                #endregion

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            notyfService.Error("There is an Error in User Data");
            return View(registerUser);
        }

        //[Authorize(Policy = "AdminsOnly")]
        public IActionResult RegisterAdmin()
        {
            ViewData["Role"] = "admin";
            ViewData["Header"] = "Register Admin";
            return View(nameof(Register));
        }
        #endregion

        #region SIGNOUT
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        #region LOGIN
        [HttpGet]
        public IActionResult Login(string ReturnUrl = "~/Home/index")
        {
            ViewData["RedirectURL"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser, string ReturnUrl = "~/Home/index")
        {
            if (ModelState.IsValid == false)
            {
                return View(loginUser);
            }
            ApplicationUser appUser = await userManager.FindByEmailAsync(loginUser.Email);

            if(appUser is null)
            {
                ModelState.AddModelError(String.Empty, "Email is not registered");
                return View(loginUser);
            }

            var isLocked = await userManager.IsLockedOutAsync(appUser);
            if (isLocked)
            {
                ModelState.AddModelError(String.Empty, "You're locked. Please try again later");
            }

            var isAuthenticated = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
            if (!isAuthenticated)
            {
                await userManager.AccessFailedAsync(appUser);
                ModelState.AddModelError(String.Empty, "password is incorrect");
            }

            var userClaims = await userManager.GetClaimsAsync(appUser);

            await signInManager.SignInWithClaimsAsync(appUser, loginUser.RememberMe, userClaims);

            //return RedirectToAction("Index", "Home");
            return LocalRedirect(ReturnUrl);
        }
        #endregion

    }
}
