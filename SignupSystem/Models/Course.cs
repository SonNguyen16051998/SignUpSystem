using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Course//khóa học
    {
        [Key,Required(ErrorMessage ="pls enter code course"),Column(TypeName ="varchar(100)")]
        public string Code_Course { get; set; }
        [Required(ErrorMessage ="pls enter name course"),Column(TypeName ="nvarchar(100)")]
        public string NameCoure { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
