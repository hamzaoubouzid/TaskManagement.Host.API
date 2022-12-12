using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTO
{ 
    public class Tasks
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string TableName { get; set; }
        [Required]
        public virtual string ActionType { get; set; }
    }
    

    public enum ActionType
    {
        Delete_All_Data = 0,
        Random_Add = 1
    }

    public enum TableName
    {
        Students = 0,
        Teachers = 1
    }
}
