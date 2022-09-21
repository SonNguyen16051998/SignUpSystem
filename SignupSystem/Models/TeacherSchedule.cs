using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class TeacherSchedule//lịch dạy
    {
        [Key]
        public int Id_Schedule { get; set; }
        [ForeignKey("Class")]
        public int Id_Class { get; set; }//mã lớp học
        [ForeignKey("Subject")]
        public string Code_Subject { get; set; }
        [ForeignKey("Teacher")]
        public int Id_Teacher { get; set; }
        [Required(ErrorMessage ="pls enter classroom"),Column(TypeName ="nvarchar(50)")]
        public string Classroom { get; set; }//phòng học
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }//giờ bắt đầu
        [Required]
        public TimeSpan EndTime { get; set; }//giờ kết thúc
        [Required]
        public DateTime StartDay { get; set; }//ngày bắt đầu
        [Required]
        public DateTime EndDay { get; set; }//ngày kết thúc
        public Teacher Teacher { get; set; }
        public Class Class { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Student_Class> student_Classes { get; set; }
    }
}
