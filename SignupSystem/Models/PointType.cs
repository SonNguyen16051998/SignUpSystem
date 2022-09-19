using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class PointType//loại điểm
    {
        [Key]
        public int Id_PointType { get; set; }
        [Required,Column(TypeName="nvarchar(50)")]
        public string TypeName { get; set; }//vd: kiểm tra miệng
        [Required]
        public int HeSo { get; set; }//vd: hệ số 1
        public ICollection<Student_Point> Points { get; set; }
        public ICollection<Subject_PointType> Subject_PointTypes { get; set; }
    }
}
