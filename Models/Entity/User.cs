using System.ComponentModel.DataAnnotations;
namespace MB_2.Models.Entity
{
    public class User
    {
        [Key]
        public int ID_User { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
