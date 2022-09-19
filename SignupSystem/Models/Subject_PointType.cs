using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Subject_PointType
    {
        [ForeignKey("Subject")]
        public string Code_Subject { get; set; }
        [ForeignKey("PointType")]
        public int Id_PointType { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public int QtyRequied { get; set; }
        public Subject Subject { get; set; }
        public PointType PointType { get; set; }
    }
}
