using MB_2.Models;
using MB_2.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MB_2.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MB_2.Models.Entity.AppDbContext _context;

        public TaskRepository(MB_2.Models.Entity.AppDbContext context)
        {
            {
                _context = context;
            }
        }

        public async Task<List<OutPutTaskList>> GetAllTasks(string searchname = "", string namesort = "", int? filterstatus = null, int page=1, int pagesize=5)
        {
            var today = DateTime.Today;
            var query= _context.Task
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(searchname))
            {
                query = query.Where(x => x.Title.Contains(searchname)|| x.Description.Contains(searchname));
            }
            if (!string.IsNullOrEmpty(namesort))
            {
                if (namesort == "ASC")
                {
                    query = query.OrderBy(x => x.Title);

                }
                else if (namesort == "DESC")
                {
                    query = query.OrderByDescending(x => x.Title);

                }
            }
            if (filterstatus.HasValue)
            {
                if (filterstatus.Value == 1)
                {
                    query = query.Where(x => x.Completed == false && x.DueDate >= today);
                }
                else if (filterstatus.Value == 2)
                {
                    query = query.Where(x => x.Completed == true);
                }
                else if (filterstatus.Value == 3)
                {
                    query = query.Where(x => x.Completed == false && x.DueDate < today);
                }
            }
            query = query.Skip((page - 1 )* pagesize)
                .Take(pagesize);

            var response = await query
                .Select(x => new OutPutTaskList
                {
                    FK_Task = x.ID_Task,
                    Title = x.Title,
                    Description = x.Description,
                    FK_Employee = x.FK_Employee,
                    CreatedDate = x.CreatedDate,
                    DueDate = x.DueDate,
                    CompletedDate = x.CompletedDate,
                    Completed = x.Completed,
                    Status = x.Completed ? 2 : (x.DueDate < today ? 3 : 1),

                })
                .ToListAsync();

            return response;
        }


        public async Task<OutPutTaskList> GetTaskById(int FK_Task)
        {
            var today = DateTime.Today;
            var response = await _context.Task

                .Where(x => x.ID_Task == FK_Task && x.IsDeleted == false)
                .Select(x => new OutPutTaskList
                {
                    FK_Task = x.ID_Task,
                    Title = x.Title,
                    Description = x.Description,
                    FK_Employee = x.FK_Employee,
                    CreatedDate = x.CreatedDate,
                    DueDate = x.DueDate,
                    CompletedDate = x.CompletedDate,
                    Completed = x.Completed,
                    Status = x.Completed ? 2 : (x.DueDate < today ? 3 : 1),
                })
                .FirstOrDefaultAsync();
            return response;
        }

        public async Task<bool> DeleteTask(int FK_task)
        {
            try {
                var task = _context.Task
                    .Where(x =>  x.ID_Task == FK_task && x.IsDeleted == false)
                    .FirstOrDefault();
                if (task is null)
                    {
                    return false;
                    }
                task.IsDeleted = true;

                //  _context.Task.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch {
                return false;
            }
      
        }

        public async Task <bool>UpdateTask(InputTaskUpdate input)
        { 
            try{
                var task = _context.Task.
                        Where(x => x.IsDeleted == false && x.ID_Task == input.FK_Task).
                        FirstOrDefault();
                if (task == null)
                    return false;
                task.Title = input.Title;
                task.Description = input.Description;
                task.FK_Employee = input.FK_Employee;
                task.DueDate = input.DueDate;
                task.Completed = input.Completed;
                if (input.Completed)
                {
                    task.CompletedDate = DateTime.Now;
                }
                else
                {
                    task.CompletedDate = null;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch{
                return false;
            }


        }

        public async Task<bool> CreateTask(InputTaskCreate input)
        {
            try
            {
                var task = new MB_2.Models.Entity.Task
                {
                    Title = input.Title,
                    Description = input.Description,
                    FK_Employee = input.FK_Employee.Value,
                    CreatedDate = DateTime.Now,
                    DueDate = input.DueDate,
                    CompletedDate = null,
                    Completed = false
                };

                _context.Task.Add(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }


        }

        
    }
} 
