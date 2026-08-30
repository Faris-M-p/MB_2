using MB_2.Models.Entity;
using MB_2.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace MB_2.Repository
{
    public class HomeRepository
    {
        private readonly AppDbContext dbContext;

        public HomeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OutputDashBoardData> GetDashBordData()
        {
            var query =
                from task in dbContext.Task
                join employee in dbContext.Employee
                    on task.FK_Employee equals employee.ID_Employee
                where !task.IsDeleted && !employee.IsDeleted
                select new
                {
                    Task = task,
                    Employee = employee
                };

            var totalEmployees = await dbContext.Employee
                .CountAsync(x => !x.IsDeleted);

            var activeEmployees = await dbContext.Employee
                .CountAsync(x => !x.IsDeleted && x.IsActive);

            var inactiveEmployees = await dbContext.Employee
                .CountAsync(x => !x.IsDeleted && !x.IsActive);

            var totalTasks = await query.CountAsync();

            var completedTasks = await query
                .CountAsync(x => x.Task.Completed);

            var pendingTasks = await query
                .CountAsync(x =>
                    !x.Task.Completed &&
                    x.Task.DueDate >= DateTime.Today);

            var overdueTasks = await query
                .CountAsync(x =>
                    !x.Task.Completed &&
                    x.Task.DueDate < DateTime.Today);

            return new OutputDashBoardData
            {
                TotalEmployees = totalEmployees,
                ActiveEmployees = activeEmployees,
                InactiveEmployees = inactiveEmployees,

                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                OverdueTasks = overdueTasks
            };
        }
    }
}