using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTO
{
    public class TaskExecution
    {
        [Key]
        public Guid IdTask { get; set; }
        public virtual Tasks Task { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }

        // flag for display  status task Expired or not
        public  string? StatusTask { get; set; }

    }
}
