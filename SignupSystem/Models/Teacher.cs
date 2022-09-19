using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Teacher
    {
        [Key]
        public int Id_Teacher { get; set; }
        [Required(ErrorMessage = "pls enter last name"), Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "pls enter first name"), Column(TypeName = "nvarchar(30)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string TaxCode { get; set; }//mã số thuế
        [Required(ErrorMessage = "pls enter birthday"), Column(TypeName = "date")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage ="pls choose gender")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "pls enter email"), Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "pls enter phone number"), Column(TypeName = "varchar(15)")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar(100)"),Required(ErrorMessage ="pls choose main subject")]
        public string MainSubject { get; set; }//môn dạy chính
        [Column(TypeName = "nvarchar(100)")]
        public string ExtraSubject { get; set; }//môn dạy phụ
        [Column(TypeName = "nvarchar(255)")]
        public string Avatar { get; set; }
        [Required(ErrorMessage = "Enter user password")]
        [Column(TypeName = "varchar(50)"), MinLength(6, ErrorMessage = "Password to 6-12 characters")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Enter retype password")]
        [Column(TypeName = "varchar(50)"), MinLength(6, ErrorMessage = "Password to 6-12 characters")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "Password does not match!!!")]
        [NotMapped]
        public string RetypePassWord { get; set; }
        public ICollection<TeacherSchedule> teacherSchedules { get; set; }
        public ICollection<Salary> Salarys { get; set; }
    }
}
