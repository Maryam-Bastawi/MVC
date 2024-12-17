using company.ass.BLL.interfaces;
using company.ass.BLL.Repositories;
using company.ass.DAL.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace company.ass.pl.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepositories;

        public DepartmentController(IDepartmentRepository departmentRepositories)
        {
            _departmentRepositories = departmentRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments0 = await _departmentRepositories.GetAllasync();
            return View(departments0);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Departments model)
        {
            if (ModelState.IsValid)
            {
                var count = await _departmentRepositories.addasync(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(model);
        }
        public async Task<IActionResult> Details(int? id ,string viewName= "Details")
        {
            if (id is null) return BadRequest();//400
            var department = await _departmentRepositories.Getasync(id.Value);
            if (department is null) return NotFound();
            return View( viewName ,department );
        }
        //update
        [HttpGet]
        public async Task<IActionResult> update(int? id)
        {
            /* if (id is null) return BadRequest();//400
             var department = _departmentRepositories.Get(id.Value);
             if (department is null) return NotFound();*/
            return await Details(id, "update");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update([FromRoute]int id , Departments model)
        {
            try
            {
                if (id != model.id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count = await _departmentRepositories.updateasync(model);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch(Exception ex )
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }

        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            /*if (id is null) return BadRequest();//400
            var department = _departmentRepositories.Get(id.Value);
            if (department is null) return NotFound();
            return View(department);*/
            return Details(id, "Delete");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Departments model)
        {
            try
            {
                if (id != model.id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count = await _departmentRepositories.Deleteasync(model);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }
    }
    }
