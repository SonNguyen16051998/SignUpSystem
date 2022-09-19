using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class User_Quyen
    {
        [ForeignKey("User")]
        public int Id_User { get; set; }
        [ForeignKey("Quyen")]
        public int Id_Quyen { get; set; }
        public User User { get; set; }
        public Quyen Quyen { get; set; }
    }
}
