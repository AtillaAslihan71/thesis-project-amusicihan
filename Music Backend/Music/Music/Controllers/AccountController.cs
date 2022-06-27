using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.AccountViewModels;
using Business.ViewModels.HomeViewModel;
using Core;
using Core.Entities;
using Data.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Music.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly IGenreService _genreService;
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context,IGenreService genreService,IWebHostEnvironment env, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _env = env;
            _genreService = genreService;
            _context = context;
        }



        #region Register

        //Register Operation

        public IActionResult Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Problem", "Error");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            

            if (!ModelState.IsValid) return View(registerViewModel);

            ApplicationUser newUser = new ApplicationUser
            {

                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);


            if (identityResult.Succeeded)
            {
                
                await _signInManager.SignInAsync(newUser, false);
                return RedirectToAction("RegisterSecondStep", "Account");
                
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerViewModel);
            }
            

        }


        public async Task<IActionResult> RegisterSecondStep()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                

                bool isSelected = _context.UserGenres.Any(c => c.UserId == user.Id);
                if (isSelected)
                {

                    return RedirectToAction("Index", "MainUser");
                }

            }
            
            var item = await _genreService.GetAllAsync();
            ApplicationUser myUser = await _userManager.GetUserAsync(User);
            SecondRegisterViewModel m1 = new SecondRegisterViewModel()
            {
                UserId = myUser.Id
            };
            m1.Genres = item.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Name,
                isChecked = false
            }).ToList();

            return View(m1);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterSecondStep(SecondRegisterViewModel registerViewModel, UserGenre userGenre)
        {
            List<UserGenre> ug = new List<UserGenre>();
            
            ApplicationUser myUser = await _userManager.GetUserAsync(User);
            
            foreach (var item in registerViewModel.Genres)
            {
                if (item.isChecked == true)
                {
                    ug.Add(new UserGenre()
                    {
                        UserId = myUser.Id,
                        GenreId = item.Id
                    });
                }
            }

            foreach (var item in ug)
            {
                _context.UserGenres.Add(item);
                
            }

            //await _context.SaveChangesAsync();
            await _unitOfWork.SaveAsync();
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(myUser);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = myUser.Email }, Request.Scheme);
            
            var msgArea =
                $"<body style=\"height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: \'Lato\', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> This message is for contacting you!. </div><table style=\"border-collapse: collapse !important;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#2941AB\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#2941AB\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Welcome</h1> <img src=\"https://www.shareicon.net/data/128x128/2015/08/04/80120_controller_512x512.png\" width=\"125\" height=\"120\" style=\"border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; display: block; border: 0px;\" /></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Thank you for registering. Activate your account by clicking on the link below.</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td align=\"center\" style=\"border-radius: 3px;\" bgcolor=\"#2941AB\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #2941AB; display: inline-block;\">Verification</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">If an error occurs during verification, please contact the admin !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\"><a href=\"mailto:contact.a-musicihan@gmail.com\" target=\"_blank\" style=\"color: #2941AB;\">contact.a-musicihan@gmail.com</a></p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Thank you very much for your attention !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">By: <br>A-Musicihan</p></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br><p style=\"margin: 0; text-align: center;\">© 2022 All rights reserved | <a href=\"mailto:contact.a-musicihan@gmail.com\" target=\"_blank\" style=\"color: #2941AB; font-weight: 700;\"> A-Musicihan </a>.</p></td></tr></table></td></tr></table></body>";

            var subject = "A-Musicihan - Account Verification";

            bool emailResponse = Helper.SendEmail(myUser.Email, msgArea, subject);


            if (emailResponse)
            {
                await _userManager.AddToRoleAsync(myUser, UserRoles.User.ToString());
                await _signInManager.SignOutAsync();
                return RedirectToAction("ConfirmedEmail", "Account");
            }
            
            return RedirectToAction("Index", "Home");
        }


        //Register Operation - End

        #endregion

        #region Login

        //Login Operation

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Problem", "Error");
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string ReturnUrl)
        {

            if (!ModelState.IsValid) return View(loginViewModel);
            ApplicationUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is incorrect!");
                return View(loginViewModel);
            }
            

            if (!user.EmailConfirmed)
            {
                
                bool isSelected = _context.UserGenres.Any(c => c.UserId == user.Id);
                if (!isSelected)
                {
                    return RedirectToAction("Problem", "Error");
                }



                ModelState.AddModelError(string.Empty, "Please confirm your email! A confirmation message has been sent to your email !");
                return View(loginViewModel);
            }

            var signInResult =
                await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false,
                    true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Please wait a few minutes !");
                return View(loginViewModel);
            }


            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or password is incorrect !");
                return View(loginViewModel);
            }


            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //Login Operation - End

        #endregion

        #region Logout

        //Logout Operation

        public async Task<IActionResult> Logout(string ReturnUrl)
        {
            await _signInManager.SignOutAsync();

            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //Logout Operation - End

        #endregion

        #region Confirmation user email on register

        //Registration Success
        public ActionResult ConfirmedEmail()
        {
            return View();
        }

        //Confirmation Success
        public ActionResult ConfirmationEmail()
        {
            return View();
        }

        //Confirm Email Operation

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmationEmail" : "Error");
        }

        //Confirm Email Operation - End

        #endregion

        #region Forget password and reset password


        //Reset password
        public IActionResult ResetPass(string token, string email)
        {
            if (token == null && email == null)
            {
                ModelState.AddModelError("", "There is no account associated with the email you are looking for ! ");
            }

            return View();
        }

        //Reset password operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPass(ResetPassViewModel reset)
        {
            if (!ModelState.IsValid) return View(reset);

            var user = await _userManager.FindByEmailAsync(reset.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Account not found !  Make sure you enter the correct email !");
                return View(user);
            }



            var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            return RedirectToAction("ResetSuccess", "Account");

        }
        //Reset password operation - End

        //Reset password succesful
        public IActionResult ResetSuccess()
        {
            return View();
        }

        //Forget password
        public IActionResult ForgetPass()
        {
            return View();
        }

        //Forget password operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPass(ForgetPassViewModel forget)
        {
            if (!ModelState.IsValid) return View(forget);

            var user = await _userManager.FindByEmailAsync(forget.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Account not found !  Make sure you enter the correct email !");
                return View(user);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string confirmationLink = Url.Action("ResetPass", "Account", new
            {
                email = user.Email,
                token = token
            }, protocol: HttpContext.Request.Scheme);



            var msg =
                    $"<body style=\"height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: \'Lato\', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> This message is for contacting you!. </div><table style=\"border-collapse: collapse !important;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#2941AB\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#2941AB\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Welcome</h1> <img src=\"https://pixy.org/src2/575/thumbs350/5755475.jpg\" width=\"125\" height=\"120\" style=\"border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; display: block; border: 0px;\" /></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">You can update your password via the link below.</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td align=\"center\" style=\"border-radius: 3px;\" bgcolor=\"#2941AB\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #2941AB; display: inline-block;\">Update</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">If an error occurs while updating the password, contact the admin !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\"><a href=\"mailto:contact.a-musicihan@gmail.com\" target=\"_blank\" style=\"color: #2941AB;\">contact.a-musicihan@gmail.com</a></p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Thank you very much for your attention !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">By: <br>A-Musicihan</p></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #2941AB; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br><p style=\"margin: 0; text-align: center;\">© 2022 All rights reserved | <a href=\"mailto:contact.a-musicihan@gmail.com\" target=\"_blank\" style=\"color: #2941AB; font-weight: 700;\"> A-Musicihan </a>.</p></td></tr></table></td></tr></table></body>";

            var subject = "A-Musicihan - Password update";

            //SendMailHelper sendEmailHelper = new SendMailHelper();
            //bool emailResponse = SendEmail(forget.Email, msg);
            bool emailResponse = Helper.SendEmail(forget.Email, msg, subject);



            if (emailResponse)
            {
                return RedirectToAction("PassVerification", "Account");
            }

            return View();
        }
        //Forget password operation - End

        //Send email for reset forget account password successful
        public IActionResult PassVerification()
        {
            return View();
        }


        #endregion
        


        #region UserProfile

        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            ApplicationUser appUser = await _userManager.GetUserAsync(HttpContext.User);

            var userProfileViewModel = new UserProfileViewModel()
            {
                Name = appUser.Name,
                Username = appUser.UserName,
                Surname = appUser.Surname,
                Email = appUser.Email,

                Genres = await _genreService.GetAllAsync(),
                UserGenres = _context.UserGenres.Where(u=>u.UserId==appUser.Id).ToList(),
                ApplicationUsers = _context.Users.Where(u=>u.Id==appUser.Id).ToList()
            };

            return View(userProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserProfileViewModel userSettingsViewModel)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);


                var result = await _userManager.ChangePasswordAsync(user,
                    userSettingsViewModel.CurrentPassword, userSettingsViewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("Problem", "Error");
                }

                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }

            return RedirectToAction("Problem", "Error");
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserData(UserProfileViewModel userSettingsViewModel)
        {

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);




            user.Name = userSettingsViewModel.Name;
            user.UserName = userSettingsViewModel.Username;
            user.Surname = userSettingsViewModel.Surname;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Profile");
        }

        #endregion


        #region for create roles

        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}

        #endregion


    }
}
