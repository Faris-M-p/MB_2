using MB_2.Models;
using MB_2.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MB_2.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TaskController(ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
        }
        // GET: TaskController
        public async Task<ActionResult> Index(string searchname = "", string namesort = "", int? filterstatus = null,int page=1,int pagesize=5)
        {
           var responce = await  _taskRepository.GetAllTasks(searchname, namesort, filterstatus,page,pagesize);
            ViewBag.Page = page;
            ViewBag.pagesize = pagesize;
            return View(responce);
        }


        // GET: Task/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees = await _employeeRepository.GetAllEmployees("", "", null, 1, 1000);
            ViewBag.Employees = employees;
            return View();
        }

        // GET: Task/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _taskRepository.GetTaskById(id);
            var employees = await _employeeRepository.GetAllEmployees("", "", null, 1, 1000);
            ViewBag.Employees = employees;
            return View(response);
        }
        // GET: TaskController/Create
        [HttpPost]
        public async Task<IActionResult> Create(InputTaskCreate input)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeRepository.GetAllEmployees("", "", null, 1, 1000);
                ViewBag.Employees = employees;
                return View(input);
            }
            await _taskRepository.CreateTask(input);

            return RedirectToAction("Index");
        }


       

        [HttpPost]
        public async Task<IActionResult> Update(InputTaskUpdate input)
        {
            await _taskRepository.UpdateTask(input);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InputTaskDelete input)
        {
            await _taskRepository.DeleteTask(input.FK_Task);

            return RedirectToAction("Index");
        }



        
    }
}
