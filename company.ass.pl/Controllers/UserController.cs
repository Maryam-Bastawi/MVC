using company.ass.DAL.models;
using company.ass.pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace company.ass.pl.Controllers
{
    [Authorize(Roles = "hr")]
    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}
        [Authorize]
        public async Task<IActionResult> Index(string searchInput)
		{
			var user = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(searchInput))
			{
				user = await _userManager.Users.Select(U => new UserViewModel
				{
					Id = U.Id,
					FirstName = U.firstname,
					LastName = U.lastname,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
			}
			else
			{
				user = await _userManager.Users.Where(U => U.Email.ToLower()
								  .Contains(searchInput.ToLower()))
								  .Select(U => new UserViewModel()
								  {
									  Id = U.Id,
									  FirstName = U.firstname,
									  LastName = U.lastname,
									  Email = U.Email,
									  Roles = _userManager.GetRolesAsync(U).Result
								  }).ToListAsync();
			}
			return View(user);
		}

        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var userFromDB = await _userManager.FindByIdAsync(id);

            if (userFromDB is null) return NotFound();

            var user = new UserViewModel()
            {
                Id = userFromDB.Id,
                FirstName = userFromDB.firstname,
                LastName = userFromDB.lastname,
                Email = userFromDB.Email,
                Roles = _userManager.GetRolesAsync(userFromDB).Result
            };

            return View(ViewName, user);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userFromDB = await _userManager.FindByIdAsync(id);

                    if (userFromDB is null) return NotFound();

                    userFromDB.firstname = model.FirstName;
                    userFromDB.lastname = model.LastName;
                    userFromDB.Email = model.Email;

                    var result = await _userManager.UpdateAsync(userFromDB);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userFromDB = await _userManager.FindByIdAsync(id);

                    if (userFromDB is null) return NotFound();

                    var result = await _userManager.DeleteAsync(userFromDB);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }



    }
}
