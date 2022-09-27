using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignupSystem.Models.ViewModel;

namespace SignupSystem.Services
{
    public interface ITeacher
    {
        Task<List<Teacher>> GetTeachersAsync();//laays danh sach giang vien
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task<bool> AddTeacherAsync(Teacher teacher);//them giang vien
        Task<List<TeacherSchedule>> GetTeacherSchedules(int id_Teacher);//lấy lịch dạy của giảng viên
        Task<bool> UpdateTeacherAsync (Teacher teacher);//cap nhat giang vien
        Task<bool> DeleteTeacherAsync (int id_Teacher);//xoas giang vien
        Task<List<TeacherSchedule>> GetTeacherSchedules();//lay toan bo lich day cuar tat ca giang vien
        Task<bool> AddTeacherScheduleAsync(TeacherSchedule schedule);//them lich day cho giang vien
        Task<bool> UpdateTeacherScheduleAsync(TeacherSchedule schedule);//cap nhat lich day
        Task<bool> DeleteTeacherScheduleAsync(int id_schedule);//xoa lich day
        Task<bool> CheckDeleteTeacher(int id_Teacher);
        Task<Teacher> LoginAsync(ViewLogin login);//ddawng nhap
        Task<bool> isPass(string email, string pass);//kiem tra pass dung hay khong
        Task<bool> isEmail(string email);//kiem tra ton tai cua email
        Task<bool> QuenMatKhauAsync(ViewQuenMatKhau quenMatKhau);//đổi mật khẩu mới khi chọn chức năng quên mật khẩu
        Task<bool> ChangePassAsync(ViewDoiMatKhau changePass);
        Task<int> AddSalaryAsync(Salary salary);//tính lương cho giảng viên
    }
    public class TeacherSvc:ITeacher
    {
        private readonly DataContext _context;
        public TeacherSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Teacher>> GetTeachersAsync()
        {
            List<Teacher> teachers = new List<Teacher>();
            teachers = await _context.Teachers.ToListAsync();
            return teachers;
        }
        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _context.Teachers.Where(x=>x.Id_Teacher == id).FirstOrDefaultAsync();
        }
        public async Task<bool> AddTeacherAsync(Teacher teacher)
        {
            bool ret = false;
            try
            {
                teacher.PassWord=Helpers.MaHoaHelper.Mahoa(teacher.PassWord);
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<List<TeacherSchedule>> GetTeacherSchedules(int id_Teacher)
        {
            List<TeacherSchedule> teacherSchedules = new List<TeacherSchedule>();
            teacherSchedules = await _context.TeacherSchedules.Where(x=>x.Id_Teacher == id_Teacher)
                            .Include(x=>x.Class)
                            .Include(x=>x.Subject)
                            .ToListAsync();
            return teacherSchedules;
        }
        public async Task<bool> UpdateTeacherAsync(Teacher teacher)
        {
            bool ret = false;
            try
            {
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteTeacherAsync(int id_Teacher)
        {
            bool ret = false;
            try
            {
                Teacher teacher = new Teacher();
                teacher = await _context.Teachers.Where(x => x.Id_Teacher == id_Teacher).FirstOrDefaultAsync();
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<List<TeacherSchedule>> GetTeacherSchedules()
        {
            List<TeacherSchedule> teacherSchedules = new List<TeacherSchedule>();
            teacherSchedules = await _context.TeacherSchedules.ToListAsync();
            return teacherSchedules;
        }
        public async Task<bool> AddTeacherScheduleAsync(TeacherSchedule schedule)
        {
            bool ret = false;
            try
            {
                await _context.TeacherSchedules.AddAsync(schedule);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> UpdateTeacherScheduleAsync(TeacherSchedule schedule)
        {
            bool ret = false;
            try
            {
                _context.TeacherSchedules.Update(schedule);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteTeacherScheduleAsync(int id_schedule)
        {
            bool ret = false;
            try
            {
                Student_Class student=await _context.Student_Classes
                    .Where(x=>x.Id_ScheduleTeacher==id_schedule).FirstOrDefaultAsync();
                _context.Student_Classes.Remove(student);
                TeacherSchedule teacher = await _context.TeacherSchedules
                    .Where(x => x.Id_Schedule == id_schedule).FirstOrDefaultAsync();
                _context.TeacherSchedules.Remove(teacher);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> CheckDeleteTeacher(int id_Teacher)
        {
            TeacherSchedule teacherSchedule = await _context.TeacherSchedules
                                            .Where(x => x.Id_Teacher == id_Teacher).FirstOrDefaultAsync();
            Salary salary=await _context.Salarys.Where(x=>x.Id_Teacher==id_Teacher).FirstOrDefaultAsync();
            if(teacherSchedule == null && salary==null)
            {
                return true;
            }
            return false;
        }
        public async Task<Teacher> LoginAsync(ViewLogin login)
        {
            Teacher teacher=await _context.Teachers.Where(x=>x.Email==login.Email 
                && x.PassWord == Helpers.MaHoaHelper.Mahoa(login.PassWord)).FirstOrDefaultAsync();
            if (teacher != null)
            {
                return teacher;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> isPass(string email, string pass)
        {
            bool ret = false;
            try
            {
                Teacher nguoiDung = await _context.Teachers.Where(x => x.Email == email
                && x.PassWord == Helpers.MaHoaHelper.Mahoa(pass))
                    .FirstOrDefaultAsync();
                if (nguoiDung != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public async Task<bool> isEmail(string email)
        {
            bool ret = false;
            try
            {
                Teacher nguoiDung = await _context.Teachers.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (nguoiDung != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
       
        public async Task<bool> QuenMatKhauAsync(ViewQuenMatKhau quenMatKhau)
        {
            bool result = false;
            try
            {
                Teacher nguoiDung = await _context.Teachers.Where(x => x.Email == quenMatKhau.Email).FirstOrDefaultAsync();
                nguoiDung.PassWord = Helpers.MaHoaHelper.Mahoa(quenMatKhau.NewPass);
                _context.Teachers.Update(nguoiDung);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { }
            return result;
        }
        public async Task<bool> ChangePassAsync(ViewDoiMatKhau changePass)
        {
            bool result = false;
            try
            {
                Teacher nguoiDung = await _context.Teachers.Where(x => x.Email == changePass.email).FirstOrDefaultAsync();
                nguoiDung.PassWord = Helpers.MaHoaHelper.Mahoa(changePass.newPassword);
                _context.Teachers.Update(nguoiDung);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { }
            return result;
        }
        public async Task<int> AddSalaryAsync(Salary salary)
        {
            int ret = 0;
            try
            {
                List<Fee> feeList = await _context.Fees.Where(x => x.Id_Teacher == salary.Id_Teacher
                                    && x.PaymentDate.Year == salary.Year && x.PaymentDate.Month == salary.Month).ToListAsync();
                float doanhthu = 0;
                foreach(var item in feeList)
                {
                    doanhthu += item.TongThu;
                }
                salary.SalaryRecevied = doanhthu * salary.TeacherSalary / 100;
                salary.TotalSalary= (doanhthu * salary.TeacherSalary / 100) + salary.TroCap;
                await _context.Salarys.AddAsync(salary);
                await _context.SaveChangesAsync();
                ret = salary.Id_Salary;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
