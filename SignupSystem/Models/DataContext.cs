using Microsoft.EntityFrameworkCore;

namespace SignupSystem.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            //ràng buộc 2 khóa chính  1 bảng cần sử dụng fluent API
            model.Entity<Student_Class>().HasKey(e => new { e.Id_Class, e.Id_Student });

            model.Entity<Subject_PointType>().HasKey(e => new { e.Code_Subject, e.Id_PointType });

            model.Entity<Role_Quyen>().HasKey(e => new { e.Id_Role, e.Id_Quyen });

            model.Entity<User_Quyen>().HasKey(e => new { e.Id_User, e.Id_Quyen });
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<HolidaySchedule> HolidaySchedules { get; set;}
        public DbSet<Khoa> Khoa { get; set; }
        public DbSet<PointType> PointTypes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectDepartment> SubjectDepartments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSchedule> TeacherSchedules { get; set; }
        public DbSet<Student_Class> Student_Classes { get; set; }
        public DbSet<Student_Point> Student_Points { get; set; }
        public DbSet<Subject_PointType> Subject_Pointypes { get; set; }
        public DbSet<Salary> Salarys { get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }  
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<User_Quyen> Users_Quyen { get; set;}
        public DbSet<Role_Quyen> Role_Quyens { get; set; }
    }
}
