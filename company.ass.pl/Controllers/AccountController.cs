using company.ass.DAL.models;
using company.ass.pl.helpers;
using company.ass.pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace company.ass.pl.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> UserManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = UserManager;
			_signInManager = signInManager;
		}
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid) //server side validation
            {   
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
					user = await _userManager.FindByEmailAsync(model.Email);

					if (user is null)
                    {
						//mapping: from signupviewmodel to applicationuser
						user = new ApplicationUser()
						{
							UserName = model.UserName,
							firstname = model.FirstName,
							lastname = model.LastName,
							Email = model.Email,
							IsAgree = model.IsAgree

						};
						var result = await _userManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
						{
							return RedirectToAction("SignIn");
						}

						foreach (var errors in result.Errors)
						{
							ModelState.AddModelError(string.Empty, errors.Description);
						}
					}
					ModelState.AddModelError(string.Empty, "email is aready exits !");

				}

				ModelState.AddModelError(string.Empty, "username is aready exits !");

			}
			return View(model);
        }

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async  Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try {
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							//sign in 
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe ,false);
							if (result.Succeeded)
							{
								return RedirectToAction("Index", "Home");

							}
						}
					}
					ModelState.AddModelError(string.Empty, "invalid login!!");
				}
				catch(Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}


			}
			return View();
		}

		public new async Task<IActionResult> SignOut()
		{
			await  _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendResertPassword(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if(user is not null)
				{
					//create token
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					//create reset password url

					var url = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme );
					//https://localhost:44301/Account/ResetPassword?email=maryam@gmail.com&token
					//create email
					var email = new Email()
					{
						To = model.Email,
						Subjict = "ResetPassword",
						Body = url
					};
					//send email
					EmailSetting.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}



			}
			ModelState.AddModelError(string.Empty, "Invaid operation , please try again !!");
			return View(model);
		}
		[HttpGet]

		public IActionResult CheckYourInbox()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();

		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
				}
			}
			ModelState.AddModelError(string.Empty, "Invaild Operation, Please Try Again!!");
			return View(model);
		}


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
