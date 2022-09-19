using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
    public class Student_Class
    {
        [ForeignKey("Student")]
        public int Id_Student { get; set; }
        [ForeignKey("Class")]
        public int Id_Class { get; set; }
        public Student Student { get; set; }
        public Class Class { get; set; }
    }
}
