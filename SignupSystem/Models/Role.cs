using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Role
    {
        [Key]
        public int Id_Quyen { get; set; }
        [Required,Column(TypeName ="nvarchar(100)")]
        public string NameQuyen { get; set; }
        public ICollection<Role_Quyen> Role_Quyens { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
