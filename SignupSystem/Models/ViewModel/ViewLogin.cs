using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.ViewModel
{
    public class ViewLogin
    {
        [Key]
        [Required(ErrorMessage = "Bạn cần nhập email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(12)]
        [MinLength(6, ErrorMessage = "Mật khẩu từ 6 - 12 kí tự")]
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
