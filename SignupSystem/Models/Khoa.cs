using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Khoa//khoa - khối
    {
        [Key]
        public int Id_Khoa { get; set; }
        [Required(ErrorMessage ="pls enter name khoa"),Column(TypeName ="nvarchar(100)")]
        public string Ten_Khoa { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
