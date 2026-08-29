using System.ComponentModel.DataAnnotations;

namespace MB_2.Models.Entity
{
    public class Task
    {
        [Key]
        public int ID_Task { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_Employee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool Completed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
