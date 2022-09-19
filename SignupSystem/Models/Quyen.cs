using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Quyen
    {
        [Key]
        public int Id_Quyen { get; set; }
        [Column(TypeName ="nvarchar(255)"),Required]
        public string Name { get; set; }
        public ICollection<User_Quyen> User_Quyens { get; set; }
        public ICollection<Role_Quyen> Role_Quyens { get; set; }
    }
}
