using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Class//lớp học
    {
        [Key]
        public int Id_Class { get; set; }//mã lớp
        [ForeignKey("Course")]
        public string Code_Course { get; set; }//khóa học
        [ForeignKey("khoa")]
        public int Id_Khoa { get; set; }//khoa - khối
        [Required(ErrorMessage ="pls enter class name"),Column(TypeName ="nvarchar(50)")]
        public string ClassName { get; set; }//tên lớp
        [Required]
        public int QtyStudent { get; set; }//số lượng học sinh
        [Required]
        public float Fee { get; set; }//học phí
        [Column(TypeName ="nvarchar(255)")]
        public string Desc { get; set; }//mô tả
        [Column(TypeName ="nvarchar(255)")]
        public string Avatar { get; set; }
        public bool IsChieuSinh { get; set; }
        public Course Course { get; set; }
        public Khoa khoa { get; set; }
        public ICollection<Fee> Fees { get; set; }
        public ICollection<Student_Class> Student_Classes { get; set; }
    }
}
