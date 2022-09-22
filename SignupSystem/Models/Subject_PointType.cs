using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Subject_PointType
    {
        [ForeignKey("Course")]
        public string Code_Course { get; set; }
        [ForeignKey("Subject")]
        public string Code_Subject { get; set; }//ma mon hoc
        [ForeignKey("PointType")]
        public int Id_PointType { get; set; }//kieu diem.vd:diem 1 tiet
        [Required]
        public int Qty { get; set; }//so luong diem
        [Required]
        public int QtyRequied { get; set; }//so luong diem bat buoc
        public Subject Subject { get; set; }
        public PointType PointType { get; set; }
        public Course Course { get; set; }
    }
}
