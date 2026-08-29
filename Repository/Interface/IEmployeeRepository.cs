using MB_2.Models;

namespace MB_2.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<OutPutEmployeeList>> GetAllEmployees(string searchname = "", string namesort = "", bool? filteractive = null,int page=1,int pagesize=5);

        Task<OutPutEmployeeList> GetEmployeeById(int FK_Employee);
        Task<bool> DeleteEmployee(int FK_employee);
        Task<bool> UpdateEmployee(InputEmployeeUpdate input);
        Task<bool> CreateEmployee(InputEmployeeCreate input);
    }
}
