using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class TeacherSchedule//lịch dạy
    {
        [Key]
        public int Id_Schedule { get; set; }
        public int Id_Class { get; set; }//mã lớp học
        public int Id_Subject { get; set; }
        [ForeignKey("Teacher")]
        public int Id_Teacher { get; set; }
        [Required(ErrorMessage ="pls enter classroom"),Column(TypeName ="nvarchar(50)")]
        public string Classroom { get; set; }//phòng học
        [Required(ErrorMessage ="pls enter chool day"),Column (TypeName ="nvarchar(100)")]
        public string SchoolDay { get; set; }//thứ học
        [Required]
        public TimeSpan StartTime { get; set; }//giờ bắt đầu
        [Required]
        public TimeSpan EndTime { get; set; }//giờ kết thúc
        [Required]
        public DateTime StartDay { get; set; }//ngày bắt đầu
        [Required]
        public DateTime EndDay { get; set; }//ngày kết thúc
        public Teacher Teacher { get; set; }
    }
}
