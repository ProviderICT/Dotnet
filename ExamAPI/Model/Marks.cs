using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamAPI.Model
{
    public class Marks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Dotnet { get; set; } 
        [Required]
        public int IOS { get; set; }
        [Required]
        public int BLockChain { get; set; }
        public int Total => Dotnet + IOS + BLockChain;

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Student Student { get; set; }
       
       
       
      
    }
}
