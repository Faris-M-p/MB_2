using MB_2.Models;
using MB_2.Models.Entity;
using MB_2.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MB_2.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            {
                _context = context;
            }
        }

        public async Task<List<OutPutEmployeeList>> GetAllEmployees()
        {
            var response = await _context.Employee
                .Where(x=> x.IsDeleted==false)
                .Select(x => new OutPutEmployeeList
                {
                    FK_Employee = x.ID_Employee,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    Email = x.Email,
                    Phone = x.Phone,
                    Department = x.Department,
                    Designation = x.Designation,
                    JoinDate = x.JoinDate,
                })
                .ToListAsync();

            return response;
        }


        public async Task<OutPutEmployeeList> GetEmployeeById(int FK_Employee)
        {
            var response = await _context.Employee

                .Where(x => x.ID_Employee == FK_Employee && x.IsDeleted == false)
                .Select(x => new OutPutEmployeeList
                {
                    FK_Employee = x.ID_Employee,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    Email = x.Email,
                    Phone = x.Phone,
                    Department = x.Department,
                    Designation = x.Designation,
                    JoinDate = x.JoinDate,
                })
                .FirstOrDefaultAsync();
            return response;
        }

        public async Task<bool> DeleteEmployee(int FK_employee)
        {
            try {
                var employee = _context.Employee
                    .Where(x =>  x.ID_Employee == FK_employee && x.IsDeleted == false)
                    .FirstOrDefault();
                if (employee is null)
                    {
                    return false;
                    }
                employee.IsDeleted = true;

                //  _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch {
                return false;
            }
      
        }

        public async Task <bool>UpdateEmployee(InputEmployeeUpdate input)
        { 
            try{
                var employee = _context.Employee.
                        Where(x => x.IsDeleted == false && x.ID_Employee == input.FK_Employee).
                        FirstOrDefault();
                if (employee == null)
                    return false;
                employee.Name = input.Name;
                employee.IsActive = input.IsActive;
                employee.Email = input.Email;
                employee.Phone = input.Phone;
                employee.Department = input.Department;
                employee.Designation = input.Designation;
                employee.JoinDate = input.JoinDate;
                await _context.SaveChangesAsync();
                return true;
            }
            catch{
                return false;
            }


        }

        public async Task<bool> CreateEmployee(InputEmployeeCreate input)
        {
            try
            {
                var employee = new Employee
                {
                    Name = input.Name,
                    IsActive = input.IsActive,
                    Email = input.Email,
                    Phone = input.Phone,
                    Department = input.Department,
                    Designation = input.Designation,
                    JoinDate = input.JoinDate
                };

                _context.Employee.Add(employee);
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
