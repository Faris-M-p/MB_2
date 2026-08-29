using System.ComponentModel.DataAnnotations;

namespace MB_2.Models
{
    public class OutPutTaskList
    {
        public int FK_Task { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_Employee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool Completed { get; set; }
        public int Status { get; set; }
    }
    public class InputTaskCreate 
    {
      [Required(ErrorMessage ="please fill Title ")]
      [StringLength(20,ErrorMessage ="Dont greater than 20 ")]
      public string? Title { get; set; }
      public string Description { get; set; }
      [Required(ErrorMessage = "Please select Employee")]
      public int? FK_Employee { get; set; }
      [Required(ErrorMessage = "Please select Due Date")]
      public DateTime? DueDate { get; set; }
    }
    public class InputTaskDelete
    {
        public int FK_Task { get; set; }

    }
    public class InputTaskUpdate 
    {
        public int FK_Task { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_Employee { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
    }
}
