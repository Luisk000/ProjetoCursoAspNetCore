using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.ViewModels;

namespace WebApplication5.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ManagementController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ManagementController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        //[Route("")]


        [Route("[action]")]
        [Route("")]
        [AllowAnonymous]
        public ViewResult List()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }


        [Route("[action]/{id?}")]
        public ViewResult Details(int? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = employee,
                PageTitle = "Dados de funcionários"
            };
            return View(homeDetailsViewModel);

            //Employee model = _employeeRepository.GetEmployee(1);
            //ViewData["Management"] = model;
            //ViewBag.Management = model;
            //ViewData["Page Title"] = "employee details";
            //ViewBag.PageTitle = "Employee Details";
            //return View(model);
        }


        [Route("[action]")]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilleName = ProcessUploadedFile(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFilleName
                };
                _employeeRepository.Add(newEmployee);
                //Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }


        [Route("[action]")]
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Id = model.Id;
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                _employeeRepository.Update(employee);
                //Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("List");
            }
            return View();
        }

       [HttpPost]
       public IActionResult Erase(int id)
       {
            _employeeRepository.Delete(id);
            return RedirectToAction("List");
        }


        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFilleName = null;
            if (model.Photo != null)
            {
                string uploadsFoler = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFilleName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFoler, uniqueFilleName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }    
            }
            return uniqueFilleName;
        }
    }
}
