using AutoMapper;
using company.ass.BLL.interfaces;
using company.ass.BLL.Repositories;
using company.ass.DAL.models;
using company.ass.pl.helpers;
using company.ass.pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Dependency;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace company.ass.pl.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitofwork _unitofwork;

        public EmployeeController(/*IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository*/
            IMapper mapper,
            IUnitofwork unitofwork)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitofwork = unitofwork;
        }
       
        public async Task<IActionResult> Index(string searchinput)
        {
            var employee0 = Enumerable.Empty<Employee>();
            //var EmployeeViewModel = new Collection<EmployeeViewModel>();

            if (string.IsNullOrEmpty(searchinput))
            {
                 employee0 = await _unitofwork.EmployeeRepository.GetAllasync();

            }
            else
            {
                employee0 = await _unitofwork.EmployeeRepository.getbynameasync(searchinput);
            }
            string message = "hello world";
            //auto mapping 
           var result =  _mapper.Map<IEnumerable<EmployeeViewModel>>(employee0);
            

            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department = await _unitofwork.DepartmentRepository.GetAllasync();
            ViewData["department"] = department;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImgName = DocumentSettings.UplaodFile(model.Image,"img");
                  //casting from empviewmodel to employee 
                //manualcasting
                /*                Employee employee = new Employee()
                                {
                                    name = model.name,
                                    age = model.age,
                                    address = model.address,
                                    salary = model.salary,
                                    IsActive = model.IsActive,
                                    PhoneNumber = model.PhoneNumber,
                                    Email = model.Email,
                                    HiringDate = model.HiringDate,
                                     workfor = model.workfor,
                                    workforid = model.workforid
                                };*/
                //auto mapping 
               var employee = _mapper.Map<Employee>(model);

                var count = await _unitofwork.EmployeeRepository.addasync(employee);
                if (count > 0)
                {
                    TempData["message"] = "employee is created successfully";
                }
                else
                {
                    TempData["message"] = "employee is not created successfully";

                }
                return RedirectToAction(nameof(Index));

            }

            return View(model);
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {

            if (id is null) return BadRequest();
            var employee = await _unitofwork.EmployeeRepository.Getasync(id.Value);

            if (employee is null) return NotFound();
            var employees = _mapper.Map<EmployeeViewModel>(employee);

            return View(viewName, employees);
        }

        [HttpGet]
        public async Task<IActionResult> update(int? id)
        {

            var department = await _unitofwork.DepartmentRepository.GetAllasync();
            ViewData["department"] = department;

            return await Details(id, "update");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update([FromRoute] int? id, EmployeeViewModel model)
        {
           

                if (id != model.id) return BadRequest();
                if (ModelState.IsValid)  {
                try {
                    

                    if(model.ImgName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImgName, "img");
                    }
                    if (model.Image is not null)
                    {
                        model.ImgName = DocumentSettings.UplaodFile(model.Image, "img");
                    }
                    //auto mapping 
                    var employee = _mapper.Map<Employee>(model);

                    var count = await _unitofwork.EmployeeRepository.updateasync(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }


            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel model)
        {
            
            /*Employee employee = new Employee()
            {
                name = model.name,
                age = model.age,
                address = model.address,
                salary = model.salary,
                IsActive = model.IsActive,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                HiringDate = model.HiringDate,
                workfor = model.workfor,
                workforid = model.workforid
            };*/
            //auto mapping 
            var employee = _mapper.Map<Employee>(model);
            try
            {
                if (id != employee.id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count = await _unitofwork.EmployeeRepository.Deleteasync(employee);
                    if (count > 0)
                    {
                        DocumentSettings.DeleteFile(model.ImgName, "img");
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
