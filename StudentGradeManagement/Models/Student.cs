using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentGradeManagement.Models
{
    public class Student
    {
        
        [Key]
        
        public string StudentId { get; set; } 

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        
        public int Tamil_T1 { get; set; }
        public int English_T1 { get; set; }
        public int Maths_T1 { get; set; }
        public int Science_T1 { get; set; }
        public int Social_T1 { get; set; }

        public int Tamil_T2 { get; set; }
        public int English_T2 { get; set; }
        public int Maths_T2 { get; set; }
        public int Science_T2 { get; set; }
        public int Social_T2 { get; set; }

        public int Tamil_T3 { get; set; }
        public int English_T3 { get; set; }
        public int Maths_T3 { get; set; }
        public int Science_T3 { get; set; }
        public int Social_T3 { get; set; }
    }
}
