using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class User
    {
        [Key]
        public int Id_User { get; set; }
        [Required,Column(TypeName ="nvarchar(100)")]
        public string UserName { get; set; }
        [Required, Column(TypeName = "nvarchar(40)"),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [ForeignKey("Role")]
        public int Id_Role { get; set; }
        [Column(TypeName = "nvarchar(100)")]
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
        public Role Role { get; set; }
    }
}
