using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class HolidaySchedule// lịch nghỉ
    {
        [Key]
        public int Id_Holiday { get; set; }
        [Required,Column(TypeName ="nvarchar(255)")]
        public string HolidayName { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Reason { get; set; }//lí do
        public DateTime StartTime { get; set; }//bắt đầu
        public DateTime EndTime { get; set; }//kết thúc
    }
}
