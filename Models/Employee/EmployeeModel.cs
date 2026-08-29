using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MB_2.Models
{
    public class OutPutEmployeeList
    {
        public int FK_Employee { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime? JoinDate { get; set; }
    }
    public class InputEmployeeCreate 
    {
      [Required(ErrorMessage ="please fill Name ")]
      [StringLength(20,ErrorMessage ="Dont greater than 20 ")]
      public string? Name { get; set; }
      public bool IsActive { get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
      public string Department { get; set; }
      public string Designation { get; set; }
      [Required(ErrorMessage = "Please select Join Date")]
      public DateTime? JoinDate { get; set; }
    }
    public class InputEmployeeDelete
    {
        public int FK_Employee { get; set; }

    }
    public class InputEmployeeUpdate 
    {
        public int FK_Employee { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
