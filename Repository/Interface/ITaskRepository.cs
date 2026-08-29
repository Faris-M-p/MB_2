using MB_2.Models;

namespace MB_2.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<List<OutPutTaskList>> GetAllTasks(string searchname = "", string namesort = "", int? filterstatus = null,int page=1,int pagesize=5);

        Task<OutPutTaskList> GetTaskById(int FK_Task);
        Task<bool> DeleteTask(int FK_task);
        Task<bool> UpdateTask(InputTaskUpdate input);
        Task<bool> CreateTask(InputTaskCreate input);
    }
}
