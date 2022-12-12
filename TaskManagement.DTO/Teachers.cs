using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTO
{
    public class Teachers
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string  MainSubjectTeaching { get; set; }
    }
}
