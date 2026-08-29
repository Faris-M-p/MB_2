using MB_2.Models;
using MB_2.Models.Entity;
using MB_2.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MB_2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
           var responce = await  _employeeRepository.GetAllEmployees();
            return View(responce);
        }


        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Employee/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _employeeRepository.GetEmployeeById(id);

            return View(response);
        }
        // GET: EmployeeController/Create
        [HttpPost]
        public async Task<IActionResult> Create(InputEmployeeCreate input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }
            await _employeeRepository.CreateEmployee(input);

            return RedirectToAction("Index");
        }


       

        [HttpPost]
        public async Task<IActionResult> Update(InputEmployeeUpdate input)
        {
            await _employeeRepository.UpdateEmployee(input);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InputEmployeeDelete input)
        {
            await _employeeRepository.DeleteEmployee(input.FK_Employee);

            return RedirectToAction("Index");
        }



        
    }
}
