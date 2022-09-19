using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Student_Point
    {
        [Key]
        public int Id_Point { get; set; }
        [ForeignKey("Subject")]
        public string Code_Subject { get; set; }
        [ForeignKey("Student")]
        public int Id_Student { get; set; }
        [ForeignKey("PointType")]
        public int Id_PointType { get; set; }
        public bool IsBlock { get; set; }
        public Subject Subject { get; set; }
        public PointType PointType { get; set; }
        public Student Student { get; set; }
    }
}
