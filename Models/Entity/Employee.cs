using System.ComponentModel.DataAnnotations;

namespace MB_2.Models.Entity
{
    public class Employee
    {
        [Key]
        public int ID_Employee { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}

