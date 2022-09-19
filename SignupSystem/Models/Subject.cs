using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Subject//môn học
    {
        [Key,Required(ErrorMessage ="pls enter code subject"),Column(TypeName ="varchar(20)")]
        public string Code_Subject { get; set; }//mã môn học
        [Required(ErrorMessage = "pls enter name subject"), Column(TypeName = "varchar(30)")]
        public string NameSubject { get; set; }
        [ForeignKey("SubjectDepartment")]
        public int Id_SubjectDerpartment { get; set; }//id tổ bộ môn
        [ForeignKey("khoa")]
        public int Id_Khoa { get; set; }
        public SubjectDepartment SubjectDepartment { get; set; }
        public Khoa khoa { get; set; }
        public ICollection<Student_Point> Points { get; set; }
        public ICollection<Subject_PointType> Subject_PointTypes { get; set; }
    }
}
