using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
