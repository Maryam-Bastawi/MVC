using company.ass.DAL.models;
using company.ass.pl.helpers;
using company.ass.pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace company.ass.pl.Controllers
{
    [Authorize(Roles = "hr")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(string searchInput)
        {
            var Roles = Enumerable.Empty<RolesViewModel>();

            if (string.IsNullOrEmpty(searchInput))
            {
                Roles = await _roleManager.Roles.Select(R => new RolesViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name,

                }).ToListAsync();
            }
            else
            {
                Roles = await _roleManager.Roles.Where(U => U.Name.ToLower()
                                  .Contains(searchInput.ToLower()))
                                  .Select(U => new RolesViewModel()
                                  {

                                      RoleName = U.Name

                                  }).ToListAsync();
            }
            return View(Roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roles = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(roles);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var RoleFromDB = await _roleManager.FindByIdAsync(id);

            if (RoleFromDB is null) return NotFound();

            var Role = new RolesViewModel()
            {
                Id = RoleFromDB.Id,
                RoleName = RoleFromDB.Name

            };

            return View(ViewName, Role);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            if (id is null) return BadRequest();

            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, RolesViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var RoleFromDB = await _roleManager.FindByIdAsync(id);

                    if (RoleFromDB is null) return NotFound();

                    RoleFromDB.Name = model.RoleName;


                    var result = await _roleManager.UpdateAsync(RoleFromDB);

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
        public async Task<IActionResult> Delete([FromRoute] string? id, RolesViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var RoleFromDB = await _roleManager.FindByIdAsync(id);
                    if (RoleFromDB is null) return NotFound();

                    var result = await _roleManager.DeleteAsync(RoleFromDB);

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
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            ViewData["RoleId"] = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();
            var UsersInRole = new List<UserInRoleViewModel>();
            var Users = await _userManager.Users.ToListAsync();
            foreach (var user in Users)
            {
                var UserInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {

                    UserInRole.IsSelect = true;

                }
                else
                {
                    UserInRole.IsSelect = false;

                }

                UsersInRole.Add(UserInRole);
            }
            return View(UsersInRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId , List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();
            
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    if(appuser is not null) {

                        if (user.IsSelect && ! await  _userManager.IsInRoleAsync(appuser,role.Name))
                    {
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                    }
                    else if (!user.IsSelect && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);

                    }
                    }
                   
                }

                return RedirectToAction(nameof(Update) , new {id = roleId});

            }
            return View(users);

        }
    }
}
