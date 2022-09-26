using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public enum TypeOfFee
    {
        [Display(Name ="Thu toàn bộ khóa học(100%)")]
        all=1,
        [Display(Name = "Thu một nửa khóa học (50%")]
        half=2
    }
    public class Fee//học phí
    {
        [Key]
        public int Id_Fee { get; set; }
        [ForeignKey("Class")]
        public int Id_Class { get; set; }
        public TypeOfFee TypeOfFee { get; set; }//loại phí
        public float FeeRate { get; set; }//mức thu phí
        public float Discount { get; set; } // giảm giá
        public DateTime PaymentDate { get; set; }
        [Column(TypeName ="nvarchar(255)")]
        public string Note { get; set; }
        [ForeignKey("Student")]
        public int Id_Student { get; set; }
        public Student Student { get; set; }
        public Class Class { get; set; }
    }
}
