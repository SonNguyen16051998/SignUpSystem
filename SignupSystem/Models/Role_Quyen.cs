using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Role_Quyen
    {
        [ForeignKey("Role")]
        public int Id_Role { get; set; }
        [ForeignKey("Quyen")]
        public int Id_Quyen { get; set; }
        public Role Role { get; set; }
        public Quyen Quyen { get; set; }
    }
}
