using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class OTP
    {
        [Key,Column(TypeName ="varchar(30)")]
        public string email { get; set; }
        [Column(TypeName ="varchar(6)")]
        public string Code_OTP { get; set; }
        public bool isUse { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
