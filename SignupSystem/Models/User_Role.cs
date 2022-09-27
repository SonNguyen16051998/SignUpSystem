using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class User_Role
    {
        [ForeignKey("NguoiDung")]
        public int Ma_NguoiDung { get; set; }
        [ForeignKey("Role")]
        public int Ma_Role { get; set; }
        public User NguoiDung { get; set; }
        public Role Role { get; set; }
    }
}
