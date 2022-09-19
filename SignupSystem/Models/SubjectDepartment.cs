using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class SubjectDepartment//bộ môn
    {
        [Key]
        public int Id_SubjectDerpartment { get; set; }
        [Required(ErrorMessage ="pls enter name subjet department")]
        [Column(TypeName ="nvarchar(100)")]
        public string Subject_DepartmentName { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
