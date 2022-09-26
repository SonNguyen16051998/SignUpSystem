using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Salary
    {
        [Key]
        public int Id_Salary { get; set; }
        [ForeignKey("Teacher")]
        public int Id_Teacher { get; set; }
        [Required]
        public int TeacherSalary { get; set; }//phần trăm lương/1 học sinh
        public float SalaryRecevied { get; set; }//lương nhận được
        public float TroCap { get; set; }
        [Required]
        public float TotalSalary { get; set; }
        [Column(TypeName ="nvarchar(255)")]
        public string Note { get; set; }
        public int Month { get; set; }//lương tháng bao nhiêu
        public int Year { get; set; }//năm bao nhiêu
        public bool Status { get; set; }//trạng thái chốt lương
        public Teacher Teacher { get; set; }
    }
}
