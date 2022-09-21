using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.ViewModel
{
    public class ViewDoiMatKhau
    {
        [Key, DataType(DataType.EmailAddress, ErrorMessage = "invalid data email")]
        [Required(ErrorMessage = "pls enter your email")]
        public string email { get; set; }
        [Required(ErrorMessage = "pls enter your currently password")]
        [MinLength(6, ErrorMessage = "password to 6 - 12 characters"), MaxLength(12)]
        public string password { get; set; }
        [Required(ErrorMessage = "pls enter your new password")]
        [MinLength(6, ErrorMessage = "password to 6 - 12 characters"), MaxLength(12)]
        public string newPassword { get; set; }
        [Required(ErrorMessage = "pls enter your new password")]
        [MinLength(6, ErrorMessage = "password to 6 - 12 characters"), MaxLength(12)]
        [Compare("newPassword", ErrorMessage = "retype password do not match")]
        public string retypePassword { get; set; }
    }
}
